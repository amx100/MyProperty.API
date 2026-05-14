# Contributing - MyProperty.API

Thanks for contributing to `MyProperty.API`.

## Development Workflow

1. Fork or create a feature branch.
2. Make focused changes with clear intent.
3. Run build/tests locally.
4. Open a pull request with context and screenshots/logs where relevant.

## Branch Naming

Recommended patterns:

- `feature/<short-description>`
- `fix/<short-description>`
- `chore/<short-description>`

## Commit Messages

Use clear imperative messages, for example:

- `Add reservation filtering by date range`
- `Fix token validation for expired refresh tokens`

## Coding Guidelines

- Keep controllers lightweight.
- Place business rules in services/domain.
- Use interfaces from `Services.Abstractions` for dependency boundaries.
- Keep DTO/contracts explicit and version-safe.

## Validation Before PR

```bash
 dotnet restore
 dotnet build
 dotnet test
```

Also ensure:

- No secrets committed.
- Documentation updated for contract/endpoint changes.
- New dependencies justified.

## Pull Request Checklist

- [ ] Scope is focused and minimal.
- [ ] Build passes locally.
- [ ] Tests added/updated where needed.
- [ ] Backward compatibility considered.
- [ ] Docs updated.
