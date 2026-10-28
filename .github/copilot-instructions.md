# Globomantics Shipping

Globomantics Shipping is an ASP.NET Core Razor Pages app (.NET 10) for booking and tracking parcels across several delivery providers. Shipments are kept in memory and reset when the app restarts. There is no database.

## Build and test

- Build: `dotnet build`
- Test: `dotnet test`
- Run: `dotnet run --project Globomantics.Shipping` (http://localhost:5080)

Always run `dotnet test` before you finish a change.

## Project layout

- `Globomantics.Shipping/Providers/` holds one folder per delivery provider.
- `Globomantics.Shipping/Shipments/` holds the shipment model, Globomantics Shipping's own statuses and service levels, and the in-memory store.
- `Globomantics.Shipping/Pages/` holds the Razor Pages UI. Styles live in `Globomantics.Shipping/wwwroot/css/site.css`.
- `Globomantics.Shipping.Tests/` mirrors the app's folder structure.

## Provider adapter conventions

Each provider has three files in `Globomantics.Shipping/Providers/<Provider>/`:

- `<Provider>Adapter.cs` implements `IProviderAdapter` and is registered in `Program.cs`.
- `<Provider>ServiceCodes.cs` lists the services we can book, as `ServiceOption` records.
- `<Provider>TrackingStatuses.cs` maps the provider's tracking codes to a `ShipmentStatus`.

When you change a mapping:

- Take codes from the provider's integration spec in the provider catalog and copy them exactly. Never guess or invent a code.
- Use the `ServiceLevel` and `ShipmentStatus` values that already exist. Ask before adding new ones.
- Tracking status dictionaries use `StringComparer.OrdinalIgnoreCase`.
- Never map a code to `ShipmentStatus.Unknown`. Unknown is what Globomantics Shipping shows for codes that aren't mapped.
- Each mapping file has a test file next to its siblings in `Globomantics.Shipping.Tests/Providers/`, named `<Provider>ServiceCodesTests.cs` or `<Provider>TrackingStatusesTests.cs`. Add one `InlineData` row per code, and a test that checks the total count matches the spec.
- Keep the service code and tracking status changes in separate files so they can be reviewed and merged independently.

## Code style

- File-scoped namespaces, primary constructors, and collection expressions.
- Keep the UI copy short, plain, and in sentence case.
