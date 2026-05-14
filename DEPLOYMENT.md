# Deployment Guide - MyProperty.API

This document describes generic deployment paths for `MyProperty.API` without tying the project to a specific cloud provider.

## Build Artifact

Use publish output as deployment artifact:

```bash
 dotnet publish MyProperty.API/MyProperty.API.csproj -c Release -o ./publish/api
```

Deploy contents of `./publish/api` to your target runtime.

## Environment Configuration

Set configuration through environment variables or secure secret stores:

- `ASPNETCORE_ENVIRONMENT`
- `ConnectionStrings__<Name>`
- `Jwt__*` / auth-related keys
- External service URLs/keys

Do not store production secrets in `appsettings.json`.

## Reverse Proxy / TLS

For production behind a reverse proxy:

- Enable HTTPS termination at edge/proxy.
- Forward headers correctly (`X-Forwarded-*`).
- Restrict HTTP and enforce TLS.

## Runtime Recommendations

- Use Release builds only.
- Configure structured logging sink where possible.
- Add health endpoints for liveness/readiness.
- Enable graceful shutdown timeouts.

## Containerized Deployment (Optional)

If deploying with containers:

1. Build image from a Dockerfile (if/when added).
2. Provide config through env vars/secrets.
3. Expose service port and map ingress routes.
4. Configure restart policies and resource limits.

## Database Migration Strategy

- Apply schema migrations as part of release pipeline (pre-start or migration job).
- Maintain backward compatibility during rolling deployments.
- Backup before major schema changes.

## Post-Deployment Checklist

- API boots with expected environment.
- Health endpoint returns success.
- OpenAPI endpoint reachable (if enabled externally).
- Authenticated endpoints validate tokens as expected.
- Critical business flows smoke-tested.

## Rollback Strategy

- Keep previous deploy artifact/image.
- Roll back to last known healthy version.
- Verify schema compatibility before rollback.
