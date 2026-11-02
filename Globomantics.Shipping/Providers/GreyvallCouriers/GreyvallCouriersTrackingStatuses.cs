using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Providers.GreyvallCouriers;

public static class GreyvallCouriersTrackingStatuses
{
    public static IReadOnlyDictionary<string, ShipmentStatus> All { get; } =
        new Dictionary<string, ShipmentStatus>(StringComparer.OrdinalIgnoreCase)
        {
            ["REGISTERED"] = ShipmentStatus.Booked,
            ["RECEIVED"] = ShipmentStatus.PickedUp,
            ["SORTED"] = ShipmentStatus.InTransit,
            ["ON_ROUTE"] = ShipmentStatus.InTransit,
            ["OUT_FOR_DELIVERY"] = ShipmentStatus.OutForDelivery,
            ["DELIVERED"] = ShipmentStatus.Delivered,
            ["PARCEL_SHOP"] = ShipmentStatus.ReadyForPickup,
            ["DELIVERY_FAILED"] = ShipmentStatus.DeliveryFailed,
            ["ON_HOLD"] = ShipmentStatus.OnHold,
            ["RETURNING"] = ShipmentStatus.Returned,
        };
}
