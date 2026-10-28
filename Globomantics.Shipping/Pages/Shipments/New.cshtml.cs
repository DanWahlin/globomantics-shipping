using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Globomantics.Shipping.Providers;
using Globomantics.Shipping.Shipments;

namespace Globomantics.Shipping.Pages.Shipments;

public class NewModel(ShipmentStore store, ProviderDirectory providers) : PageModel
{
    [BindProperty]
    public BookingForm Form { get; set; } = new();

    [TempData]
    public string? Notice { get; set; }

    public IReadOnlyList<IProviderAdapter> Bookable => providers.All.Where(p => p.Services.Count > 0).ToList();
    public IReadOnlyList<IProviderAdapter> NotBookable => providers.All.Where(p => p.Services.Count == 0).ToList();

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        var (providerId, serviceCode) = SplitService(Form.Service);
        var provider = providers.Find(providerId);

        if (provider?.FindService(serviceCode) is null)
        {
            ModelState.AddModelError($"{nameof(Form)}.{nameof(Form.Service)}", "Choose a service from the list.");
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var recipient = new Recipient(Form.RecipientName.Trim(), Form.Street.Trim(), Form.PostalCode.Trim(), Form.City.Trim());
        var shipment = store.Book(provider!.ProviderId, serviceCode, recipient, Form.WeightKg);

        Notice = $"{shipment.Reference} is booked with {provider.DisplayName}.";
        return RedirectToPage("/Shipments/Details", new { reference = shipment.Reference });
    }

    public static string ServiceValue(IProviderAdapter provider, ServiceOption service) =>
        $"{provider.ProviderId}/{service.Code}";

    private static (string ProviderId, string ServiceCode) SplitService(string? value)
    {
        var parts = (value ?? "").Split('/', 2);
        return parts.Length == 2 ? (parts[0], parts[1]) : ("", "");
    }
}

public class BookingForm
{
    [Display(Name = "Recipient name")]
    [Required(ErrorMessage = "Enter the recipient's name.")]
    [StringLength(80)]
    public string RecipientName { get; set; } = "";

    [Display(Name = "Street address")]
    [Required(ErrorMessage = "Enter a street address.")]
    [StringLength(120)]
    public string Street { get; set; } = "";

    [Display(Name = "Postal code")]
    [Required(ErrorMessage = "Enter a postal code.")]
    [StringLength(12)]
    public string PostalCode { get; set; } = "";

    [Required(ErrorMessage = "Enter a city.")]
    [StringLength(60)]
    public string City { get; set; } = "";

    [Display(Name = "Weight (kg)")]
    [Range(0.1, 30, ErrorMessage = "Weight must be between 0.1 and 30 kg.")]
    public decimal WeightKg { get; set; } = 1.0m;

    [Required(ErrorMessage = "Choose a service.")]
    public string? Service { get; set; }
}
