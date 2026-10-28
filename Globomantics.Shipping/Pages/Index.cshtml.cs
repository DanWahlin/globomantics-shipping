using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Globomantics.Shipping.Providers;
using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Pages;

public class IndexModel(ShipmentStore store, ProviderDirectory providers, TimeProvider time) : PageModel
{
    public static IReadOnlyList<(string Key, string Label)> Views { get; } =
    [
        ("all", "All"),
        ("in-progress", "In progress"),
        ("attention", "Needs attention"),
        ("delivered", "Delivered"),
    ];

    [BindProperty(SupportsGet = true)]
    public string Show { get; set; } = "all";

    [BindProperty(SupportsGet = true)]
    public string? Provider { get; set; }

    public IReadOnlyList<IProviderAdapter> Providers => providers.All;
    public IReadOnlyList<ShipmentOverview> Shipments { get; private set; } = [];
    public Dictionary<string, int> Counts { get; } = [];
    public DateTimeOffset Now { get; private set; }

    public void OnGet()
    {
        Now = time.GetUtcNow();

        var forProvider = store.All()
            .Where(s => string.IsNullOrEmpty(Provider) || s.ProviderId == Provider)
            .Select(providers.Describe)
            .ToList();

        foreach (var (key, _) in Views)
        {
            Counts[key] = forProvider.Count(s => Matches(key, s.Status));
        }

        Shipments = forProvider
            .Where(s => Matches(Show, s.Status))
            .OrderByDescending(s => s.Shipment.LastActivity)
            .ToList();
    }

    private static bool Matches(string view, ShipmentStatus status) => view switch
    {
        "in-progress" => status is not ShipmentStatus.Delivered && !status.NeedsAttention(),
        "attention" => status.NeedsAttention(),
        "delivered" => status is ShipmentStatus.Delivered,
        _ => true,
    };
}
