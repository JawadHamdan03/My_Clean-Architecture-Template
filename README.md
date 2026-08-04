# Clean Architecture Template (CLA)

A comprehensive **.NET 10** clean architecture template demonstrating modern best practices for building scalable, maintainable, and testable applications.

## 📋 Overview

This repository serves as a production-ready template implementing the clean architecture principles, designed to showcase how to structure a robust .NET application with clear separation of concerns, dependency injection, CQRS pattern, JWT authentication, and real-time communication.

### Key Features

✅ **Clean Architecture** - Organized in 4 distinct layers  
✅ **CQRS Pattern** - Command Query Responsibility Segregation with MediatR  
✅ **JWT Authentication** - Secure token-based authentication with refresh tokens  
✅ **Real-time Communication** - SignalR hub for live chat functionality  
✅ **Validation** - FluentValidation for robust input validation  
✅ **Entity Framework Core** - ORM with SQLite database and migrations  
✅ **OpenAPI/Swagger** - Interactive API documentation with Scalar UI  
✅ **Global Exception Handling** - Centralized error management  
✅ **Authorization** - Role-based access control (RBAC)  
✅ **.NET 10** - Latest framework features and performance improvements

---

## 🏗️ Architecture Overview

The solution is organized into four distinct projects following clean architecture principles:

### 1. **cla.Domain** (Core Layer)
The innermost layer containing business logic and domain-driven entities.

**Key Components:**
- `Entities/` - Domain entities representing core business objects
  - `User.cs` - User entity with authentication properties
  - `Product.cs` - Product entity for e-commerce functionality
  - `RefreshToken.cs` - Token management for JWT refresh flow
  - `ChatMessage.cs` - Real-time messaging entity

**Responsibilities:**
- Define domain entities and business rules
- Contains only interfaces and abstractions
- No dependencies on external libraries
- Pure domain logic

### 2. **cla.Application** (Use Cases Layer)
Implements application-specific business rules and orchestrates domain logic.

**Key Components:**
- `Common/`
  - `Abstractions/` - Service interfaces
	- `IJwtTokenServiceProvider` - JWT token generation contract
  - `IAppDbContext.cs` - Database context abstraction
  - `Exceptions/` - Custom application exceptions
  - `Behaviors/` - MediatR pipeline behaviors
	- `ValidationBehavior.cs` - Automatic request validation
- `Features/` - Feature-organized CQRS operations
  - **Accounts**
	- `RegisterCommand/` - User registration
	- `LoginCommand/` - User authentication
	- `RefreshTokenCommand/` - Token refresh flow
	- `Responses/` - DTOs for API responses
  - **Products**
	- `CreateProduct/` - Product creation command
	- `GetProducts/` - Product retrieval query
	- `Requests/` - API request models
	- `Responses/` - Product response DTOs

**Responsibilities:**
- Implement use cases through commands and queries
- Validate incoming requests
- Orchestrate domain logic
- Define application-level abstractions
- No direct reference to UI or presentation layer

### 3. **cla.Infrastructure** (External Interfaces Layer)
Implements technical concerns and external dependencies.

**Key Components:**
- `Data/`
  - `AppDbContext.cs` - Entity Framework context
  - `Configurations/` - Entity configurations for EF
  - `Migrations/` - Database schema versions
  - `SeedData.cs` - Initial data seeding
- `Common/Implementation/`
  - `JwtTokenServiceProvider.cs` - JWT token generation
- `Hubs/`
  - `ChatHub.cs` - SignalR hub for real-time chat

**Responsibilities:**
- Implement repository patterns
- Database configuration and migrations
- External service implementations (JWT, SignalR)
- Data access abstractions

### 4. **cla.API** (Presentation Layer)
Handles HTTP requests/responses and serves as the application's entry point.

**Key Components:**
- `Controllers/` - REST API endpoints
  - `AccountController.cs` - Authentication endpoints
  - `ProductsController.cs` - Product management endpoints
- `Requests/` - API request models
- `Exceptions/`
  - `GlobalExceptionHandler.cs` - Centralized error handling
- `Program.cs` - Application configuration and startup
- `appsettings.json` - Configuration files

**Responsibilities:**
- Handle HTTP requests/responses
- Route requests to appropriate handlers
- Return formatted API responses
- Global exception handling

---

## 🔐 Security Architecture

### Authentication & Authorization

This template implements a complete JWT-based authentication system:

```
User Registration → Hashed Password Stored → JWT Token Issued
										   → Refresh Token Stored
										   ↓
			   Login → Credentials Validated → New JWT Issued
											 → Claims-based Authorization
```

**Key Security Features:**
- **JWT Tokens** - Stateless authentication with expiration
- **Refresh Tokens** - Secure token refresh mechanism
- **Role-Based Access Control** - Customers, Admin roles
- **Password Hashing** - Secure password storage
- **Token Validation** - Issuer, Audience, and Signature verification
- **CORS Configuration** - Frontend integration security

---

## 📐 CQRS Pattern

This template implements Command Query Responsibility Segregation using MediatR:

### Commands (State-Changing Operations)
```
RegisterCommand        → Handled by RegisterCommandHandler
LoginCommand          → Handled by LoginCommandHandler  
RefreshTokenCommand   → Handled by RefreshTokenCommandHandler
CreateProductCommand  → Handled by CreateProductCommandHandler
```

### Queries (Read-Only Operations)
```
GetProductsQuery     → Handled by GetProductsQueryHandler
```

### Validation Pipeline
All commands/queries pass through `ValidationBehavior`:
- Automatic FluentValidation integration
- Centralized validation error handling
- Consistent error responses

---

## 🗄️ Database Schema

The application uses **SQLite** with Entity Framework Core for data persistence.

### Entities

**Users Table**
```
- Id (Guid, Primary Key)
- Name (string, indexed)
- Password (string, hashed)
- Role (enum: Admin, Customer)
- CreatedAt (DateTime)
```

**Products Table**
```
- Id (Guid, Primary Key)
- Name (string)
- Description (string)
- Price (decimal)
- CreatedAt (DateTime)
```

**RefreshTokens Table**
```
- Id (Guid, Primary Key)
- UserId (Guid, Foreign Key)
- Token (string, unique)
- ExpiryDate (DateTime)
```

**ChatMessages Table**
```
- Id (Guid, Primary Key)
- SenderId (Guid)
- Content (string)
- CreatedAt (DateTime)
```

---

## 🚀 Getting Started

### Prerequisites
- **.NET 10 SDK** or later
- Visual Studio 2026 (Community, Professional, or Enterprise)
- SQLite (included with EF Core)

### Installation

1. **Clone the repository:**
```bash
git clone https://github.com/JawadHamdan03/My_Clean-Architecture-Template.git
cd cla
```

2. **Restore dependencies:**
```bash
dotnet restore
```

3. **Build the solution:**
```bash
dotnet build
```

4. **Run the application:**
```bash
cd src/cla.API
dotnet run
```

The API will start at: `https://localhost:7173` (or another available port)

### Database Setup

Migrations are automatically applied on startup:
- `Program.cs` calls `dbContext.Database.MigrateAsync()`
- Initial data is seeded via `SeedData.SeedUserData()`
- Database file: `src/cla.API/app.db`

---

## 📡 API Endpoints

### Authentication
```
POST   /api/accounts/register          - Register new user
POST   /api/accounts/login             - Login and get JWT token
POST   /api/accounts/refresh-token     - Refresh expired JWT
```

### Products (Requires Authentication & Customer Role)
```
GET    /api/products                   - Get all products
POST   /api/products                   - Create new product
```

### Interactive API Documentation
- **OpenAPI/Swagger UI**: Navigate to `https://localhost:7173/scalar/v1`
- **OpenAPI JSON**: `https://localhost:7173/openapi/v1.json`

---

## 🔌 Dependencies

### Core Framework
- **Microsoft.AspNetCore.Authentication.JwtBearer** `10.0.10` - JWT authentication
- **Microsoft.EntityFrameworkCore.Sqlite** `10.0.10` - SQLite data access
- **Microsoft.EntityFrameworkCore.Design** `10.0.10` - EF Core tooling

### Application Patterns
- **MediatR** `14.2.0` - CQRS pattern implementation
- **FluentValidation.DependencyInjectionExtensions** `12.1.1` - Input validation
- **Mapster** `10.0.11` - Automatic object mapping

### API Documentation
- **Microsoft.AspNetCore.OpenApi** `10.0.10` - OpenAPI support
- **Scalar.AspNetCore** `2.16.16` - Alternative Swagger UI

---

## 🧪 Testing

The template uses:
- FluentValidation for request validation
- Global exception handling for consistent error responses
- `.http` files for manual endpoint testing (see `cla.API.http`)

To add unit/integration tests:
```bash
dotnet new xunit -n cla.Tests
dotnet add cla.Tests reference src/cla.Application/cla.Application.csproj
```

---

## ⚙️ Configuration

### JWT Settings (`appsettings.json`)
```json
{
  "JwtSettings": {
	"SecretKey": "your-256-bit-secret-key-min-32-chars",
	"Issuer": "cla-api",
	"Audience": "cla-client",
	"ExpiryMinutes": 15
  }
}
```

### CORS Configuration
Frontend URL: `http://127.0.0.1:5500`

### Database Connection
SQLite: `Data Source = app.db` (local file-based)

---

## 📝 Project Structure

```
cla/
├── src/
│   ├── cla.Domain/           # Entity definitions, core business logic
│   │   └── Entities/
│   ├── cla.Application/       # Use cases, CQRS commands/queries
│   │   ├── Features/
│   │   ├── Common/
│   │   └── IAssemblyMarker.cs # Assembly reference for DI
│   ├── cla.Infrastructure/    # Data access, external services
│   │   ├── Data/
│   │   └── Hubs/
│   └── cla.API/               # REST endpoints, middleware
│       ├── Controllers/
│       └── Program.cs
├── cla.sln                    # Solution file
└── README.md                  # This file
```

---

## 🔄 Flow Diagrams

### User Registration & Login Flow
```
User Request
	↓
RegisterCommand / LoginCommand
	↓
Validator (FluentValidation)
	↓
Handler
	↓
Domain Logic (Authentication)
	↓
Database (EF Core)
	↓
JWT Token Generated
	↓
Response to Client
```

### Product Management Flow
```
CreateProductCommand / GetProductsQuery
	↓
ValidationBehavior
	↓
Handler
	↓
AppDbContext
	↓
SQLite Database
	↓
Response (DTO)
```

---

## 🤝 Contributing

This is a template repository. To use it as a starting point:

1. **Use as Template**: Click "Use this template" on GitHub
2. **Customize**: Adapt entities, features, and business logic to your needs
3. **Extend**: Add new features following the CQRS pattern
4. **Test**: Add unit and integration tests as needed

---

## 📚 Best Practices Implemented

| Practice | Implementation |
|----------|-----------------|
| Single Responsibility | Each layer has a single purpose |
| Open/Closed Principle | Services use abstractions (interfaces) |
| Dependency Inversion | DI Container manages dependencies |
| CQRS Pattern | Clear separation of commands and queries |
| Validation | Centralized via FluentValidation behavior |
| Error Handling | Global exception handler middleware |
| Security | JWT authentication with role-based authorization |
| Testability | Abstractions enable easy mocking |
| Documentation | OpenAPI/Swagger integration |
| Scalability | Layered architecture supports growth |

---

## 🐛 Troubleshooting

### Database Migration Errors
```bash
# Remove existing database
rm src/cla.API/app.db

# Regenerate from migrations
cd src/cla.API
dotnet ef database update
```

### Port Already in Use
Edit `launchSettings.json` to change the port:
```json
"applicationUrl": "https://localhost:7174;http://localhost:5174"
```

### JWT Token Expired
Use the refresh token endpoint to obtain a new JWT token.

---

## 📖 Learning Resources

- [Clean Architecture by Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [MediatR Documentation](https://github.com/jbogard/MediatR)
- [Entity Framework Core Docs](https://learn.microsoft.com/en-us/ef/core/)
- [JWT Best Practices](https://tools.ietf.org/html/rfc8725)
- [FluentValidation Guide](https://docs.fluentvalidation.net/)

---

## 📄 License

This project is provided as a template for educational and commercial purposes.

---

## ✨ Support

For issues, questions, or suggestions:
- Open an issue on GitHub
- Check existing documentation
- Review code examples in the template

---

**Built with ❤️ using .NET 10 and Clean Architecture Principles**
