# Security Policy

## Supported Versions

| Version | Supported |
|---------|-----------|
| 1.0.x   | ✅        |

## Reporting a Vulnerability

If you discover a security vulnerability, please do not open a public issue.

Contact: aigerim.10803@gmail.com

## Security Practices

- All secrets are stored in environment variables, not in source code
- `appsettings.Development.json` is excluded from repository via `.gitignore`
- Local development secrets managed via `dotnet user-secrets`
- Azure App Service configuration is used for production secrets
- Passwords are hashed using ASP.NET Core Identity (PBKDF2)
- HTTPS enforced in production
- Anti-forgery tokens on all POST requests
- Optimistic locking prevents concurrent data corruption
- Blocked users cannot send messages or perform write operations