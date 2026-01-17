using System.Net;
using System.Text.Json;

namespace ATM.Backend.Api.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError; // 500 if unexpected

        if (exception is InvalidOperationException) 
        {
            // Username duplicado ou outras operações inválidas retornam 400 Bad Request
            code = HttpStatusCode.BadRequest;
        }
        else if (exception is KeyNotFoundException) 
        {
            code = HttpStatusCode.NotFound;
        }
        else if (exception is UnauthorizedAccessException) 
        {
            code = HttpStatusCode.Unauthorized;
        }

        var result = JsonSerializer.Serialize(new
        {
            error = exception.Message,
            status = (int)code,
            timestamp = DateTime.UtcNow
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }
}