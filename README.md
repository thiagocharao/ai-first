# AIFirst.DotNet

[![Build and Test](https://github.com/thiagocharao/aifirst-dotnet/actions/workflows/build.yml/badge.svg)](https://github.com/thiagocharao/aifirst-dotnet/actions/workflows/build.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/)

AIFirst.DotNet is an AI-first .NET SDK that makes MCP tool-calling feel like native C# with compile-time safety, policy enforcement, and observability. The MVP uses an attribute-based DSL so tool contracts become strongly-typed methods with analyzer support.

## Why AIFirst.DotNet?

If you're building AI-powered applications in .NET, you've likely hit these problems:

- 🔴 **Runtime tool failures** — typos in tool names, schema mismatches
- 🔴 **Security concerns** — PII leakage, prompt injection via tool outputs
- 🔴 **Debugging nightmares** — "Why did the agent do that?"
- 🔴 **Governance gaps** — no allowlists, no audit trails

AIFirst.DotNet solves these with:

✅ **Compile-time safety** — tool calls are strongly-typed, validated at build time  
✅ **Policy enforcement** — allowlists, redaction, and output checks built-in  
✅ **Observability** — trace every prompt and tool call for debugging and compliance  
✅ **MCP-native** — leverage the emerging standard for tool interoperability

## Goals

- **Typed tools** generated from MCP schemas
- **Compile-time validation** for tool names and argument shapes
- **Policy pipeline** for allowlists, redaction, and safety checks
- **Tracing + replay** for observability and debugging
- **Broad compatibility** via `netstandard2.0`, `net6.0`, and `net8.0` targets

## Repo layout

```
/src
  /AIFirst.Core          # Core abstractions
  /AIFirst.Mcp           # MCP client
  /AIFirst.Roslyn        # Source generator + analyzer
  /AIFirst.Cli           # CLI tool (aifirst)
  /AIFirst.DotNet        # Meta package (Core + Mcp + Roslyn)
/samples
  /HelloMcp              # Basic MCP connectivity
  /TypedToolsDemo        # Type-safe tool calls
  /PolicyAndTracingDemo  # Governance and observability
/tests
  /AIFirst.Core.Tests
  /AIFirst.Mcp.Tests
  /AIFirst.Roslyn.Tests
  /AIFirst.Cli.Tests
/docs
  design.md              # Architecture
  threat-model.md        # Security considerations
```

## Attribute DSL

```csharp
[Tool("weather.getForecast")]
public static partial Task<Forecast> GetForecastAsync(ForecastRequest request);
```

The source generator will emit MCP calls for these tool methods.

## Build

```bash
dotnet restore
dotnet build
dotnet test
```

## Packages and feeds

**Packages**
- `AIFirst.DotNet` (meta package)
- `AIFirst.Core`
- `AIFirst.Mcp`
- `AIFirst.Roslyn`
- `AIFirst.Cli` (tool)

**Feeds**
- NuGet.org for stable releases
- GitHub Packages for prerelease/nightly builds
- Azure Artifacts for enterprise/private feeds
- Cloudsmith for a managed multi-feed option

See `docs/nuget.md` for feed setup and release guidance.

## Current Status

🚧 **MVP in Progress** — Milestone 0 (Build Foundation) complete

See [CHANGELOG.md](CHANGELOG.md) for details and repository issues for implementation progress.

## Contributing

This project is in early development. Contributions welcome once Milestone 3 is complete.

## License

MIT License - see [LICENSE](LICENSE) for details.
