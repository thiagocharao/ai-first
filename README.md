# AIFirst.DotNet

[![Build and Test](https://github.com/thiagocharao/aifirst-dotnet/actions/workflows/build.yml/badge.svg)](https://github.com/thiagocharao/aifirst-dotnet/actions/workflows/build.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/)

AIFirst.DotNet is an AI-first .NET SDK that makes MCP tool-calling feel like native C# with compile-time safety, policy enforcement, and observability.

## The Problem

When building AI agents that call external tools, you face:

| Problem | Impact |
|---------|--------|
| Typo in tool name | Runtime crash in production |
| Schema mismatch | Silent failures, weird behavior |
| PII in logs | Compliance violations |
| "Why did AI do that?" | Impossible to debug |

## The Solution

AIFirst.DotNet gives you typed tool calls with build-time validation:

```csharp
// ❌ Without AIFirst: Runtime errors, no safety
var result = await llm.CallTool("send_emal", jsonArgs); // typo discovered in prod

// ✅ With AIFirst: Compile-time safety
[Tool("email.send")]
public partial Task<EmailResult> SendEmailAsync(SendEmailRequest request);
// ^ Build fails if tool doesn't exist or schema is wrong
```

**See [Use Cases](docs/use-cases.md) for detailed scenarios and when to use AIFirst.DotNet.**

## Quick Start

```bash
# 1. Discover tools from an MCP server
aifirst pull-tools npx @modelcontextprotocol/server-filesystem /tmp

# 2. Generate C# DTOs
aifirst gen aifirst.tools.json --namespace MyApp.Tools

# 3. Use typed tools in your code
```

```csharp
await using var transport = new StdioMcpTransport("npx", "@modelcontextprotocol/server-filesystem", "/tmp");
await using var client = new McpClient(transport);

var tools = await client.ListToolsAsync();
var result = await client.CallToolAsync("read_file", new { path = "/tmp/test.txt" });
```

## Features

| Feature | Status | Description |
|---------|--------|-------------|
| MCP Client | ✅ | Connect to any MCP server via stdio |
| Schema Parser | ✅ | Parse JSON Schema from tool definitions |
| Code Generator | ✅ | Generate C# records from schemas |
| CLI Tool | ✅ | `pull-tools` and `gen` commands |
| Source Generator | 🚧 | `[Tool]` attribute with build-time validation |
| Policy Pipeline | 📋 | Allowlists, redaction, output checks |
| Tracing | 📋 | Capture and replay tool calls |

## Testing with MCP Servers

AIFirst.DotNet works with any MCP-compliant server. Try these official ones:

```bash
# Filesystem operations
aifirst pull-tools npx @modelcontextprotocol/server-filesystem /tmp

# Memory/knowledge graph
aifirst pull-tools npx @modelcontextprotocol/server-memory

# Git operations  
aifirst pull-tools npx @modelcontextprotocol/server-git
```

See [MCP Server Directory](https://www.mcplist.ai/) for 775+ community servers.

## Packages

| Package | Description |
|---------|-------------|
| `AIFirst.DotNet` | Meta package (includes all below) |
| `AIFirst.Core` | Core abstractions, schema parser, code generator |
| `AIFirst.Mcp` | MCP client and transports |
| `AIFirst.Roslyn` | Source generator and analyzers |
| `AIFirst.Cli` | CLI tool (`aifirst`) |

## Build

```bash
dotnet restore
dotnet build
dotnet test
```

Requires .NET 8 SDK.

## Roadmap

Track development progress and upcoming features on the [Issues page](https://github.com/thiagocharao/aifirst-dotnet/issues).

**Completed:**
- ✅ M0: Build foundation
- ✅ M1: MCP client with stdio transport
- ✅ M2: JSON Schema parser and DTO code generator

**In Progress:**
- 🚧 M3: Source generator and analyzers

**Planned:**
- M4: Policy pipeline and tracing
- M5: Documentation and release

## Documentation

- [Use Cases](docs/use-cases.md) - When and why to use AIFirst.DotNet
- [Design](docs/design.md) - Architecture and components
- [Threat Model](docs/threat-model.md) - Security considerations

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

## License

MIT License - see [LICENSE](LICENSE) for details.
