# RealEstate API Project

## Overview

This is a comprehensive Real Estate API project developed in C\# using ASP.NET Core 8.0. The API provides robust endpoints to manage real estate properties, including creating, reading, updating, and deleting property listings. The project follows clean architecture principles with a well-structured layered design including Controllers, Services, Data Access Layer (DAL), and AutoMapper profiles for efficient object mapping.

## Features

- **Full CRUD Operations** for real estate properties
- **Layered Architecture** for better separation of concerns and maintainability
- **Entity Framework Core** for efficient database operations and migrations
- **AutoMapper Integration** for object-to-object mapping
- **RESTful API Design** following industry best practices
- **Containerized Deployment** with Docker support
- **Configuration Management** via appsettings.json
- **Development and Production** environment configurations


## Technology Stack

- **Backend Framework:** ASP.NET Core 8.0
- **Programming Language:** C\# (99.0%)
- **Database ORM:** Entity Framework Core
- **Object Mapping:** AutoMapper
- **Containerization:** Docker
- **Configuration:** JSON-based configuration files


## Getting Started

### Prerequisites

Before running this project, ensure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- SQL Server or any Entity Framework Core supported database
- Docker (optional, for containerized deployment)
- Visual Studio 2022 or VS Code (recommended IDEs)


### Installation \& Setup

1. **Clone the repository:**

```bash
git clone https://github.com/hassanolaa/RealEstate-API-project.git
```

2. **Navigate to the project directory:**

```bash
cd RealEstate-API-project
```

3. **Restore NuGet packages:**

```bash
dotnet restore
```

4. **Configure Database Connection:**
    - Update the connection string in `appsettings.json` and `appsettings.Development.json`
    - Configure your preferred database provider
5. **Apply Database Migrations:**

```bash
dotnet ef database update
```

6. **Run the application:**

```bash
dotnet run
```


The API will be available at `https://localhost:5001` or `http://localhost:5000`

## API Usage

### Base URL

```
https://localhost:{port}/api/
```


### Testing the API

- Use tools like **Postman**, **Swagger UI**, or **curl** to test the API endpoints
- The project includes HTTP request examples in `realstate.http` file
- Access the Swagger documentation at `/swagger` endpoint when running in development mode


## Docker Deployment

### Building the Docker Image

```bash
docker build -t realestate-api .
```


### Running the Container

```bash
docker run -d -p 8080:80 --name realestate-api-container realestate-api
```

The API will be accessible at `http://localhost:8080`

## Project Structure

```
RealEstate-API-project/
├── Controllers/          # API Controllers handling HTTP requests
├── Services/            # Business logic and service layer
├── DAL/                # Data Access Layer with repositories
├── Profiles/           # AutoMapper mapping profiles
├── Properties/         # Model classes and DTOs
├── Migrations/         # Entity Framework Core migrations
├── bin/               # Compiled binaries
├── obj/               # Build objects
├── Dockerfile         # Docker container configuration
├── Program.cs         # Application entry point and configuration
├── appsettings.json   # Production configuration
├── appsettings.Development.json  # Development configuration
├── realstate.csproj   # Project file with dependencies
├── realstate.http     # HTTP request examples
└── realstate.sln      # Solution file
```

## **Core Properties Endpoints**

### **1. GET /api/properties**

**Purpose:** Retrieve all property listings with optional filtering and pagination

**Request Parameters:**

- `page` (query) - Page number for pagination (default: 1)
- `pageSize` (query) - Number of items per page (default: 10)
- `location` (query) - Filter by city, state, or address
- `minPrice` (query) - Minimum price filter
- `maxPrice` (query) - Maximum price filter
- `propertyType` (query) - Filter by type (apartment, house, commercial, etc.)
- `bedrooms` (query) - Number of bedrooms
- `bathrooms` (query) - Number of bathrooms
- `sortBy` (query) - Sort criteria (price, date, size)
- `sortOrder` (query) - asc or desc

**Response (200 OK):**

```json
{
  "data": [
    {
      "id": 1,
      "title": "Beautiful 3BR House",
      "description": "Spacious family home in quiet neighborhood",
      "price": 450000,
      "location": "123 Main St, Springfield",
      "propertyType": "House",
      "bedrooms": 3,
      "bathrooms": 2,
      "squareFeet": 2100,
      "yearBuilt": 2015,
      "images": ["image1.jpg", "image2.jpg"],
      "agentId": 5,
      "createdAt": "2025-08-15T10:30:00Z",
      "updatedAt": "2025-08-16T14:20:00Z"
    }
  ],
  "totalCount": 150,
  "page": 1,
  "pageSize": 10
}
```


### **2. GET /api/properties/{id}**

**Purpose:** Retrieve detailed information for a specific property

**Path Parameters:**

- `id` (integer) - Unique property identifier

**Response (200 OK):**

```json
{
  "id": 1,
  "title": "Beautiful 3BR House",
  "description": "Spacious family home with modern amenities...",
  "price": 450000,
  "location": "123 Main St, Springfield",
  "propertyType": "House",
  "bedrooms": 3,
  "bathrooms": 2,
  "squareFeet": 2100,
  "yearBuilt": 2015,
  "lotSize": 0.25,
  "features": ["Garage", "Swimming Pool", "Garden"],
  "images": ["image1.jpg", "image2.jpg", "image3.jpg"],
  "agent": {
    "id": 5,
    "name": "John Smith",
    "phone": "555-0123",
    "email": "john@realestate.com"
  },
  "createdAt": "2025-08-15T10:30:00Z",
  "updatedAt": "2025-08-16T14:20:00Z"
}
```

**Error Response (404 Not Found):**

```json
{
  "error": "Property not found",
  "message": "Property with ID 1 does not exist"
}
```


### **3. POST /api/properties**

**Purpose:** Create a new property listing

**Request Headers:**

- `Content-Type: application/json`
- `Authorization: Bearer {token}` (if authentication required)

**Request Body:**

```json
{
  "title": "Modern Downtown Condo",
  "description": "Luxury condo with city views",
  "price": 350000,
  "location": "456 Downtown Ave, Metro City",
  "propertyType": "Condo",
  "bedrooms": 2,
  "bathrooms": 2,
  "squareFeet": 1200,
  "yearBuilt": 2020,
  "features": ["Balcony", "Gym Access", "Parking"],
  "agentId": 3
}
```

**Response (201 Created):**

```json
{
  "id": 151,
  "title": "Modern Downtown Condo",
  "description": "Luxury condo with city views",
  "price": 350000,
  "location": "456 Downtown Ave, Metro City",
  "propertyType": "Condo",
  "bedrooms": 2,
  "bathrooms": 2,
  "squareFeet": 1200,
  "yearBuilt": 2020,
  "features": ["Balcony", "Gym Access", "Parking"],
  "agentId": 3,
  "createdAt": "2025-08-16T18:54:00Z",
  "updatedAt": "2025-08-16T18:54:00Z"
}
```

**Error Response (400 Bad Request):**

```json
{
  "errors": {
    "title": ["Title is required"],
    "price": ["Price must be greater than 0"]
  }
}
```


### **4. PUT /api/properties/{id}**

**Purpose:** Update an existing property listing

**Path Parameters:**

- `id` (integer) - Property ID to update

**Request Body:**

```json
{
  "title": "Updated Beautiful 3BR House",
  "description": "Recently renovated family home",
  "price": 475000,
  "location": "123 Main St, Springfield",
  "propertyType": "House",
  "bedrooms": 3,
  "bathrooms": 2,
  "squareFeet": 2100,
  "yearBuilt": 2015,
  "features": ["Garage", "Swimming Pool", "Garden", "New Kitchen"]
}
```

**Response (200 OK):**
Returns the updated property object with new `updatedAt` timestamp.

**Error Responses:**

- `404 Not Found` - Property doesn't exist
- `400 Bad Request` - Validation errors


### **5. DELETE /api/properties/{id}**

**Purpose:** Delete a property listing

**Path Parameters:**

- `id` (integer) - Property ID to delete

**Response (204 No Content):**
Empty response body indicating successful deletion.

**Error Response (404 Not Found):**

```json
{
  "error": "Property not found",
  "message": "Cannot delete property that doesn't exist"
}
```


## **Additional Feature Endpoints**

### **6. GET /api/properties/search**

**Purpose:** Advanced property search with multiple criteria

**Query Parameters:**

- `keyword` - Search in title and description
- `location` - Geographic search
- `radius` - Search radius in miles
- `priceRange` - Format: "min-max"
- Multiple other filters


### **7. GET /api/categories**

**Purpose:** Retrieve available property categories/types

**Response (200 OK):**

```json
[
  {
    "id": 1,
    "name": "House",
    "description": "Single-family residential homes"
  },
  {
    "id": 2,
    "name": "Condo",
    "description": "Condominium units"
  },
  {
    "id": 3,
    "name": "Commercial",
    "description": "Business and commercial properties"
  }
]
```


### **8. GET /api/agents**

**Purpose:** Retrieve real estate agents list

**Response (200 OK):**

```json
[
  {
    "id": 1,
    "name": "John Smith",
    "email": "john@realestate.com",
    "phone": "555-0123",
    "specialization": "Residential",
    "propertiesCount": 25
  }
]
```


### **9. GET /api/agents/{id}/properties**

**Purpose:** Get all properties managed by a specific agent

## **Authentication Endpoints (If Implemented)**

### **10. POST /api/auth/register**

**Purpose:** Register a new user account

**Request Body:**

```json
{
  "email": "user@example.com",
  "password": "SecurePass123!",
  "firstName": "Jane",
  "lastName": "Doe",
  "role": "Agent"
}
```


### **11. POST /api/auth/login**

**Purpose:** Authenticate user and receive access token

**Request Body:**

```json
{
  "email": "user@example.com",
  "password": "SecurePass123!"
}
```

**Response (200 OK):**

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresAt": "2025-08-17T18:54:00Z",
  "user": {
    "id": 1,
    "email": "user@example.com",
    "name": "Jane Doe",
    "role": "Agent"
  }
}
```


## **HTTP Status Codes Used**

- **200 OK** - Successful GET/PUT requests
- **201 Created** - Successful POST requests
- **204 No Content** - Successful DELETE requests
- **400 Bad Request** - Validation errors, malformed requests
- **401 Unauthorized** - Authentication required
- **403 Forbidden** - Access denied
- **404 Not Found** - Resource doesn't exist
- **409 Conflict** - Resource conflict (e.g., duplicate)
- **500 Internal Server Error** - Server-side errors


## **Common Request/Response Headers**

**Request Headers:**

- `Content-Type: application/json`
- `Authorization: Bearer {jwt-token}`
- `Accept: application/json`

**Response Headers:**

- `Content-Type: application/json`
- `X-Total-Count` - For paginated responses
- `Location` - For created resources (201 responses)


## **Error Response Format**

All error responses follow a consistent structure:

```json
{
  "error": "Error Type",
  "message": "Detailed error description",
  "timestamp": "2025-08-16T18:54:00Z",
  "path": "/api/properties/123"
}
```

## Configuration

### Database Configuration

Update the connection strings in your `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your-Database-Connection-String-Here"
  }
}
```


### Environment-Specific Settings

- **Development:** Configure in `appsettings.Development.json`
- **Production:** Configure in `appsettings.json`


## Development

### Adding New Features

1. Create models in the `Properties/` directory
2. Add business logic in the `Services/` layer
3. Implement data access in the `DAL/` layer
4. Create API endpoints in the `Controllers/` directory
5. Configure mapping profiles in `Profiles/`

### Database Changes

When making model changes:

```bash
dotnet ef migrations add YourMigrationName
dotnet ef database update
```


## Contributing

Contributions are welcome! Please follow these steps:

1. **Fork** the repository
2. **Create** a feature branch (`git checkout -b feature/AmazingFeature`)
3. **Commit** your changes (`git commit -m 'Add some AmazingFeature'`)
4. **Push** to the branch (`git push origin feature/AmazingFeature`)
5. **Open** a Pull Request

### Contribution Guidelines

- Follow C\# coding conventions
- Write meaningful commit messages
- Add unit tests for new features
- Update documentation as needed


## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Support

For questions, issues, or contributions, please:

- Open an issue on GitHub
- Contact the maintainer: [hassanolaa](https://github.com/hassanolaa)

***


