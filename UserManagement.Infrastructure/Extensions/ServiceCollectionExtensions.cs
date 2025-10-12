using Microsoft.Extensions.DependencyInjection;
using UserManagement.Infrastructure.Implementations;
using UserManagement.Infrastructure.Interfaces;

namespace UserManagement.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        => services.AddScoped<IUserRepository, UserRepository>();
}
