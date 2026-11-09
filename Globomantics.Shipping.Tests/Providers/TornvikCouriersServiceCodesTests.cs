using Globomantics.Shipping.Providers;
using Globomantics.Shipping.Providers.TornvikCouriers;
using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Tests.Providers;

public class TornvikCouriersServiceCodesTests
{
    private readonly TornvikCouriersAdapter _adapter = new();

    [Theory]
    [InlineData("TVK-ECO", "Tornvik Economy", ServiceLevel.Economy, 5)]
    [InlineData("TVK-STD", "Tornvik Standard", ServiceLevel.Standard, 2)]
    [InlineData("TVK-EXP", "Tornvik Express", ServiceLevel.Express, 1)]
    public void MapsServiceCode(string code, string name, ServiceLevel level, int transitDays)
    {
        var service = _adapter.FindService(code);

        Assert.Equal(new ServiceOption(code, name, level, transitDays), service);
    }

    [Fact]
    public void OffersExactlyThreeServices()
    {
        Assert.Equal(3, _adapter.Services.Count);
    }
}
