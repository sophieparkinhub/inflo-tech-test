using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Extensions;
using UserManagement.Infrastructure.Extensions;
using Westwind.AspNetCore.Markdown;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddDataAccess()
    .AddInfrastructureLayer()
    .AddApplicationLayer()
    .AddDomainServices()
    .AddMarkdown()
    .AddControllersWithViews();

var app = builder.Build();

app.UseMarkdown();

app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
