using Globomantics.Shipping.Providers;
using Globomantics.Shipping.Providers.HarborPost;
using Globomantics.Shipping.Providers.HavsgrenLines;
using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Tests.Shipments;

public class ShipmentStoreTests
{
    private static readonly DateTimeOffset Now = new(2026, 10, 9, 12, 0, 0, TimeSpan.Zero);

    private readonly ShipmentStore _store = new(new FixedTimeProvider(Now));
    private readonly ProviderDirectory _providers = new([new HarborPostAdapter(), new HavsgrenLinesAdapter()]);

    [Fact]
    public void BookingGivesTheShipmentTheNextReference()
    {
        var highest = _store.All().Max(s => s.Reference);

        var shipment = _store.Book("harbor-post", "HP-EXP", new("Daniel Robinson", "Exempelgatan 1", "111 22", "Stockholm"), 1.5m);

        Assert.True(string.CompareOrdinal(shipment.Reference, highest) > 0);
        Assert.Same(shipment, _store.Find(shipment.Reference));
    }

    [Fact]
    public void NewlyBookedShipmentIsBooked()
    {
        var shipment = _store.Book("harbor-post", "HP-ECO", new("Daniel Robinson", "Exempelgatan 1", "111 22", "Stockholm"), 2m);

        var overview = _providers.Describe(shipment);

        Assert.Equal(ShipmentStatus.Booked, overview.Status);
        Assert.Equal(Now, shipment.BookedAt);
    }

    [Fact]
    public void StatusComesFromTheLatestTrackingEvent()
    {
        var shipment = _store.Find("GS-104201")!;

        Assert.Equal(ShipmentStatus.Delivered, _providers.Describe(shipment).Status);
    }

    [Fact]
    public void EverySampleShipmentHasARegisteredProvider()
    {
        Assert.All(_store.All(), s => Assert.NotNull(_providers.Find(s.ProviderId)));
    }

    private sealed class FixedTimeProvider(DateTimeOffset now) : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() => now;
    }
}
