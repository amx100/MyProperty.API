# Runbook - MyProperty.API

Operational runbook for day-to-day support and incident response.

## Service Ownership

- Service: `MyProperty.API`
- Type: ASP.NET Core Web API
- Dependencies: Database, auth/token configuration, external integrations (if configured)

## Start / Stop

Local:

```bash
 dotnet run --project MyProperty.API/MyProperty.API.csproj
```

Production start/stop depends on your host (service manager/container/orchestrator).

## Health Checks

Recommended:

- Liveness endpoint (process is running)
- Readiness endpoint (DB/connectivity checks)

If health endpoints are not yet implemented, prioritize adding them.

## Logs

Primary sources:

- Application logs (ASP.NET Core logger output)
- Hosting platform logs (runtime/proxy/container)

Common checks:

- Startup exceptions
- Database connection failures
- Authentication/token validation errors
- Unhandled exceptions in request pipeline

## Common Incidents

### 1) API fails on startup
- Verify required environment variables exist.
- Validate connection strings and credentials.
- Check for migration/schema mismatch.

### 2) 401/403 authorization issues
- Verify JWT signing keys/issuer/audience settings.
- Confirm token expiration and clock drift.
- Validate middleware ordering in startup pipeline.

### 3) Latency spikes/timeouts
- Check database response times and slow queries.
- Inspect thread pool saturation and CPU/memory limits.
- Review recent deployments for regressions.

## Backup & Recovery

- Ensure database backup policy is active.
- Test restore process periodically.
- Keep versioned deploy artifacts for rollback.

## Change Management

Before release:

- Build + tests pass.
- Migrations reviewed.
- Rollback plan prepared.

After release:

- Smoke test critical endpoints.
- Monitor error rate and latency.

## Security Operations

- Rotate secrets/tokens periodically.
- Use least-privilege for DB and infrastructure identities.
- Patch .NET runtime and dependencies regularly.
