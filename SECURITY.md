# Security Policy

## Supported Versions

| Version | Supported          |
| ------- | ------------------ |
| 0.1.x   | :white_check_mark: |

## Reporting a Vulnerability

If you discover a security vulnerability in AIFirst.DotNet, please report it by:

1. **Email**: Send details to the repository owner (see GitHub profile)
2. **Private Report**: Use GitHub's "Report a vulnerability" feature (Security tab)

**Please do not open public issues for security vulnerabilities.**

### What to Include

- Description of the vulnerability
- Steps to reproduce
- Potential impact
- Suggested fix (if any)

### Response Timeline

- **Initial response**: Within 48 hours
- **Status update**: Within 1 week
- **Fix timeline**: Depends on severity (critical issues prioritized)

## Security Best Practices

When using AIFirst.DotNet:

- **Redact sensitive data** — Use policy pipeline to redact PII, tokens, secrets
- **Validate tool outputs** — Treat MCP tool results as untrusted input
- **Enable tracing carefully** — Traces may contain sensitive data; store securely
- **Allowlist tools** — Only permit approved tools in production environments

See `docs/threat-model.md` for detailed security considerations.
