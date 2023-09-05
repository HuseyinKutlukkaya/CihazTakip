# DeviceManagement(Cihaz Takip)

Asp.Net Core project with Ef Core.

## Table of Contents

- [Introduction](#introduction)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
	- [Database Connection](#database-connection)
	- [Adding Migrations and Creating the Database](#adding-migrations-and-creating-the-database)
- [Running the Application](#running-the-application)
- [Usage](#usage)


## Introduction

My project, titled "Device Management," aims to  track users and devices within. It offers distinct roles, including Admin, Authorized User, and Standard User, each with specific capabilities. 
<br/>
Admins can manage users and devices comprehensively, including adding, editing, deleting, and listing them. <br/>
Authorized Users can assign devices to users and remove these assignments, in addition to viewing all devices. <br/>
Standard Users, on the other hand, can only view devices assigned to them.

## Prerequisites

- .NET SDK 7.0
- PostgreSQL

## Getting Started


### Database Connection

This project may require a database connection. You can configure the database connection in the `appsettings.json` file. Here's an example connection string for PostgreSQL:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=DatabaseName;Port=5432;User ID=username;Password=password"
  }
}
```
Adjust the connection string based on your database type and settings.

### Adding Migrations and Creating the Database

To create the necessary database schema and tables, you can use Entity Framework Core migrations. Follow these steps:

1. Open a terminal or command prompt.

2. Navigate to the solution's Data project where your `ApplicationDbContext.cs` is located.

3. Run the following command to create a migration:

```bash
Add-Migration InitialCreate //Visual Studio
dotnet ef migrations add InitialCreate //.NET CLI
```

After creating the migration, apply it to create the database:
```bash
Update-Database //Visual Studio
dotnet ef database update //.NET CLI
```
This command will execute the migration and create the database.


## Running the Application
After the application is launched  you can use these automatically added accounts:<br/>

```json
  {
    "username": "adminuser",
    "password": "Password_123",
    "role": "admin",

  },
  {
    "username": "authorizeduser",
    "password": "Password_123",
    "role": "authorized",

  },
  {
    "username": "user",
    "password": "Password_123",
    "role": "user",
  }
```
You can edit automatically generated data from `appsettings.json` and `ModelBuilderExtensions.cs`.
## Usage
After logging in, you can check the various functions of the application on the home page.
