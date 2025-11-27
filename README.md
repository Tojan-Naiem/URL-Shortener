# URL Shortener API

A simple and efficient URL shortening service built with ASP.NET Core, Entity Framework, and Redis caching.

## Features

- Shorten long URLs into compact 7-character codes
- Redis caching for fast redirects
- SQL Server database for persistent storage
- Automatic unique code generation

## Prerequisites

- .NET 8.0 or higher
- SQL Server
- Redis

## Configuration

Update your `appsettings.json` with the following connection strings:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your SQL Server connection string",
    "Redis": "Your Redis connection string"
  }
}
```

## API Endpoints

### 1. Shorten URL

Creates a shortened URL from a long URL.

**Endpoint:** `POST /api/shorten`

**Request Body:**
```json
{
  "url": "https://example.com/very/long/url/that/needs/shortening"
}
```

**Response:**
```
https://yourdomain.com/api/abc123X
```

**Status Codes:**
- `200 OK` - URL successfully shortened
- `400 Bad Request` - Invalid URL format

---

### 2. Redirect to Original URL

Redirects from a short code to the original long URL.

**Endpoint:** `GET /api/{code}`

**Example:**
```
GET /api/abc123X
```

**Response:**
- Redirects to the original long URL
- Uses Redis cache for improved performance (10-minute TTL)

**Status Codes:**
- `302 Found` - Successful redirect
- `404 Not Found` - Short code does not exist

## Usage Examples

### Creating a Short URL

```bash
curl -X POST https://yourdomain.com/api/shorten \
  -H "Content-Type: application/json" \
  -d '{"url": "https://example.com/long-url"}'
```

### Using a Short URL

Simply visit the shortened URL in your browser or make a GET request:

```bash
curl -L https://yourdomain.com/api/abc123X
```

## Database Schema

**ShortenedUrls Table:**
- `Id` (Guid) - Primary key
- `LongUrl` (string) - Original URL
- `ShortUrl` (string) - Complete shortened URL
- `Code` (string, 7 chars) - Unique short code (indexed)
- `CreatedOnUtc` (DateTime) - Creation timestamp

## Performance

- Redis caching reduces database queries for frequently accessed URLs
- Cache TTL: 10 minutes
- Unique code generation with collision detection
