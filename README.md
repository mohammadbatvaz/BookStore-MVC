# BookStore-MVC

This repository is a sample ASP.NET Core MVC project that demonstrates a layered architecture (Domain, Infrastructure, Services, Presentation) for a simple bookstore application.

## Table of contents

- Description
- Features
- Project structure
- Prerequisites
- Database configuration
- Setup and run (PowerShell)
- Migrations
- Notes and common scenarios
- Contribution
- License

## Description

BookStore-MVC is an educational/example project intended to show a clean separation of concerns for a bookstore web app. Key technologies and patterns used:

- Entity Framework Core for ORM and migrations
- Repository and Service patterns for data and business logic separation
- ASP.NET Core MVC for the presentation layer

## Features

- Manage books and categories
- Basic user management and authentication scaffolding
- DTOs and ViewModels to separate domain and UI models
- EF Core migrations and database-first initialization scripts

## Project structure (brief)

- `Domain/` — Entities, DTOs, enums and domain models
- `Infrastructure/` — `AppDbContext`, EF Core configurations, repositories, and migrations
- `Services/` — Business logic and service implementations
- `Presentation/` — MVC project: controllers, views, configuration and application startup

Important files/folders:

- `Infrastructure/Migrations/` — EF Core migrations
- `Presentation/Program.cs` — service registrations and `AppDbContext` setup
- `Presentation/appsettings.json` — optional configuration file

## Prerequisites

- .NET SDK 7.0 or newer
- SQL Server / SQL Server Express (project currently configured for SQL Server Express)
- (Optional) `dotnet-ef` CLI tool for creating and applying migrations

## Database configuration

`AppDbContext` is registered in `Presentation/Program.cs` to use SQL Server. The current default connection string (hard-coded in `Program.cs`) is:

```csharp
options.UseSqlServer(@"Server=MBATVAZ-PC\SQLEXPRESS;Database=W17_BookStore_DB;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;")
```

Before running the app, update this connection string to match your environment. It is recommended to move the connection string to `appsettings.json` and read it with `Configuration.GetConnectionString("DefaultConnection")`.

If you prefer another provider (SQLite, PostgreSQL, etc.), change the `DbContext` registration in `Presentation/Program.cs` and add the appropriate EF Core provider NuGet package.

## Setup and run (PowerShell)

1. From the repository root, restore and build:

```powershell
dotnet restore
dotnet build
```

2. (Optional) Install `dotnet-ef` if you plan to add or apply migrations:

```powershell
dotnet tool install --global dotnet-ef
```

3. Apply migrations to create or update the database (the migrations live in the `Infrastructure` project and the startup project is `Presentation`):

```powershell
dotnet ef database update --project Infrastructure --startup-project Presentation
```

4. Run the web app:

```powershell
dotnet run --project Presentation
```

The application will typically be available at `https://localhost:5001` or the port specified in `Presentation/Properties/launchSettings.json`.

## Migrations

- Existing migrations are in `Infrastructure/Migrations`.
- To add a new migration after model changes:

```powershell
dotnet ef migrations add <MigrationName> --project Infrastructure --startup-project Presentation
dotnet ef database update --project Infrastructure --startup-project Presentation
```

## Notes and common scenarios

- If you do not have SQL Server available locally, you can switch to SQLite for local development: change the `UseSqlServer` call in `Program.cs` to `UseSqlite` and install `Microsoft.EntityFrameworkCore.Sqlite`.
- Moving the connection string to `appsettings.json` and using `IConfiguration` is recommended for easier deployment and environment-specific overrides.

## Tests

This repository does not include a dedicated test project. Adding unit tests (xUnit/NUnit) for services and repositories is recommended.

## Contributing

1. Open an issue to discuss changes or file bugs.
2. Fork the repository and create a feature branch.
3. Make changes, add tests if applicable, and submit a Pull Request for review.

## License

No license file is included in the repository. If you want to make the project public, add a `LICENSE` (for example MIT) to clarify usage rights.

---

Note: The project currently uses a SQL Server Express connection string in `Presentation/Program.cs`. Update it to match your environment before running the app.
