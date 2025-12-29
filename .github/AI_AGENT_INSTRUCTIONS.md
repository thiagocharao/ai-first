# Instructions for AI Agents (Copilot, Claude, Cursor, etc.)

## ⚠️ CRITICAL: Pre-Commit Validation

**NEVER commit or push code without running validation first.**

### Mandatory Pre-Commit Checklist

Before ANY `git commit`, you MUST:

1. **Run full validation:**
   ```bash
   ./scripts/validate-ci.sh
   ```

2. **Check for common issues:**
   - No build warnings
   - No test failures
   - No missing files referenced in .csproj
   - No XML doc warnings for samples/tests
   - Icon files exist and match references

3. **If validation fails:**
   - Fix ALL errors
   - Re-run validation
   - Only commit after validation passes

### Pre-Push Checklist

Before any `git push` or `git push --force`, you MUST:

1. **Ensure pre-commit validation passed**
2. **Verify changes are minimal and focused**
3. **Check commit message is descriptive**

### Common Issues to Catch

#### Missing Files
- ❌ `error NU5019: File not found: '/path/to/file'`
- ✅ Fix: Ensure file exists or remove reference from .csproj

#### XML Documentation Warnings
- ❌ `warning CS1591: Missing XML comment for publicly visible type`
- ✅ Fix: Tests and samples should have `<IsPackable>false</IsPackable>`
- ✅ Fix: Only libraries need `GenerateDocumentationFile`

#### Target Framework Mismatches
- ❌ Tests/samples target net6.0 but CI has net8.0
- ✅ Fix: Ensure tests/samples/CLI target `net8.0`
- ✅ Fix: Libraries multi-target `netstandard2.0;net6.0;net8.0`

#### Netstandard2.0 Compatibility
- ❌ `ValueTask` or `IsExternalInit` not found
- ✅ Fix: Add `System.Threading.Tasks.Extensions` package
- ✅ Fix: Include `IsExternalInit` polyfill

#### Build Artifacts
- ❌ Committing `bin/`, `obj/`, `.vs/` folders
- ✅ Fix: Ensure `.gitignore` is correct

## For any .csproj or code changes:
- Ensure tests target `net8.0` (not net6.0)
- Ensure samples target `net8.0` (not net6.0)
- Libraries can multi-target `netstandard2.0;net6.0;net8.0`
- Tests and samples must have `<IsPackable>false</IsPackable>`

### Validation Commands

```bash
# Full CI simulation (recommended)
./scripts/validate-ci.sh

# Manual validation
dotnet restore
dotnet build --configuration Release
dotnet test --configuration Release --no-build

# Check for warnings (should be 0)
dotnet build --configuration Release 2>&1 | grep -i warning

# Verify pack works
dotnet pack --configuration Release --no-build --output ./artifacts
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
