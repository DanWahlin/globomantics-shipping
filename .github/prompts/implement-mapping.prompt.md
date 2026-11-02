---
description: Implement a provider's service code or tracking status mapping from its spec
agent: agent
model: Claude Sonnet 4.5
tools: ['search', 'edit', 'execute/runTests']
---

Implement the ${input:mapping:service codes or tracking statuses} mapping for the ${input:provider:provider name} provider, using version ${input:apiVersion:API version} of its spec.

1. Open the provider's folder under `Globomantics.Shipping/Providers/` and the matching mapping file.
2. Use the spec details I've given you. If I haven't given you the spec, stop and ask for it. Don't guess codes.
3. Add every code from the spec to the mapping file, following the adapter conventions in the repository instructions.
4. Update or create the matching test file in `Globomantics.Shipping.Tests/Providers/` with one `InlineData` row per code and a count test.
5. Run the tests and fix any failures.
6. Summarize which codes you mapped and anything in the spec you weren't sure how to map.
