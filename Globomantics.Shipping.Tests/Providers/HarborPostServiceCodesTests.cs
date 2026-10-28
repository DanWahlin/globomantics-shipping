using Globomantics.Shipping.Providers;
using Globomantics.Shipping.Providers.HarborPost;
using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Tests.Providers;

public class HarborPostServiceCodesTests
{
    private readonly HarborPostAdapter _adapter = new();

    [Theory]
    [InlineData("HP-ECO", "Harbor Post Economy", ServiceLevel.Economy, 4)]
    [InlineData("HP-PRI", "Harbor Post Priority", ServiceLevel.Standard, 2)]
    [InlineData("HP-EXP", "Harbor Post Express", ServiceLevel.Express, 1)]
    public void MapsServiceCode(string code, string name, ServiceLevel level, int transitDays)
    {
        var service = _adapter.FindService(code);

        Assert.Equal(new ServiceOption(code, name, level, transitDays), service);
    }

    [Fact]
    public void OffersOnlyTheServicesInTheSpec()
    {
        Assert.Equal(["HP-ECO", "HP-PRI", "HP-EXP"], _adapter.Services.Select(s => s.Code));
    }
}
