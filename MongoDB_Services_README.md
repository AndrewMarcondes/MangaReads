# MongoDB Services for MangaReads

This document describes the MongoDB integration added to the MangaReads application.

## Overview

MongoDB NoSQL services have been added to provide an alternative storage backend to the existing JSON and PostgreSQL services. The implementation follows the existing service pattern and implements the same interfaces.

## Components Added

### 1. Configuration
- `MongoDbSettings.cs` - Configuration class for MongoDB connection settings
- Updated `appsettings.json` with MongoDB connection string and database name

### 2. Data Models
- `MongoManga.cs` - MongoDB-specific manga model with BSON attributes
- `MongoUser.cs` - MongoDB-specific user model with BSON attributes
- Both models include conversion methods to/from the original domain models

### 3. Services
- `MangaMongoService.cs` - Implements `IMangaStorageService` for MongoDB
- `UserMongoService.cs` - Implements `IUserService` for MongoDB

## Configuration

To use MongoDB as the storage provider, set the following in `appsettings.json`:

```json
{
  "MongoDbSettings": {
    "DatabaseName": "mangareads",
    "ConnectionString": "mongodb://localhost:27017"
  },
  "StorageProvider": "MongoDB"
}
```

## Features

### MangaMongoService Features:
- Get all manga from storage
- Async operations for better performance
- Get manga by ID or third-party ID
- Create, update, and delete manga
- Search manga by title or description using regex

### UserMongoService Features:
- User management (create, get, delete)
- User manga management (add, update status/volume, delete)
- Async operations available
- Array operations for nested manga documents

## Database Collections

- `mangas` - Stores manga information
- `users` - Stores user information and their manga collections

## Usage

The services are automatically registered in the DI container when `StorageProvider` is set to "MongoDB". No code changes are required in controllers or other services.

## Requirements

- MongoDB server running on localhost:27017 (or update connection string)
- MongoDB.Driver NuGet package (already included)
- MongoDB.Bson NuGet package (already included)

## Storage Provider Options

The application now supports three storage providers:
1. "JSON" - File-based JSON storage (default)
2. "PostgreSQL" - PostgreSQL database storage
3. "MongoDB" - MongoDB NoSQL storage

Change the `StorageProvider` setting in `appsettings.json` to switch between them.