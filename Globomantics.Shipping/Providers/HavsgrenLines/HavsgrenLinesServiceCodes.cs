using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Providers.HavsgrenLines;

public static class HavsgrenLinesServiceCodes
{
    public static IReadOnlyList<ServiceOption> All { get; } =
    [
        new("HVL-ECO", "Havsgren Economy", ServiceLevel.Economy, TransitDays: 5),
        new("HVL-STD", "Havsgren Standard", ServiceLevel.Standard, TransitDays: 2),
        new("HVL-NXT", "Havsgren Next Day", ServiceLevel.Express, TransitDays: 1),
        new("HVL-SDY", "Havsgren Same Day", ServiceLevel.SameDay, TransitDays: 0),
    ];
}
