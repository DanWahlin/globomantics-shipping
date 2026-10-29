using Globomantics.Shipping.Providers;
using Globomantics.Shipping.Providers.HavsgrenLines;
using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Tests.Providers;

public class HavsgrenLinesServiceCodesTests
{
    private readonly HavsgrenLinesAdapter _adapter = new();

    [Theory]
    [InlineData("HVL-ECO", "Havsgren Economy", ServiceLevel.Economy, 5)]
    [InlineData("HVL-STD", "Havsgren Standard", ServiceLevel.Standard, 2)]
    [InlineData("HVL-NXT", "Havsgren Next Day", ServiceLevel.Express, 1)]
    [InlineData("HVL-SDY", "Havsgren Same Day", ServiceLevel.SameDay, 0)]
    public void MapsServiceCode(string code, string name, ServiceLevel level, int transitDays)
    {
        var service = _adapter.FindService(code);

        Assert.Equal(new ServiceOption(code, name, level, transitDays), service);
    }

    [Fact]
    public void OffersExactlyFourServices()
    {
        Assert.Equal(4, _adapter.Services.Count);
    }
}
