# BookStore-MVC

This repository is a sample ASP.NET Core MVC project that demonstrates a layered architecture (Domain, Infrastructure, Services, Presentation) for a simple bookstore application.

## Description

BookStore-MVC is an educational/example project intended to show a clean separation of concerns for a bookstore web app. Key technologies and patterns used:

- Entity Framework Core for ORM and migrations
- Repository and Service patterns for data and business logic separation
- ASP.NET Core MVC for the presentation layer

## Features

- Manage books and categories
- Basic user management and authentication scaffolding
- DTOs and ViewModels to separate domain and UI models


## Project structure (brief)

- `Domain/` — Entities, DTOs, enums and domain models
- `Infrastructure/` — `AppDbContext`, EF Core configurations, repositories, and migrations
- `Services/` — Business logic and service implementations
- `Presentation/` — MVC project: controllers, views, configuration and application startup

Important files/folders:

- `Infrastructure/Migrations/` — EF Core migrations
- `Presentation/Program.cs` — service registrations and `AppDbContext` setup
- `Presentation/appsettings.json` — optional configuration file