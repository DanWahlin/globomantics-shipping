using Globomantics.Shipping.Providers;
using Globomantics.Shipping.Providers.GreyvallCouriers;
using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Tests.Providers;

public class GreyvallCouriersTrackingStatusesTests
{
    private readonly GreyvallCouriersAdapter _adapter = new();

    [Theory]
    [InlineData("REGISTERED", ShipmentStatus.Booked)]
    [InlineData("RECEIVED", ShipmentStatus.PickedUp)]
    [InlineData("SORTED", ShipmentStatus.InTransit)]
    [InlineData("ON_ROUTE", ShipmentStatus.InTransit)]
    [InlineData("OUT_FOR_DELIVERY", ShipmentStatus.OutForDelivery)]
    [InlineData("DELIVERED", ShipmentStatus.Delivered)]
    [InlineData("PARCEL_SHOP", ShipmentStatus.ReadyForPickup)]
    [InlineData("DELIVERY_FAILED", ShipmentStatus.DeliveryFailed)]
    [InlineData("ON_HOLD", ShipmentStatus.OnHold)]
    [InlineData("RETURNING", ShipmentStatus.Returned)]
    public void MapsTrackingCode(string code, ShipmentStatus expected)
    {
        Assert.Equal(expected, _adapter.ToShipmentStatus(code));
    }

    [Fact]
    public void MapsEveryCodeInTheSpec()
    {
        Assert.Equal(10, _adapter.TrackingStatuses.Count);
    }

    [Fact]
    public void LeavesUnmappedCodesUnknown()
    {
        Assert.Equal(ShipmentStatus.Unknown, _adapter.ToShipmentStatus("NOT_A_REAL_CODE"));
    }

    [Fact]
    public void ReadyForPickupUsesOutForDeliveryTone()
    {
        Assert.Equal("Ready for pickup", ShipmentStatus.ReadyForPickup.DisplayName());
        Assert.Equal(ShipmentStatus.OutForDelivery.Tone(), ShipmentStatus.ReadyForPickup.Tone());
    }
}
