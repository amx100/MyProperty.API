# Architecture - MyProperty.API

## High-Level Context

`MyProperty.API` is the backend service layer of the MyProperty platform. It exposes HTTP endpoints and coordinates domain/business operations through a structured multi-project solution.

## Layered Components

## 1) API Host (`MyProperty.API`)
- Application startup and middleware pipeline.
- DI container setup and service registration.
- Swagger/OpenAPI setup.
- Environment-aware configuration loading.

## 2) Presentation Layer (`Presentation`)
- Controllers and request handling.
- Input validation and HTTP response mapping.
- No heavy business logic.

## 3) Application Layer (`Services`, `Services.Abstractions`)
- Use-case orchestration.
- Interface-driven service design.
- Transaction/business workflow boundaries.

## 4) Domain Layer (`Domain`)
- Core entities and business rules.
- Framework-independent domain modeling.

## 5) Infrastructure Layer (`Persistence`)
- Data access implementation.
- Repository/data context integration.
- External storage concerns.

## 6) Shared Contracts (`Contract`)
- DTOs and messages crossing layer boundaries.
- Shared abstractions for API communication.

## Request Lifecycle (Conceptual)

1. Client sends HTTP request to endpoint.
2. Controller/action resolves required service abstraction.
3. Service executes business use case and coordinates domain/persistence.
4. Data is loaded/saved through infrastructure components.
5. Response contract/DTO is returned to caller.

## Cross-Cutting Concerns

- **Configuration:** `appsettings*.json` + environment variables.
- **Observability:** Logging via ASP.NET Core logging providers.
- **Security:** Authentication/authorization middleware and token settings (if enabled in startup/services).
- **API Discoverability:** Swagger/OpenAPI for endpoint exploration.

## Architectural Principles

- Separation of concerns across projects.
- Dependency inversion through abstractions.
- Thin transport layer; rich application/domain logic.
- Contracts explicitly represent integration boundaries.

## Suggested Future Enhancements

- Add architecture tests to enforce layer boundaries.
- Add centralized exception handling middleware.
- Add health checks/readiness probes.
- Add integration tests for critical flows.
