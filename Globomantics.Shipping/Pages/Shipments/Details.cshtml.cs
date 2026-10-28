using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Globomantics.Shipping.Providers;
using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Pages.Shipments;

public class DetailsModel(ShipmentStore store, ProviderDirectory providers) : PageModel
{
    public ShipmentOverview Overview { get; private set; } = null!;

    public IReadOnlyList<(TrackingEvent Event, ShipmentStatus Status)> Timeline { get; private set; } = [];

    [TempData]
    public string? Notice { get; set; }

    public IActionResult OnGet(string reference)
    {
        if (store.Find(reference) is not { } shipment)
        {
            return NotFound();
        }

        Overview = providers.Describe(shipment);
        Timeline = shipment.Events
            .OrderByDescending(e => e.OccurredAt)
            .Select(e => (e, Overview.Provider.ToShipmentStatus(e.StatusCode)))
            .ToList();

        return Page();
    }
}
