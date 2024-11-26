namespace LoyaltySystemApi.Infrastructure.Caches.Models;

public sealed class CacheSettingPart
{
    public string ProviderName { get; set; }
    public int ExpirationScanFrequencyInSecond { get; set; }
    public int ExpirationTimeInMinute { get; set; }
    public int PeriodReloadTimeInMinute { get; set; }
    public bool ReloadOnStartUp { get; set; }
    public int ConnectRetry { get; set; }
}
