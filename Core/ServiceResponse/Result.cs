using Infra.Constants;

namespace Core.ServiceResponse;

public class Result
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public object? Response { get; set; }
    
    private Result() { }

    private Result(bool isSuccess, string message, object? response)
    {
        IsSuccess = isSuccess;
        Message = message;
        Response = response;
    }

    private Result(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public static Result Added() => new(true, Consts.AddSuccessful);

    public static Result Added(object response) => new(true, Consts.AddSuccessful, response);

    public static Result Deleted() => new(true, Consts.DeleteSuccessful);
    
    public static Result Updated() => new(true, Consts.UpdateSuccessful);

    public static Result Updated(object response) => new(true, Consts.UpdateSuccessful, response);


    public static Result Success(string message) => new(true, message);

    public static Result Success(string message, object response) => new(true, message, response);

    public static Result Failure(string message) => new(false, message);

    public static Result Failure(string message, object response) => new(false, message, response);
}