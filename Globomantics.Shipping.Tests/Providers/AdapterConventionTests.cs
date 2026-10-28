using Globomantics.Shipping.Providers;
using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Tests.Providers;

/// <summary>
/// Rules every provider adapter must follow. New adapters are picked up automatically.
/// </summary>
public class AdapterConventionTests
{
    public static TheoryData<string> AdapterNames()
    {
        var names = new TheoryData<string>();
        foreach (var adapter in AllAdapters())
        {
            names.Add(adapter.GetType().Name);
        }

        return names;
    }

    [Fact]
    public void ProviderIdsAreUnique()
    {
        var ids = AllAdapters().Select(a => a.ProviderId).ToList();

        Assert.Equal(ids.Count, ids.Distinct(StringComparer.OrdinalIgnoreCase).Count());
    }

    [Fact]
    public void ServiceCodesAreUniqueAcrossProviders()
    {
        var codes = AllAdapters().SelectMany(a => a.Services).Select(s => s.Code).ToList();

        Assert.Equal(codes.Count, codes.Distinct(StringComparer.OrdinalIgnoreCase).Count());
    }

    [Theory]
    [MemberData(nameof(AdapterNames))]
    public void TrackingCodesMatchRegardlessOfCase(string adapterName)
    {
        var adapter = Adapter(adapterName);

        foreach (var (code, status) in adapter.TrackingStatuses)
        {
            Assert.Equal(status, adapter.ToShipmentStatus(code.ToLowerInvariant()));
        }
    }

    [Theory]
    [MemberData(nameof(AdapterNames))]
    public void UnrecognisedTrackingCodeIsUnknown(string adapterName)
    {
        Assert.Equal(ShipmentStatus.Unknown, Adapter(adapterName).ToShipmentStatus("NOT_A_REAL_CODE"));
    }

    [Theory]
    [MemberData(nameof(AdapterNames))]
    public void NoTrackingCodeMapsToUnknown(string adapterName)
    {
        Assert.DoesNotContain(ShipmentStatus.Unknown, Adapter(adapterName).TrackingStatuses.Values);
    }

    private static IProviderAdapter Adapter(string name) =>
        AllAdapters().Single(a => a.GetType().Name == name);

    private static IEnumerable<IProviderAdapter> AllAdapters() =>
        typeof(IProviderAdapter).Assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && t.IsAssignableTo(typeof(IProviderAdapter)))
            .OrderBy(t => t.Name)
            .Select(t => (IProviderAdapter)Activator.CreateInstance(t)!);
}
