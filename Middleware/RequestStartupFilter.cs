namespace MvcMovie.Middleware;
// <snippet1>
public class RequestStartupFilter : IStartupFilter
{
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return builder =>
        {
            builder.UseMiddleware<RequestGetMenusMiddleware>();
            builder.UseMiddleware<RequestShowLogsMiddleware>();
            next(builder);
        };
    }
}