using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Providers.TornvikCouriers;

public static class TornvikCouriersServiceCodes
{
    public static IReadOnlyList<ServiceOption> All { get; } =
    [
        new("TVK-ECO", "Tornvik Economy", ServiceLevel.Economy, TransitDays: 5),
        new("TVK-STD", "Tornvik Standard", ServiceLevel.Standard, TransitDays: 2),
        new("TVK-EXP", "Tornvik Express", ServiceLevel.Express, TransitDays: 1),
    ];
}
