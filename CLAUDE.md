# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Development Commands

### Build and Run
- `dotnet build` - Build the solution
- `dotnet run --project MangaReads` - Run the application
- `dotnet watch --project MangaReads` - Run with hot reload for development

### Development Environment
- Application runs on https://localhost:7007 and http://localhost:5138
- Swagger UI available at `/swagger` endpoint
- Uses .NET 8.0 with ASP.NET Core Web API

## Architecture Overview

This is a .NET 8 ASP.NET Core Web API application for manga reading management that follows a layered architecture pattern:

### Core Structure
- **Controllers/** - API endpoints for manga and user operations
- **Services/** - Business logic and external API integration
- **Interfaces/** - Service contracts and abstractions
- **Classes/** - Domain models (Manga, User, Volume)
- **DTOs/** - Data transfer objects for API responses

### Key Services
- **MangaDex**: Primary manga service implementing IMangaService, integrates with MangaDex API
- **MangaJsonService**: Local storage service implementing IMangaStorageService, manages JSON file persistence
- **UserJsonService**: User management service implementing IUserService

### External Dependencies
- **MangaDx API**: Primary data source at https://api.mangadx.org/ 
- **Newtonsoft.Json**: JSON serialization throughout the application
- **Swagger/OpenAPI**: API documentation and testing interface

### Data Storage
- Uses JSON files for local persistence: `mangadata.json` and `userData.json`
- No database - file-based storage approach
- ReadAndParseJsonFileWithNewtonsoftJson utility class handles JSON file operations

### API Structure
- Base route: `manga/` for MangaController with endpoints:
  - `GetMangaSearch` - Search manga by name
  - `GetMangaInformation` - Get detailed manga info by ID  
  - `GetMangaVolumeInformation` - Get volume data for manga
- CORS enabled with "AllowAll" policy for cross-origin requests
- Dependency injection configured in Program.cs with singleton services

### Important Implementation Details
- MangaDx service transforms API responses from MangaSearch DTOs to internal Manga objects
- Volume handling uses dynamic JSON parsing due to MangaDx API structure
- All services are registered as singletons, meaning HttpClient instances are reused
- No testing framework detected - tests would need to be added if required