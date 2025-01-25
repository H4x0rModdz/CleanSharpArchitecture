# CleanSharpArchitecture

## Why Use CleanSharpArchitecture?

If you're looking for a robust, scalable, and modern architecture template for your next .NET project, CleanSharpArchitecture is the ultimate choice. Here's why:

- 🚀 **Scalability**: Clean architecture principles ensure your application can grow without turning into spaghetti code.
- 🔧 **Pre-configured Goodness**: Out-of-the-box support for Authentication, Azure Blob Storage, structured logging, and more.
- 🛠️ **Developer Productivity**: Simplified layering and reusable patterns speed up your development process.
- 🔒 **Security First**: Built-in token authentication and password hashing using BCrypt ensure your application is secure from day one.
- 📈 **Performance Monitoring**: Integrated OpenTelemetry and health checks help you stay on top of your app's performance.
- 🧩 **Extensibility**: Designed with flexibility in mind, so you can plug in additional features or services as needed.
- 🧪 **Testing Made Easy**: Comprehensive testing setup with XUnit, Moq, and Coverlet to catch bugs before they reach production.

> With CleanSharpArchitecture, you'll spend less time setting up your project and more time building features your users will love!

---

## Summary

[Why Use CleanSharpArchitecture?](#why-use-cleansharparchitecture)

[Layers Overview](#layers-overview)
- [1. CleanSharpArchitecture](#1-cleansharparchitecture)
- [2. CleanSharpArchitecture.AppHost](#2-cleansharparchitectureapphost)
- [3. CleanSharpArchitecture.Application](#3-cleansharparchitectureapplication)
- [4. CleanSharpArchitecture.BackgroundTasks](#4-cleansharparchitecturebackgroundtasks)
- [5. CleanSharpArchitecture.CrossCutting](#5-cleansharparchitecturecrosscutting)
- [6. CleanSharpArchitecture.Domain](#6-cleansharparchitecturedomain)
- [7. CleanSharpArchitecture.Infrastructure](#7-cleansharparchitectureinfrastructure)
- [8. CleanSharpArchitecture.Messaging](#8-cleansharparchitecturemessaging)
- [9. CleanSharpArchitecture.Monitoring](#9-cleansharparchitecturemonitoring)
- [10. CleanSharpArchitecture.Presentation](#10-cleansharparchitecturepresentation)
- [11. CleanSharpArchitecture.ServiceDefaults](#11-cleansharparchitectureservicedefaults)
- [12. CleanSharpArchitecture.Testing](#12-cleansharparchitecturetesting)

[Key Features](#key-features)

[Code Example: Authentication Flow](#code-example-authentication-flow)

[Technologies Used](#technologies-used)

[Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Running the Application](#running-the-application)

[Contributing](#contributing)

[License](#license)

---

## Layers Overview

### 1. **CleanSharpArchitecture**
This is the main entry point of the application. It contains:
- **Controllers**: Defines API endpoints that expose application functionalities to clients.
- **appsettings.json**: Stores configuration settings, such as connection strings and app-specific configurations.
- **Program.cs**: Configures Dependency Injection (DI), middleware, and initializes the application. It acts as the startup file.

### 2. **CleanSharpArchitecture.AppHost**
- Generated using the **"Enlist in .NET Aspire orchestration"** option.
- Contains `Program.cs` and `appsettings.json`, similar to the main layer but customized for orchestration scenarios.
- **Aspire**: A library designed to streamline API lifecycle management, including orchestration, dependency management, and hosting configurations.
- **Purpose**: This layer enables advanced hosting scenarios and integration with distributed systems.

### 3. **CleanSharpArchitecture.Application**
Defines the core business logic, including:
- **DTOs**: Data Transfer Objects used to pass data between layers or APIs.
- **Exceptions**: Custom exceptions for domain-specific error handling.
- **Interfaces**: Contracts for service abstractions, ensuring decoupled implementations.
- **Mappings**: Profiles for converting entities to DTOs and vice versa using tools like AutoMapper.
- **UseCases**: Encapsulates specific business operations and workflows.
- **Validations**: Business rule validation logic, often implemented using FluentValidation.
- **Purpose**: Centralizes the business logic, separating it from infrastructure and presentation concerns.

### 4. **CleanSharpArchitecture.BackgroundTasks**
Handles background processes via Hosted Services. This includes:
- **HostedServices**: Implements tasks that run periodically or continuously in the background, such as processing queues or scheduled tasks.
- **Purpose**: Enables asynchronous processing for better scalability and performance.

### 5. **CleanSharpArchitecture.CrossCutting**
Centralized utilities shared across layers, such as:
- **Caching**: Provides caching strategies for performance optimization.
- **ExceptionHandling**: Manages global error handling.
- **Logging**: Centralized logging configuration, often using Serilog.
- **Security**: Shared security-related utilities, such as token validation or encryption.
- **Purpose**: Promotes reusability and consistency across the application.

### 6. **CleanSharpArchitecture.Domain**
Represents the core of the application, focusing on:
- **Entities**: Core business objects (e.g., User, Product).
- **Enums**: Enumerations for domain concepts (e.g., UserRole).
- **Exceptions**: Domain-specific errors that encapsulate business rules.
- **ValueObjects**: Immutable objects that represent domain-specific concepts (e.g., Money, Address).
- **Purpose**: Maintains the integrity and logic of the domain.

### 7. **CleanSharpArchitecture.Infrastructure**
Implements data and external service integrations, such as:
- **Caching**: Implementations of caching mechanisms (e.g., Redis).
- **Configurations**: Configuration classes for external services or settings.
- **Data**: Database context and setup using EF Core.
- **Logging**: Implementation of structured logging.
- **Migrations**: Database schema migration scripts.
- **Repositories**: Data access layer for CRUD operations.
- **Services**: Implementations of service interfaces defined in the application layer.
- **Purpose**: Provides implementations for dependencies required by the application layer.

### 8. **CleanSharpArchitecture.Messaging**
Handles asynchronous communication and event-driven architectures with:
- **EventHandlers**: Listens to domain or integration events and processes them.
- **Purpose**: Enables loosely coupled communication between components.

### 9. **CleanSharpArchitecture.Monitoring**
Ensures application reliability and performance with:
- **HealthChecks**: Monitors the health of application dependencies.
- **Telemetry**: Tracks application performance and metrics using OpenTelemetry.
- **Purpose**: Facilitates observability and proactive issue detection.

### 10. **CleanSharpArchitecture.Presentation**
Customizes how data is presented and processed, including:
- **ExceptionHandlers**: Global exception handling for consistent API responses.
- **Filters**: Custom filters for request/response manipulation or validation.
- **Views**: Handles rendering and data formatting.
- **Purpose**: Defines how the application interacts with clients.

### 11. **CleanSharpArchitecture.ServiceDefaults**
- Generated by Aspire, containing default service configurations.
- **Extensions.cs**: Provides reusable service extension methods for dependency injection and configurations.
- **Purpose**: Simplifies and centralizes service configuration.

### 12. **CleanSharpArchitecture.Testing**
Houses unit and integration tests, using:
- **XUnit**: Test framework for unit testing.
- **Moq**: Mocking library for dependencies.
- **Coverlet**: Tool for code coverage.
- **FluentAssertions**: Provides human-readable assertions for tests.
- **Purpose**: Ensures code reliability and prevents regressions.

---

## Key Features
- **Authentication**: Pre-configured authentication system with JWT Bearer tokens.
- **Azure Blob Integration**: Streamlined blob storage management.
- **Modern Development Practices**:
  - OpenTelemetry for tracing and performance insights.
  - Serilog for structured logging.
  - KubernetesClient for container orchestration.
  - Polly for resilience and retry policies.

---

## Code Example: Authentication Flow

### `BaseEntity`
```csharp
using System;

namespace CleanSharpArchitecture.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
```

### `User`
```csharp
namespace CleanSharpArchitecture.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Biography { get; set; }
    }
}

```

### `AuthController`
```csharp
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) => _authService = authService;

    [HttpPost("register")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Register([FromForm] RegisterDto registerUserDto)
    {
        var result = await _authService.RegisterUserAsync(registerUserDto);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _authService.LoginUserAsync(loginDto);
        return Ok(result);
    }
}
```

### `AuthService`
```csharp
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly BlobService _blobService;

    public AuthService(IUserRepository userRepository, ITokenService tokenService, BlobService blobService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _blobService = blobService;
    }

    public async Task<RegisterResultDto> RegisterUserAsync(RegisterDto registerDto)
    {
        // Implementation...
    }

    public async Task<LoginResultDto> LoginUserAsync(LoginDto loginDto)
    {
        // Implementation...
    }
}
```

---

## Technologies Used
- **C# & .NET 8**: Framework for building applications.
- **Aspire 8.2**: API orchestration.
- **Azure Blob Storage**: File storage.
- **BCrypt**: Password hashing.
- **EF Core & SQL Server**: Database management.
- **Identity**: User authentication.
- **XUnit, Moq, Coverlet**: Testing suite.
- **OpenTelemetry, Serilog**: Monitoring and logging.
- **GRPC, KubernetesClient, Polly**: Modern cloud integration tools.

---

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server
- Azure Storage Account

### Installation
1. Clone the repository:
   ```bash
   git clone <repo_url>
   ```
2. Navigate to the project directory and restore dependencies:
   ```bash
   cd CleanSharpArchitecture
   dotnet restore
   ```
3. Update the `appsettings.json` with your database and blob storage configurations.
4. Run migrations:
   ```bash
   dotnet ef database update
   ```


### Running the Application
  To run the API locally:
  ```bash
  dotnet run
  ```

---

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

---

## License
This project is licensed under the MIT License - see the LICENSE file for details.

