using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Providers;

/// <summary>
/// Translates one delivery provider's codes into Globomantics Shipping's own service levels and statuses.
/// </summary>
public interface IProviderAdapter
{
    /// <summary>Stable id used in URLs and stored on shipments, for example "harbor-post".</summary>
    string ProviderId { get; }

    string DisplayName { get; }

    /// <summary>The services we can book with this provider.</summary>
    IReadOnlyList<ServiceOption> Services { get; }

    /// <summary>The provider's tracking codes and the Globomantics Shipping status each one means.</summary>
    IReadOnlyDictionary<string, ShipmentStatus> TrackingStatuses { get; }
}

/// <summary>A service a provider offers, with its code exactly as the provider spells it.</summary>
public sealed record ServiceOption(string Code, string Name, ServiceLevel Level, int TransitDays);

public static class ProviderAdapterExtensions
{
    public static ServiceOption? FindService(this IProviderAdapter adapter, string serviceCode) =>
        adapter.Services.FirstOrDefault(s =>
            string.Equals(s.Code, serviceCode, StringComparison.OrdinalIgnoreCase));

    /// <summary>
    /// Returns the Globomantics Shipping status for a provider tracking code, or
    /// <see cref="ShipmentStatus.Unknown"/> when the code isn't mapped yet.
    /// </summary>
    public static ShipmentStatus ToShipmentStatus(this IProviderAdapter adapter, string statusCode) =>
        adapter.TrackingStatuses.GetValueOrDefault(statusCode, ShipmentStatus.Unknown);

    public static bool IsOnboarding(this IProviderAdapter adapter) =>
        adapter.Services.Count == 0 || adapter.TrackingStatuses.Count == 0;
}
