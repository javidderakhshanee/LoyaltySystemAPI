using LoyaltySystemApi.Infrastructure.Caches.Configs;
using LoyaltySystemApi.Infrastructure.Caches.Services;
using LoyaltySystemApi.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Collections.Concurrent;

namespace LoyaltySystemApi.Infrastructure.Caches.Implementations;

public sealed class RedisCacheService : ICacheManagementService
{
    private readonly IConfiguration _configuration;
    private IDatabase Db(string part) { return RedisConnectionFactory.GetDatabase(part, _configuration); }

    public RedisCacheService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> Exists<T>(string key, string part, CancellationToken cancellationToken = default)
    {
        return await Db(part).KeyExistsAsync(new RedisKey(key));
    }

    public async Task<T> Get<T>(string key, string part , CancellationToken cancellationToken = default)
    {
        var value = await Db(part).StringGetAsync(GenerateKey(key, part));
        if (value.IsNullOrEmpty)
            return default;

        return value.ToString().Deserialized<T>();
    }

    private static RedisKey GenerateKey(string key, string part)
    {
        return new RedisKey($"{(!string.IsNullOrWhiteSpace(part) ? $"{part}:" : "")}{key}");
    }

    public async Task Remove(string key, string part, CancellationToken cancellationToken = default)
    {
        await Db(part).KeyDeleteAsync(new RedisKey(key));
    }

    public async Task Set<T>(string key, T value, string part, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        await Db(part).StringSetAsync(new RedisKey(key), new RedisValue(value.Serialize().Compress()), expiry: expiration.HasValue ? expiration.Value : null);
    }
    public async Task SetBulk<T>(string key, List<T> list, string part, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        var items = new ConcurrentBag<KeyValuePair<RedisKey, RedisValue>>();
        foreach (var item in list)
            items.Add(new KeyValuePair<RedisKey, RedisValue>
                        (new RedisKey(key), new RedisValue(item.Serialize().Compress())));

        await Db(part).StringSetAsync(items.ToArray());
    }
}
