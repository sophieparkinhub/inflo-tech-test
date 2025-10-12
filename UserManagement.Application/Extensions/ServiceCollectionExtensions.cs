using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Mapper;

namespace UserManagement.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        => services.AddScoped<IUserMapper, UserMapper>();
}

