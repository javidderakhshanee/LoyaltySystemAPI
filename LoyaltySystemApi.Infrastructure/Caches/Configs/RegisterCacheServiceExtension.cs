using LoyaltySystemApi.Infrastructure.Caches.Implementations;
using LoyaltySystemApi.Infrastructure.Caches.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LoyaltySystemApi.Infrastructure.Caches.Configs;

public static class RegisterCacheServiceExtension
{
    public static IServiceCollection RegisterCacheService(this IServiceCollection services)
    {

        return services
            .AddScoped<ICacheManagementService, RedisCacheService>();
    }
}
