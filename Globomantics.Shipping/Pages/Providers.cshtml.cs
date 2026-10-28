using Microsoft.AspNetCore.Mvc.RazorPages;
using Globomantics.Shipping.Providers;
using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Pages;

public class ProvidersModel(ProviderDirectory providers, ShipmentStore store) : PageModel
{
    public IReadOnlyList<ProviderSummary> Summaries { get; private set; } = [];

    public void OnGet()
    {
        var shipments = store.All();

        Summaries = providers.All
            .Select(provider =>
            {
                var theirs = shipments.Where(s => s.ProviderId == provider.ProviderId).ToList();

                var unrecognisedCodes = theirs
                    .SelectMany(s => s.Events)
                    .Select(e => e.StatusCode)
                    .Where(code => provider.ToShipmentStatus(code) is ShipmentStatus.Unknown)
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .Order()
                    .ToList();

                return new ProviderSummary(provider, theirs.Count, unrecognisedCodes);
            })
            .ToList();
    }
}

public sealed record ProviderSummary(IProviderAdapter Provider, int ShipmentCount, IReadOnlyList<string> UnrecognisedCodes);
