# Instructions for AI Agents (Copilot, Claude, Cursor, etc.)

## Critical Rule: ALWAYS Validate Before Push

**NEVER push code without running local validation first.**

### Pre-Push Checklist

Before any `git push` or `git push --force`, you MUST:

1. **Run validation script:**
   ```bash
   ./scripts/validate-ci.sh
   ```

2. **If validation fails:**
   - Fix the errors
   - Re-run validation
   - Only push after validation passes

3. **For any .csproj or code changes:**
   - Ensure tests target `net8.0` (not net6.0)
   - Ensure samples target `net8.0` (not net6.0)
   - Libraries can multi-target `netstandard2.0;net6.0;net8.0`

### Common CI Failures

- **"Framework 'Microsoft.NETCore.App', version '6.0.0' not found"**
  - Fix: Update test/sample projects to `<TargetFramework>net8.0</TargetFramework>`
  
- **"ValueTask not found" or "IsExternalInit" errors for netstandard2.0**
  - Fix: Ensure `System.Threading.Tasks.Extensions` package reference
  - Fix: Ensure `IsExternalInit` polyfill is included

- **Build succeeds but tests fail**
  - Fix: Run `dotnet restore` after any project file changes
  - Fix: Ensure `--no-build` isn't used without building first

### Validation Commands

```bash
# Full CI simulation
dotnet restore
dotnet build --configuration Release
dotnet test --configuration Release --no-build

# Quick validation (Debug)
dotnet build
dotnet test --no-build
```

## Code Standards

- Keep PRs small (≤400 LOC)
- Prefer records + immutable models
- No reflection in hot paths
- Add XML docs to public APIs
- Build with 0 warnings
- All tests must pass
- Keep `AIFirst.Core` independent

## Multi-Targeting

- **Libraries** (Core, Mcp, Roslyn): `netstandard2.0;net6.0;net8.0`
- **Executables** (CLI, samples, tests): `net8.0` only
- CI environment runs .NET 8 SDK

## Security

- Redact secrets by default
- Tracing is opt-in
- Validate tool outputs (treat as untrusted)
- Use allowlists for production tools
