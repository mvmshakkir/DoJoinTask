# DoJoinTask Project

This is an ASP.NET Core application that provides a user authentication system using Identity, JWT authentication, and role management.

## Project Setup

### Prerequisites
- .NET 6 or higher
- SQL Server (for the database)
- Visual Studio or any C# IDE

### Steps to Run the Project:

1. Clone this repository to your local machine.
   
   ```bash
   git clone https://github.com/your-repository/DoJoinTask.git```
2 Install required NuGet packages
   ```dotnet restore```
   
3  Configure Database Connection

```{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=your_db;User Id=your_user;Password=your_password;"
  }
}```


### How to Test the Application

1 Login Endpoint:

/api/User/Login
{
  "username": "user@example.com",
  "password": "your_password"
}

2 Response:

--Success (200 OK):
json
Copy code
{
  "token": "your_jwt_token_here"
}
--Failure (401 Unauthorized):
json
Copy code
{
  "message": "Invalid credentials."
}

2 Register Endpoint:
/api/User/Register
{
  "Username": "testuser",
  "Email": "testuser@example.com",
  "Password": "Test@12345",
  "ConfirmPassword": "Test@12345"
}
--Success (200 OK):
{
    "message": "User created successfully!"
}