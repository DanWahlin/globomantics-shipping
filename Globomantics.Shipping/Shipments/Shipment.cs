namespace Globomantics.Shipping.Shipments;

public sealed class Shipment
{
    public required string Reference { get; init; }
    public required string ProviderId { get; init; }
    public required string ServiceCode { get; init; }
    public required Recipient Recipient { get; init; }
    public required decimal WeightKg { get; init; }
    public required DateTimeOffset BookedAt { get; init; }

    public List<TrackingEvent> Events { get; } = [];

    public TrackingEvent? LatestEvent => Events.MaxBy(e => e.OccurredAt);

    public DateTimeOffset LastActivity => LatestEvent?.OccurredAt ?? BookedAt;
}

public sealed record Recipient(string Name, string Street, string PostalCode, string City);

/// <summary>
/// A tracking update exactly as the provider sent it. <see cref="StatusCode"/> is the provider's own code.
/// </summary>
public sealed record TrackingEvent(DateTimeOffset OccurredAt, string StatusCode, string Location);
