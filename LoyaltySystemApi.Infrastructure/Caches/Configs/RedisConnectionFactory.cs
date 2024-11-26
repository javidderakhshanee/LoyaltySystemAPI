using LoyaltySystemApi.Infrastructure.Caches.Models;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace LoyaltySystemApi.Infrastructure.Caches.Configs;

public static class RedisConnectionFactory
{
    public static IDatabase GetDatabase(string name, IConfiguration configuration)
    {
        var config = configuration
            .GetSection($"Caching:{name}")
            .Get<CacheSettingPart>();


        var configString = configuration.GetConnectionString("RedisConnectionString");

        var options = ConfigurationOptions.Parse(configString);
        options.ClientName = config.ProviderName;
        options.AllowAdmin = true;
        options.ConnectTimeout = config.ExpirationTimeInMinute;
        options.ConnectRetry = config.ConnectRetry;
        var conn = ConnectionMultiplexer.Connect(options);

        return conn.GetDatabase();
    }
}
