using System.Net;
using System.Text.Json;
using Infra.Constants;
using Infra.Profiles;
using Infra.Profiles.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infra.Middlewares;

public class GlobalExceptionalHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionalHandlingMiddleware> _logger;

    public GlobalExceptionalHandlingMiddleware(ILogger<GlobalExceptionalHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate _next)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = Consts.ContentType;
        HttpResponse response = context.Response;

        ApiResponse errorResponse = new()
        {
            IsSuccess = false
        };


        switch (exception)
        {
            case ApplicationException ex:
                HandleApplicationException(response, errorResponse, ex);
                break;
            case DbUpdateException dbEx when dbEx?.InnerException is SqlException sqlEx && sqlEx.Number == 2627:
                HandleDuplicateRecordException(response, errorResponse, dbEx);
                break;
            case DbUpdateException dbEx when dbEx.InnerException is SqlException sqlEx &&
                                             (sqlEx.Number == 547 || sqlEx.Number == 5471):
                HandleForeignKeyConstraintException(response, errorResponse, dbEx);
                break;
            default:
                HandleInternalServerError(response, errorResponse, exception);
                break;
        }

        string result = JsonSerializer.Serialize(errorResponse);
        _logger.LogError(result);
        await context.Response.WriteAsync(result);
    }

    private void HandleApplicationException(HttpResponse response, ApiResponse errorResponse, ApplicationException ex)
    {
        if (ex.Message.Contains(Consts.InvalidToken))
        {
            response.StatusCode = (int)HttpStatusCode.Forbidden;
            errorResponse.Message = ex.Message;
        }
        else
        {
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            errorResponse.Message = ex.Message;
        }
    }

    private void HandleDuplicateRecordException(HttpResponse response, ApiResponse errorResponse,
        DbUpdateException dbEx)
    {
        response.StatusCode = (int)HttpStatusCode.BadRequest;
        errorResponse.Message = Consts.AlreadyExist;
        errorResponse.Response = dbEx?.InnerException?.Message;
    }

    private void HandleForeignKeyConstraintException(HttpResponse response, ApiResponse errorResponse,
        DbUpdateException dbEx)
    {
        response.StatusCode = (int)HttpStatusCode.BadRequest;
        errorResponse.Message = Consts.ContentUsedByOther;
        errorResponse.Response = dbEx?.InnerException?.Message;
    }

    private void HandleInternalServerError(HttpResponse response, ApiResponse errorResponse, Exception exception)
    {
        response.StatusCode = (int)HttpStatusCode.InternalServerError;
        errorResponse.Message = exception.Message;
    }
}