using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Providers.HavsgrenLines;

public static class HavsgrenLinesServiceCodes
{
    // Havsgren Lines is still being onboarded. Its service codes are defined in the
    // Havsgren Lines integration spec in the provider catalog.
    public static IReadOnlyList<ServiceOption> All { get; } = [];
}
