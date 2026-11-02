using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Providers.GreyvallCouriers;

public sealed class GreyvallCouriersAdapter : IProviderAdapter
{
    public string ProviderId => "greyvall-couriers";
    public string DisplayName => "Greyvall Couriers";
    public IReadOnlyList<ServiceOption> Services => GreyvallCouriersServiceCodes.All;
    public IReadOnlyDictionary<string, ShipmentStatus> TrackingStatuses => GreyvallCouriersTrackingStatuses.All;
}
