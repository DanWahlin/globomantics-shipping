---
name: implement-mapping
description: Implement or update a delivery provider's service code or tracking status mapping in Globomantics Shipping, from the provider's spec. Use this when someone asks to implement, add, or fix a provider mapping.
---

## Implement a provider mapping

If you don't know which mapping (service codes or tracking statuses), which provider, or which API version this is about, ask before you start.

1. Open the provider's folder under `Globomantics.Shipping/Providers/` and the matching mapping file.
2.  Get the spec for that version from the provider catalog. If you can't reach it, stop and say so. Don't guess codes.
3. Add every code from the spec, following the adapter conventions in .github/copilot-instructions.md.
4. Update or create the matching test file in Globomantics.Shipping.Tests/Providers/, with one test case per code and a count test.
5. Run the tests and fix any failures.
6. Summarize which codes you mapped and anything in the spec you weren't sure how to map.