using Globomantics.Shipping.Providers;
using Globomantics.Shipping.Providers.HavsgrenLines;
using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Tests.Providers;

public class HavsgrenLinesTrackingStatusesTests
{
    private readonly HavsgrenLinesAdapter _adapter = new();

    [Theory]
    [InlineData("MANIFESTED", ShipmentStatus.Booked)]
    [InlineData("COLLECTED", ShipmentStatus.PickedUp)]
    [InlineData("AT_HUB", ShipmentStatus.InTransit)]
    [InlineData("IN_LINEHAUL", ShipmentStatus.InTransit)]
    [InlineData("OFD", ShipmentStatus.OutForDelivery)]
    [InlineData("DELIVERED", ShipmentStatus.Delivered)]
    [InlineData("DELIVERED_NEIGHBOUR", ShipmentStatus.Delivered)]
    [InlineData("ATTEMPT_FAILED", ShipmentStatus.DeliveryFailed)]
    [InlineData("ADDRESS_QUERY", ShipmentStatus.OnHold)]
    [InlineData("CUSTOMS_HOLD", ShipmentStatus.OnHold)]
    [InlineData("RETURNED_TO_SENDER", ShipmentStatus.Returned)]
    [InlineData("IN_LOCKER", ShipmentStatus.Delivered)]
    public void MapsTrackingCode(string code, ShipmentStatus expected)
    {
        Assert.Equal(expected, _adapter.ToShipmentStatus(code));
    }

    [Fact]
    public void MapsEveryCodeInTheSpec()
    {
        Assert.Equal(12, _adapter.TrackingStatuses.Count);
    }
}
