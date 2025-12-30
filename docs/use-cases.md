# Use Cases

## When to Use AIFirst.DotNet

AIFirst.DotNet is designed for .NET developers building AI-powered applications that call external tools via MCP (Model Context Protocol). It's especially valuable when you need:

- **Compile-time safety** for tool calls
- **Governance and compliance** (audit trails, PII redaction)
- **Multi-tool orchestration** across MCP servers
- **Debugging capabilities** for AI agent behavior

## Real-World Scenarios

### 1. Enterprise AI Assistants

Building an internal AI assistant that queries databases, sends emails, and accesses internal APIs:

```csharp
// Without AIFirst: Runtime errors, no audit trail, potential PII leaks
var result = await llm.CallTool("send_emal", jsonArgs); // typo? schema wrong?

// With AIFirst: Compile-time safety, redaction, tracing
[Tool("email.send")]
public partial Task<EmailResult> SendEmailAsync(SendEmailRequest request);
// ^ Analyzer catches typos and schema mismatches at build time
// ^ Policy pipeline redacts PII before logging
// ^ Traces capture everything for compliance audits
```

### 2. Multi-Tool AI Agents

Orchestrating multiple MCP servers (filesystem, database, APIs) in a single agent:

```bash
# Pull tools from multiple servers
aifirst pull-tools npx @modelcontextprotocol/server-filesystem /data
aifirst pull-tools npx @modelcontextprotocol/server-postgres "postgres://..."
aifirst gen --namespace MyAgent.Tools
```

```csharp
// Generated typed tools - no JSON wrangling
[Tool("read_file")] 
public partial Task<FileContent> ReadFileAsync(ReadFileRequest request);

[Tool("query")]
public partial Task<QueryResult> QueryDatabaseAsync(QueryRequest request);
```

### 3. Regulated Industries

Healthcare, finance, and legal applications where compliance is critical:

| Requirement | AIFirst Solution |
|-------------|------------------|
| Only approved tools | Allowlist policy |
| No PII in logs | Redaction policy |
| Audit trail | Trace sink with replay |
| Security review | Build-time validation |

### 4. AI Coding Tools Development

Testing and developing MCP tools that IDEs like VS Code, Cursor, or Windsurf will consume:

```bash
# Test your MCP server with AIFirst CLI
aifirst pull-tools node ./my-server.js
aifirst gen --namespace MyServer.Tools

# Validate tool schemas compile correctly
dotnet build
```

## Comparison

| Scenario | Without AIFirst | With AIFirst |
|----------|-----------------|--------------|
| Typo in tool name | Runtime error after deployment | Build error ❌ |
| Schema mismatch | Silent failures, weird behavior | Build error ❌ |
| PII in logs | Compliance nightmare | Auto-redacted ✅ |
| "Why did AI do that?" | No visibility | Full trace replay ✅ |
| Tool sprawl | Any tool can be called | Allowlist enforced ✅ |
| Multi-server orchestration | Manual JSON wrangling | Typed methods ✅ |

## When NOT to Use AIFirst.DotNet

- **Simple scripts**: If you're just testing an MCP server once, use the raw SDK
- **Non-.NET projects**: AIFirst.DotNet is C#-specific
- **No governance needs**: If you don't need policies, tracing, or compile-time checks

## Testing with MCP Servers

### Official Reference Servers

These npm packages are great for testing:

| Server | Command | Description |
|--------|---------|-------------|
| Filesystem | `npx @modelcontextprotocol/server-filesystem /tmp` | File operations |
| Memory | `npx @modelcontextprotocol/server-memory` | Knowledge graph |
| Git | `npx @modelcontextprotocol/server-git` | Git operations |
| Fetch | `npx @modelcontextprotocol/server-fetch` | Web fetching |
| Time | `npx @modelcontextprotocol/server-time` | Time utilities |
| Everything | `npx @modelcontextprotocol/server-everything` | Demo server |

### Quick Start

```bash
# 1. Discover tools
aifirst pull-tools npx @modelcontextprotocol/server-filesystem /tmp

# 2. Generate DTOs
aifirst gen aifirst.tools.json --namespace MyApp.Tools --output Tools.cs

# 3. Use in code
await using var transport = new StdioMcpTransport("npx", "@modelcontextprotocol/server-filesystem", "/tmp");
await using var client = new McpClient(transport);

var tools = await client.ListToolsAsync();
var result = await client.CallToolAsync("read_file", new { path = "/tmp/test.txt" });
```

### MCP Inspector

For interactive debugging:

```bash
npx @modelcontextprotocol/inspector npx @modelcontextprotocol/server-filesystem /tmp
```

Opens a web UI to explore tools, test calls, and inspect responses.

## Resources

- [MCP Specification](https://modelcontextprotocol.io/)
- [Official MCP Servers](https://github.com/modelcontextprotocol/servers)
- [MCP Server Directory](https://www.mcplist.ai/) - 775+ community servers
