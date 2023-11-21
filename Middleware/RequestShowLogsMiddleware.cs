using System.Net;
using MvcMovie.Data;


public class RequestShowLogsMiddleware
{
    private readonly RequestDelegate _next;
    public RequestShowLogsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var option = httpContext.Request.Query["logs"];

        if (!string.IsNullOrWhiteSpace(option))
        {
            Console.WriteLine("[LOGS]: " + httpContext.Items["Menus"]);
        }

        await _next(httpContext);
    }
}
