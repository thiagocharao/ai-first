# NuGet Publishing

## Packages

- `AIFirst.DotNet` (meta package)
- `AIFirst.Core`
- `AIFirst.Mcp`
- `AIFirst.Roslyn`
- `AIFirst.Cli` (tool)

## Feeds

- **NuGet.org**: Primary public feed for stable releases.
- **GitHub Packages**: Good for prerelease/nightly builds.
- **Azure Artifacts**: Enterprise/private hosting for internal consumers.
- **Cloudsmith**: Managed multi-feed option with access control.

## Suggested release flow

1. Publish `-alpha` or `-beta` builds to GitHub Packages.
2. Publish stable releases to NuGet.org.
3. Mirror or promote to Azure Artifacts/Cloudsmith as needed.

## Sample `nuget.config`

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
    <add key="github" value="https://nuget.pkg.github.com/OWNER/index.json" />
    <add key="azure" value="https://pkgs.dev.azure.com/ORG/_packaging/FEED/nuget/v3/index.json" />
    <add key="cloudsmith" value="https://nuget.cloudsmith.io/ORG/REPO/v3/index.json" />
  </packageSources>
</configuration>
```
