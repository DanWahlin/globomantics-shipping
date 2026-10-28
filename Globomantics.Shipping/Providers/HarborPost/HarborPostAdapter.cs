using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Providers.HarborPost;

public sealed class HarborPostAdapter : IProviderAdapter
{
    public string ProviderId => "harbor-post";
    public string DisplayName => "Harbor Post";
    public IReadOnlyList<ServiceOption> Services => HarborPostServiceCodes.All;
    public IReadOnlyDictionary<string, ShipmentStatus> TrackingStatuses => HarborPostTrackingStatuses.All;
}
