using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Providers.HarborPost;

public static class HarborPostServiceCodes
{
    public static IReadOnlyList<ServiceOption> All { get; } =
    [
        new("HP-ECO", "Harbor Post Economy", ServiceLevel.Economy, TransitDays: 4),
        new("HP-PRI", "Harbor Post Priority", ServiceLevel.Standard, TransitDays: 2),
        new("HP-EXP", "Harbor Post Express", ServiceLevel.Express, TransitDays: 1),
    ];
}
