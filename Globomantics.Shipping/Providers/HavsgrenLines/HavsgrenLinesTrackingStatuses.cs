using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Providers.HavsgrenLines;

public static class HavsgrenLinesTrackingStatuses
{
    // Havsgren Lines is still being onboarded. Its tracking codes are defined in the
    // Havsgren Lines integration spec in the provider catalog.
    public static IReadOnlyDictionary<string, ShipmentStatus> All { get; } =
        new Dictionary<string, ShipmentStatus>(StringComparer.OrdinalIgnoreCase);
}
