using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Providers.TornvikCouriers;

public sealed class TornvikCouriersAdapter : IProviderAdapter
{
    public string ProviderId => "tornvik-couriers";
    public string DisplayName => "Tornvik Couriers";
    public IReadOnlyList<ServiceOption> Services => TornvikCouriersServiceCodes.All;
    public IReadOnlyDictionary<string, ShipmentStatus> TrackingStatuses => TornvikCouriersTrackingStatuses.All;
}
