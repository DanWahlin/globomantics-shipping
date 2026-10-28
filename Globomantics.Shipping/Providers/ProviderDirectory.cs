using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Providers;

/// <summary>
/// Looks up the adapter for a provider and uses it to describe shipments in Globomantics Shipping's terms.
/// </summary>
public sealed class ProviderDirectory(IEnumerable<IProviderAdapter> adapters)
{
    private readonly IReadOnlyList<IProviderAdapter> _adapters = [.. adapters];

    public IReadOnlyList<IProviderAdapter> All => _adapters;

    public IProviderAdapter? Find(string providerId) =>
        _adapters.FirstOrDefault(a =>
            string.Equals(a.ProviderId, providerId, StringComparison.OrdinalIgnoreCase));

    public ShipmentOverview Describe(Shipment shipment)
    {
        var provider = Find(shipment.ProviderId)
            ?? throw new InvalidOperationException($"No adapter is registered for provider '{shipment.ProviderId}'.");

        var status = shipment.LatestEvent is { } latest
            ? provider.ToShipmentStatus(latest.StatusCode)
            : ShipmentStatus.Booked;

        return new ShipmentOverview(shipment, provider, provider.FindService(shipment.ServiceCode), status);
    }
}

/// <summary>A shipment together with what its provider's codes mean.</summary>
public sealed record ShipmentOverview(
    Shipment Shipment,
    IProviderAdapter Provider,
    ServiceOption? Service,
    ShipmentStatus Status);
