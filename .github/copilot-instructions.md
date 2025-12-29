# Copilot Instructions

- Keep PRs small (≤400 LOC).
- Prefer records + immutable models.
- No reflection in hot paths; favor generated code.
- Add tests for schema parsing and generator output.
- Keep `AIFirst.Core` independent of any orchestrator.
- Tracing/logging must be opt-in and redact secrets by default.

## CI/CD Validation

**Before pushing ANY changes, ALWAYS validate locally:**

```bash
# Run this validation script
./scripts/validate-ci.sh

# Or manually:
dotnet restore
dotnet build --configuration Release
dotnet test --configuration Release --no-build
```

**If validation fails, DO NOT push.** Fix issues first.

## Multi-targeting

- Libraries: `netstandard2.0;net6.0;net8.0` for broad compatibility
- Tests/Samples/CLI: `net8.0` only (matches CI environment)
- CI uses .NET 8.0 SDK - ensure all executable projects target net8.0
