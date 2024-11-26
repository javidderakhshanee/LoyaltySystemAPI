namespace LoyaltySystemApi.Infrastructure.Caches.Services;

public interface ICacheManagementService
{
    Task<T> Get<T>(string key, string part, CancellationToken cancellationToken = default);
    Task Set<T>(string key, T data, string part, TimeSpan? expiration = null, CancellationToken cancellationToken = default);
    Task SetBulk<T>(string key, List<T> list, string part, TimeSpan? expiration = null, CancellationToken cancellationToken = default);
    Task Remove(string key, string part, CancellationToken cancellationToken = default);
    Task<bool> Exists<T>(string key, string part, CancellationToken cancellationToken = default);
}
