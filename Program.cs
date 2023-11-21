using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using MvcMovie.Middleware;
using MvcMovie.Models;
using MvcMovie.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IStartupFilter, RequestStartupFilter>();
builder.Services.AddTransient<ISendMailService, SendMailService>();

//Logger
builder.Logging.ClearProviders().AddConsole();
//Check environment
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<MvcMovieContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("MvcMovieContext")));
}
else
{
    builder.Services.AddDbContext<MvcMovieContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ProductionMvcMovieContext")));
}
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.MapControllerRoute(
    name: "error",
    pattern: "Error",
    defaults: new { controller = "Home", action = "Error" });

app.MapControllerRoute(
    name: "TestMail",
    pattern: "TestMail",
    defaults: new { controller = "Home", action = "TestMail" });


app.MapFallback(context =>
{
    context.Response.Redirect("/Error");
    return Task.CompletedTask;
});

app.Run();

