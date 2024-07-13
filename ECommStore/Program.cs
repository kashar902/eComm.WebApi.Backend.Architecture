using Core;
using Infra;
using Infra.Constants;
using Infra.DatabaseContext;
using Infra.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(
    (options) => {
        _ = options.UseSqlServer(builder.Configuration.GetConnectionString(Consts.ConnectionString));
    });

builder.Services.AddTransient<GlobalExceptionalHandlingMiddleware>();

builder.Services
    .AddCore()
    .AddInfra();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GlobalExceptionalHandlingMiddleware>();

app.MapControllers();

app.Run();