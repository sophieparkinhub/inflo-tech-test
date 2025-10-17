using UserManagement.Application.Extensions;
using UserManagement.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClientUI",
          policy =>
          {
              policy.WithOrigins("https://localhost:7085")
              .AllowAnyHeader()
              .AllowAnyMethod();
          });
}).AddDataAccess(builder.Configuration.GetConnectionString("DefaultConnection")!)
    .AddInfrastructureLayer()
    .AddApplicationLayer()
    .AddDomainServices();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("AllowBlazorClientUI");

app.UseAuthorization();

app.MapControllers();

app.Run();
