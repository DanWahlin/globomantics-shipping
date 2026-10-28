using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Providers.HavsgrenLines;

public sealed class HavsgrenLinesAdapter : IProviderAdapter
{
    public string ProviderId => "havsgren-lines";
    public string DisplayName => "Havsgren Lines";
    public IReadOnlyList<ServiceOption> Services => HavsgrenLinesServiceCodes.All;
    public IReadOnlyDictionary<string, ShipmentStatus> TrackingStatuses => HavsgrenLinesTrackingStatuses.All;
}
