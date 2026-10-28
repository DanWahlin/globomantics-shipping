namespace Globomantics.Shipping.Shipments;

/// <summary>
/// Globomantics Shipping's own tracking statuses. Every provider's tracking codes are mapped to one of these.
/// </summary>
public enum ShipmentStatus
{
    Unknown,
    Booked,
    PickedUp,
    InTransit,
    OutForDelivery,
    Delivered,
    DeliveryFailed,
    OnHold,
    Returned,
}

public static class ShipmentStatusExtensions
{
    public static string DisplayName(this ShipmentStatus status) => status switch
    {
        ShipmentStatus.Booked => "Booked",
        ShipmentStatus.PickedUp => "Picked up",
        ShipmentStatus.InTransit => "In transit",
        ShipmentStatus.OutForDelivery => "Out for delivery",
        ShipmentStatus.Delivered => "Delivered",
        ShipmentStatus.DeliveryFailed => "Delivery failed",
        ShipmentStatus.OnHold => "On hold",
        ShipmentStatus.Returned => "Returned to sender",
        _ => "Unknown status",
    };

    public static bool NeedsAttention(this ShipmentStatus status) =>
        status is ShipmentStatus.DeliveryFailed
            or ShipmentStatus.OnHold
            or ShipmentStatus.Returned
            or ShipmentStatus.Unknown;

    /// <summary>
    /// Groups statuses into a handful of colours for the UI.
    /// </summary>
    public static string Tone(this ShipmentStatus status) => status switch
    {
        ShipmentStatus.Booked => "waiting",
        ShipmentStatus.PickedUp or ShipmentStatus.InTransit => "moving",
        ShipmentStatus.OutForDelivery => "arriving",
        ShipmentStatus.Delivered => "done",
        ShipmentStatus.Unknown => "unknown",
        _ => "problem",
    };
}
