using System.Net;
using ApiInvoices.Api.Dtos;
using ApiInvoices.Application.Validations;

namespace ApiInvoices.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionMiddleware> logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unhandled API exception.");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception switch
            {
                ValidationException => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var message = exception is ValidationException or UnauthorizedAccessException
                ? exception.Message
                : "An unexpected error occurred.";

            await context.Response.WriteAsJsonAsync(ApiResponse<object>.Error(message));
        }
    }
}
