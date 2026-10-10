using ApiInvoices.Application.DTOs;
using ApiInvoices.Application.Validations;
using System.Net;
using System.Text.Json;

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
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = ApiResponse<object>.Error(ex.Message);
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
