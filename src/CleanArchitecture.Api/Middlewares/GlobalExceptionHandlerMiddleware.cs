using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.Exceptions;

namespace CleanArchitecture.Api.Middlewares;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex) when (ex is IAppException businessException)
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<GlobalExceptionHandlerMiddleware>>();

            logger.LogError(ex, $"Business exception has thrown");

            context.Response.StatusCode = ex switch
            {
                ArgumentException => StatusCodes.Status400BadRequest,
                EntityNotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            await WriteResponseAsync(context, ex);
        }
        catch (Exception ex)
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<GlobalExceptionHandlerMiddleware>>();
            logger.LogError(ex, "An unhandled exception has occurred");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await WriteResponseAsync(context, ex);
        }
    }

    private static async Task WriteResponseAsync(HttpContext context, Exception ex)
    {
        var response = new
        {
            Error = ex.Message.Replace(Environment.NewLine, " - ")
        };

        await context.Response.WriteAsJsonAsync(response);
    }

}