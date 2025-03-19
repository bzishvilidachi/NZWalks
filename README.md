# ASP.NET Core Web API Project - .NET 8

## Prerequisites
Ensure you have the following installed on your machine:

- **.NET 8 SDK** – [Download .NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- **SQL Server** (or use **SQL Server Express** / **Azure SQL**)
- **SQL Server Management Studio (SSMS)** *(optional, for database management)*
- **Visual Studio 2022** (or VS Code with C# extension)
- **Postman** (or Swagger for API testing)
- **Git** *(optional, for version control)*

## Getting Started

### 1. Clone the Repository
```sh
git clone <repository-url>
cd <repository-folder>
```

### 2. Set Up Database
#### Using SQL Server
1. Update the **appsettings.json** file with your database connection string:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
2. Apply database migrations:
```sh
dotnet ef database update
```

### 3. Run the Application
```sh
dotnet run
```

The API should now be running at `https://localhost:5001` (HTTPS) or `http://localhost:5000` (HTTP).

### 4. API Testing
- **Swagger:** Open your browser and navigate to `https://localhost:5001/swagger`
- **Postman:** Use Postman to test API endpoints manually.

## Features Implemented
 **CRUD operations with Entity Framework Core**  
 **Repository & Unit of Work pattern**  
 **Domain Driven Design (DDD)**  
 **Authentication & Role-based Authorization (JWT, Identity)**  
 **Automapper for DTO mapping**  
 **Filtering, Sorting, and Pagination**  
 **RESTful API principles**  
 **Swagger for API documentation**  
 **Validation with FluentValidation**  

## Environment Variables
You can configure sensitive values using environment variables:
```sh
ASPNETCORE_ENVIRONMENT=Development
JWT_SECRET=<your-secret-key>
```

## Deployment
To publish the application:
```sh
dotnet publish -c Release -o ./publish
```

## Contributing
1. Fork the repository
2. Create a feature branch (`git checkout -b feature-name`)
3. Commit changes (`git commit -m "Add new feature"`)
4. Push to branch (`git push origin feature-name`)
5. Open a Pull Request
