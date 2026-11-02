---
name: scope-provider
description: Scope a new delivery provider for Globomantics Shipping. Use this when someone asks to scope, plan, or break down onboarding a provider, or to prepare provider issues for the cloud agent.
---

# Scope a delivery provider

1. If the provider name or the API version is missing, ask for it. Don't guess a version.
2. Look up the provider and the spec for that version in the provider catalog. If you can't reach it, stop and say so.
3. Follow the provider adapter conventions in .github/copilot-instructions.md.
4. Map a code only when its description clearly matches one Globomantics Shipping status or service level. If the spec says a status isn't final, don't map it to Delivered or Returned. If a code could match more than one, or none, leave it unmapped and list it under Open questions with the options.
5. Write the breakdown in exactly this format:

## <Provider> (API version <version>)

### Task 1: Adapter and service codes
- Files: a new Globomantics.Shipping/Providers/<Provider>/ folder with the adapter, the service codes and an empty tracking status mapping, the adapter registered in Program.cs, and the service codes test file
- Codes: a table with code, name, transit days and Globomantics Shipping service level
- Done when: dotnet test passes, with one test case per code and a count test

### Task 2: Tracking statuses
- Starts after Task 1 is merged
- Files: Globomantics.Shipping/Providers/<Provider>/<Provider>TrackingStatuses.cs and its test file
- Codes: a table with code, description and Globomantics Shipping status
- Done when: dotnet test passes, with one test case per code and a count test

