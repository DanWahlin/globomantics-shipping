using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Providers.HarborPost;

public static class HarborPostTrackingStatuses
{
    public static IReadOnlyDictionary<string, ShipmentStatus> All { get; } =
        new Dictionary<string, ShipmentStatus>(StringComparer.OrdinalIgnoreCase)
        {
            ["PRE_ADVICE"] = ShipmentStatus.Booked,
            ["ACCEPTED"] = ShipmentStatus.PickedUp,
            ["IN_TRANSIT"] = ShipmentStatus.InTransit,
            ["ARRIVED_AT_DEPOT"] = ShipmentStatus.InTransit,
            ["OUT_FOR_DELIVERY"] = ShipmentStatus.OutForDelivery,
            ["DELIVERED"] = ShipmentStatus.Delivered,
            ["DELIVERED_TO_PICKUP_POINT"] = ShipmentStatus.Delivered,
            ["NOT_DELIVERED"] = ShipmentStatus.DeliveryFailed,
            ["HELD"] = ShipmentStatus.OnHold,
            ["RETURNED"] = ShipmentStatus.Returned,
        };
}
