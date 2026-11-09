using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Providers.TornvikCouriers;

public static class TornvikCouriersTrackingStatuses
{
    public static IReadOnlyDictionary<string, ShipmentStatus> All { get; } =
        new Dictionary<string, ShipmentStatus>(StringComparer.OrdinalIgnoreCase);
}
