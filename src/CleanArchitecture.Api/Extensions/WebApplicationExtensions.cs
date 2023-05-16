using CleanArchitecture.Api.Middlewares;

namespace CleanArchitecture.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void AutomaticDatabaseMigratorMiddleware(this WebApplication webApplication)
    {
        webApplication.UseMiddleware<AutomaticDatabaseMigratorMiddleware>();
    }

    public static void UseGlobalExceptionHandler(this WebApplication webApplication)
    {
        webApplication.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}