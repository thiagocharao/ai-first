# Copilot Instructions

## Pre-Commit Validation (MANDATORY)

**Before ANY commit or push, ALWAYS run:**
```bash
./scripts/validate-ci.sh
```

**If validation fails, DO NOT commit. Fix issues first.**

## Code Standards

- Keep PRs small (≤400 LOC).
- Prefer records + immutable models.
- No reflection in hot paths; favor generated code.
- Add tests for schema parsing and generator output.
- Keep `AIFirst.Core` independent of any orchestrator.
- Tracing/logging must be opt-in and redact secrets by default.
- Build with 0 warnings (no XML doc warnings for tests/samples).
- All tests must pass.

## CI/CD Validation

**Validation script catches:**
- Build failures
- Test failures
- Missing files (icons, READMEs)
- XML documentation warnings
- Target framework mismatches
- Package issues

## Multi-targeting

- **Libraries** (Core, Mcp, Roslyn, DotNet): `netstandard2.0;net6.0;net8.0` for broad compatibility
- **Tests/Samples/CLI**: `net8.0` only (matches CI environment)
- **Tests/Samples**: Must have `<IsPackable>false</IsPackable>`
- CI uses .NET 8.0 SDK - ensure all executable projects target net8.0

## Common Pre-Commit Checks

1. No missing files referenced in .csproj
2. No build warnings
3. All tests pass
4. XML docs only for libraries (not tests/samples)
5. Correct target frameworks
6. Icon files exist and are PNG format
