using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Providers.GreyvallCouriers;

public static class GreyvallCouriersServiceCodes
{
    public static IReadOnlyList<ServiceOption> All { get; } =
    [
        new("GRV-SAVER", "Greyvall Saver", ServiceLevel.Economy, TransitDays: 4),
        new("GRV-STD", "Greyvall Standard", ServiceLevel.Standard, TransitDays: 2),
        new("GRV-PRIO", "Greyvall Priority", ServiceLevel.Express, TransitDays: 1),
    ];
}
