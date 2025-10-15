using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Data;

namespace UserManagement.Web;

public static class MigrationManager
{
    public static WebApplication MigrateDatabase(this WebApplication webApp)
    {
        using (var scope = webApp.Services.CreateScope())
        {
            using (var appContext = scope.ServiceProvider.GetRequiredService<DataContext>())
            {
                try
                {
                    appContext.Database.Migrate();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        return webApp;
    }
}
