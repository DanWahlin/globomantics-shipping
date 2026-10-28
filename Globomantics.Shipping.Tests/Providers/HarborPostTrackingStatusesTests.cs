using Globomantics.Shipping.Providers;
using Globomantics.Shipping.Providers.HarborPost;
using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Tests.Providers;

public class HarborPostTrackingStatusesTests
{
    private readonly HarborPostAdapter _adapter = new();

    [Theory]
    [InlineData("PRE_ADVICE", ShipmentStatus.Booked)]
    [InlineData("ACCEPTED", ShipmentStatus.PickedUp)]
    [InlineData("IN_TRANSIT", ShipmentStatus.InTransit)]
    [InlineData("ARRIVED_AT_DEPOT", ShipmentStatus.InTransit)]
    [InlineData("OUT_FOR_DELIVERY", ShipmentStatus.OutForDelivery)]
    [InlineData("DELIVERED", ShipmentStatus.Delivered)]
    [InlineData("DELIVERED_TO_PICKUP_POINT", ShipmentStatus.Delivered)]
    [InlineData("NOT_DELIVERED", ShipmentStatus.DeliveryFailed)]
    [InlineData("HELD", ShipmentStatus.OnHold)]
    [InlineData("RETURNED", ShipmentStatus.Returned)]
    public void MapsTrackingCode(string code, ShipmentStatus expected)
    {
        Assert.Equal(expected, _adapter.ToShipmentStatus(code));
    }

    [Fact]
    public void MapsEveryCodeInTheSpec()
    {
        Assert.Equal(10, _adapter.TrackingStatuses.Count);
    }
}
