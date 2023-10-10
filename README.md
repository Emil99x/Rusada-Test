# Rusada-Test
Web application to assist plane spotters in logging their sightings .

## Table of Contents

- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
  - [Backend](#backend)
  - [Frontend](#frontend)


### Features

List the key features of your solution, highlighting what makes it unique or valuable.

* List all aircraft spotted
* View details ( Make , Model , Registration .. )
* Add aircraft sighting
* Detele sighting recorded 

## Prerequisites

Outline the prerequisites for running your solution. This could include software dependencies, hardware requirements, or any other necessary setup.

- .NET Core 7
- MSSQL 
- Angular CLI
- Node.js ( ^16.14.0 || ^18.10.0 )

## Getting Started

Explain how to get a copy of your project up and running on a local machine. Include instructions for both the backend and frontend components.

### Backend

Backend API follows clear architecture 

Key Components:

* Rusada.API: The Web API layer for seamless external interactions.
* Rusada.Core: Where all core logic and business services reside.
* Rusada.Domain: Home to essential domain entities.
* Rusada.Infrastructure: Handles database operations and data access.

## Run Db migrations 

### Using Command-Line Interface (CLI):

1 . Navigate to the Infrastructure Project: Use the cd command to navigate to the directory containing the Rusada.Infrastructure.csproj file:
```shell
cd path\to\Rusada.Infrastructure
```
2 . Run the Migration Command: Use the dotnet ef CLI tool to apply migrations. Replace YourMigrationName with the actual migration name:
```shell
dotnet ef database update 
```
### Using Visual Studio (VS) Tools:

1. Open the Package Manager Console: Go to Tools > NuGet Package Manager > Package Manager Console to open the Package Manager Console.
2. Ensure Default Project: In the Package Manager Console, make sure the "Default project" dropdown is set to Rusada.Infrastructure.
3. Run the Update-Database Command: Use the following command to apply migrations.
```shell
Update-Database
```
### Frontend

Frondend application uses Angular 16 and Ng Bootstrap for build and run the application, execute bellow comands 
```shell
npm i 
```
```shell
ng serve -o
```
