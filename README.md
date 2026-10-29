# Globomantics Shipping

Globomantics Shipping is a small shipping app used in the Pluralsight course **Advanced GitHub Copilot**. It books parcels with delivery providers and shows where each one is.

Every provider speaks its own language. Harbor Post says `OUT_FOR_DELIVERY`, Havsgren Lines says `OFD`. Globomantics Shipping translates each provider's service and tracking codes into its own statuses through a small adapter per provider. The team is now onboarding a second provider, Havsgren Lines, and that work is the backlog you'll hand to GitHub Copilot in the course.

## What you need

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio Code](https://code.visualstudio.com) with the C# Dev Kit extension
- [GitHub CLI](https://cli.github.com), signed in with `gh auth login` (only for the setup step)

## Run it

```bash
dotnet run --project Globomantics.Shipping
```

Then open http://localhost:5080. Shipments live in memory, so restarting the app resets the sample data.

## Run the tests

```bash
dotnet test
```

## Set up your copy for the course

1. Create your own copy of this repository on GitHub with **Use this template** (or fork it), and clone it.
2. From the root of your clone, create the starter issues:

   ```powershell
   powershell -ExecutionPolicy Bypass -File ./setup/create-issues.ps1
   ```

   The `-ExecutionPolicy Bypass` part lets Windows run the script once without changing your PowerShell settings.

   On macOS or Linux you can use `./setup/create-issues.sh` instead (it needs `jq`).

The script adds two issues, **Add Havsgren Lines service codes** and **Map Havsgren Lines tracking statuses**, plus a couple of labels. It turns on issues if your copy has them switched off, and you can run it again safely: issues that already exist are skipped.

## How it's organized

```
Globomantics.Shipping/
  Providers/          one folder per delivery provider
    HarborPost/       adapter, service codes, tracking statuses
    HavsgrenLines/     being onboarded, nothing mapped yet
  Shipments/          shipments, statuses, and the in-memory store
  Pages/              the Razor Pages UI
Globomantics.Shipping.Tests/    tests, mirroring the app's folders
.github/              Copilot instructions, prompt file, and workflows
setup/                the script that creates the course issues
```

The Providers page in the app shows every code Globomantics Shipping understands, and lists any tracking codes it has received but can't translate yet.

## Disclaimer

This demo uses fictional names and people. No affiliation with or endorsement by any real company.
