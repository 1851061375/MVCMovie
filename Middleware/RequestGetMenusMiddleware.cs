using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

public class RequestGetMenusMiddleware
{
    private readonly RequestDelegate _next;
    //RequestDelegate ~ async (HttpContext httpContext) => {}
    public RequestGetMenusMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, MvcMovieContext context)
    {

        httpContext.Items["Menus"] = context.MenuCustom
            .Where(x => x.ParentId == null)
            .Include(nameof(MenuCustom.Childrens))
            .ToList();
        await _next(httpContext);
    }
}