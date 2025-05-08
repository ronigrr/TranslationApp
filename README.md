# Translation App

A .NET-based web application that provides English to Hebrew translation services using Google Cloud Translation API.

## Overview

This application is a RESTful API service that translates English words to Hebrew. It uses Google Cloud Translation API for translations and implements an in-memory caching mechanism to store previously translated words for better performance.

## Features

- English to Hebrew translation
- In-memory caching of translations
- Similar word matching for better user experience
- RESTful API endpoints
- Swagger UI for API documentation
- Input validation and error handling

## Prerequisites

- .NET 9.0 SDK
- Google Cloud account with Translation API enabled
- Google Cloud credentials file

## Setup

1. Clone the repository
2. Place your Google Cloud credentials file at `c:\google\googletranslatorapi-key.json` or change the path in program.cs
   - The credentials file should be a valid JSON service account key file from Google Cloud Console
   - Make sure the service account has access to the Translation API
3. Build and run the application

## API Endpoints

### Translate Word
- **GET** `/api/Translation?word={word}`
- Translates an English word to Hebrew
- Returns the Hebrew translation as a string
- Maximum word length: 50 characters

## Project Structure

- `Controllers/` - API endpoints
- `Services/` - Business logic and translation service implementation
- `Repositorys/` - Data access layer (in-memory storage)
- `Infrastructure/` - Core interfaces and abstractions
- `Abstractions/` - Domain entities and models
- `Tests/` - Unit tests

## Dependencies

- Google.Cloud.Translation.V2 (3.4.0)
- Google.Apis.Translate.v2 (1.68.0.875)
- Swashbuckle.AspNetCore (8.1.1)
- xUnit (2.9.3) for testing
- Moq (4.20.72) for test mocking

## Running the Application

The application runs on:
- HTTP: http://localhost:5052
- HTTPS: https://localhost:7219

In development mode, Swagger UI is available at `/swagger` endpoint.

## Important Notes

### Credentials File
The application requires a Google Cloud credentials file to be present at `c:\google\googletranslatorapi-key.json`. This file must:
- Be a valid JSON service account key file
- Have the necessary permissions for Google Cloud Translation API
- Not be empty or corrupted

If the credentials file is missing or invalid, the application will fail to start.

## Testing

The project includes unit tests for the translation service. Run tests using:
```bash
dotnet test
```

## Error Handling

The application includes comprehensive error handling for:
- Missing or invalid credentials
- Invalid input (empty or too long words)
- Translation API failures
- Network issues