using Microsoft.EntityFrameworkCore;
using UserManagement.Data;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DataContext>(options =>
              options.UseSqlServer(connectionString));

        return services;
    }
}
