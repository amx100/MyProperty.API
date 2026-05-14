# MyProperty.API

Backend API for the MyProperty platform built with ASP.NET Core and a layered architecture.

## Overview

`MyProperty.API` provides REST endpoints for core business capabilities such as authentication, property management, reservations, and user/account operations. The solution is split into multiple projects to enforce separation of concerns.

## Solution Structure

- `MyProperty.API/` – API host (startup, middleware, dependency injection, OpenAPI/Swagger, configuration).
- `Presentation/` – API presentation layer (controllers, request/response surface, web concerns).
- `Services/` – Application/service layer with business logic orchestration.
- `Services.Abstractions/` – Service contracts/interfaces used across layers.
- `Persistence/` – Data access and infrastructure implementation.
- `Domain/` – Domain entities, value objects, and core domain rules.
- `Contract/` – Shared DTO/contracts used between boundaries.
- `MyProperty/` – Additional shared/core project used by the solution.

## Architecture (Layered)

The project follows a clean layered flow:

1. **Presentation** receives HTTP requests.
2. **Services** validate and execute business use cases.
3. **Persistence** reads/writes data stores.
4. **Domain** models business data and rules.
5. **Contract** defines data exchange contracts.

### Dependency Direction

Outer layers depend on abstractions from inner/application contracts. Business rules should stay isolated from transport/infrastructure details.

## Technology Stack

- .NET (ASP.NET Core)
- Swagger / OpenAPI
- Dependency Injection via built-in ASP.NET Core container
- JSON configuration (`appsettings.json`, environment overrides)

## Getting Started

### Prerequisites

- .NET SDK (version required by `MyProperty.API/MyProperty.API.csproj`)
- A configured database / infrastructure expected by `Persistence`

### Run locally

```bash
# from repository root
 dotnet restore
 dotnet build
 dotnet run --project MyProperty.API/MyProperty.API.csproj
```

API will start on the URLs configured by launch settings and environment.

### OpenAPI

Swagger is configured in startup. After running, open:

- `https://localhost:<port>/swagger`

## Configuration

Configuration files:

- `MyProperty.API/appsettings.json`
- `MyProperty.API/appsettings.Development.json`

Typical configuration areas include:

- Logging levels
- Connection strings
- JWT/auth settings
- External service endpoints

> Keep secrets outside source control (environment variables, secret manager, CI/CD secrets).

## API Design Notes

- Use consistent resource naming and HTTP verbs.
- Keep controllers thin; business logic belongs in services.
- Prefer DTOs/contracts for public API boundaries.
- Return meaningful status codes and validation responses.

## Build & Quality

```bash
 dotnet format
 dotnet build
 dotnet test
```

(If test projects are added/available in the solution.)

## Repository Conventions

- One logical change per pull request.
- Keep domain logic out of controllers/infrastructure.
- Register new dependencies in startup/DI composition root.
- Update docs when endpoints/contracts change.

## Related Repository

Frontend client lives in: `amx100/MyProperty.UI`
