# Contributing to AIFirst.DotNet

Thank you for considering contributing to AIFirst.DotNet! This project is in early development.

## Development Status

🚧 **MVP in Progress** — The project is currently building out core functionality through Milestone 4. Contributions are welcome once Milestone 3 is complete.

## Getting Started

### Prerequisites

- .NET 8 SDK
- Git
- Familiarity with C#, Roslyn, and MCP concepts

### Setup

```bash
git clone https://github.com/thiagocharao/ai-first.git
cd ai-first
dotnet restore
dotnet build
dotnet test
```

## Coding Guidelines

See `.github/copilot-instructions.md` for code style preferences:

- Keep PRs small (≤400 LOC)
- Prefer records + immutable models
- No reflection in hot paths; favor generated code
- Add tests for schema parsing and generator output
- Keep `AIFirst.Core` independent of any orchestrator
- Tracing/logging must be opt-in and redact secrets by default

### Code Style

- Follow `.editorconfig` rules (enforced automatically)
- Add XML docs to all public APIs
- Build must produce 0 warnings
- All tests must pass

## Pull Request Process

1. **Fork** the repository
2. **Create a feature branch** (`git checkout -b feature/my-feature`)
3. **Make your changes** with clear, focused commits
4. **Run tests** (`dotnet test`)
5. **Ensure build succeeds** with no warnings
6. **Submit a PR** with a clear description

### PR Checklist

- [ ] Code follows style guidelines
- [ ] Tests added/updated for changes
- [ ] All tests passing
- [ ] Build produces 0 warnings
- [ ] XML docs added for public APIs
- [ ] CHANGELOG.md updated (if applicable)

## Issue Tracking

- Check existing issues before creating new ones
- Use issue templates when available
- Link PRs to related issues

## Questions?

Open a discussion or issue on GitHub.

## License

By contributing, you agree that your contributions will be licensed under the MIT License.
