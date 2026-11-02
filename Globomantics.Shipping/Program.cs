using Globomantics.Shipping.Providers;
using Globomantics.Shipping.Providers.GreyvallCouriers;
using Globomantics.Shipping.Providers.HarborPost;
using Globomantics.Shipping.Providers.HavsgrenLines;
using Globomantics.Shipping.Shipments;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSingleton(TimeProvider.System);

builder.Services.AddSingleton<IProviderAdapter, HarborPostAdapter>();
builder.Services.AddSingleton<IProviderAdapter, HavsgrenLinesAdapter>();
builder.Services.AddSingleton<IProviderAdapter, GreyvallCouriersAdapter>();
builder.Services.AddSingleton<ProviderDirectory>();
builder.Services.AddSingleton<ShipmentStore>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseRequestLocalization("en-GB");
app.UseRouting();

app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();
