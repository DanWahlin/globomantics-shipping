using Globomantics.Shipping.Providers;
using Globomantics.Shipping.Providers.GreyvallCouriers;
using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Tests.Providers;

public class GreyvallCouriersServiceCodesTests
{
    private readonly GreyvallCouriersAdapter _adapter = new();

    [Theory]
    [InlineData("GRV-SAVER", "Greyvall Saver", ServiceLevel.Economy, 4)]
    [InlineData("GRV-STD", "Greyvall Standard", ServiceLevel.Standard, 2)]
    [InlineData("GRV-PRIO", "Greyvall Priority", ServiceLevel.Express, 1)]
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
