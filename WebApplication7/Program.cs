using System;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<taskContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("constring")));

builder.Services.AddControllersWithViews();

/*builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Account/Login"; // Redirect to login page if unauthorized
        options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect if access is denied
    });*/


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ProblemInfoes}/{action=Index}/{id?}");

app.Run();
