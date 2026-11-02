using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Providers.GreyvallCouriers;

public static class GreyvallCouriersTrackingStatuses
{
    public static IReadOnlyDictionary<string, ShipmentStatus> All { get; } =
        new Dictionary<string, ShipmentStatus>(StringComparer.OrdinalIgnoreCase);
}
