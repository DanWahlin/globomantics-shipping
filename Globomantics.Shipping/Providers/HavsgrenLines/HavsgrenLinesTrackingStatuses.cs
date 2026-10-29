using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Providers.HavsgrenLines;

public static class HavsgrenLinesTrackingStatuses
{
    public static IReadOnlyDictionary<string, ShipmentStatus> All { get; } =
        new Dictionary<string, ShipmentStatus>(StringComparer.OrdinalIgnoreCase)
        {
            ["MANIFESTED"] = ShipmentStatus.Booked,
            ["COLLECTED"] = ShipmentStatus.PickedUp,
            ["AT_HUB"] = ShipmentStatus.InTransit,
            ["IN_LINEHAUL"] = ShipmentStatus.InTransit,
            ["OFD"] = ShipmentStatus.OutForDelivery,
            ["DELIVERED"] = ShipmentStatus.Delivered,
            ["DELIVERED_NEIGHBOUR"] = ShipmentStatus.Delivered,
            ["ATTEMPT_FAILED"] = ShipmentStatus.DeliveryFailed,
            ["ADDRESS_QUERY"] = ShipmentStatus.OnHold,
            ["CUSTOMS_HOLD"] = ShipmentStatus.OnHold,
            ["RETURNED_TO_SENDER"] = ShipmentStatus.Returned,
            ["IN_LOCKER"] = ShipmentStatus.Delivered,
        };
}
