## .Net Core 3.0 C# Api example using Clean Architecture  

#### Project Update  

Added an Angular 8 Web UI project to access .Net API.  

This solution consists of a simple .Net Core 3.0 C# API developed using Clean Architecture principles. The solution contains the API, Infrastructure, Core, and Test projects. The Infrastructure project utilizes the EntityFramework ORM with a Repository pattern for data access which is accomplished by a single, generic repository interface. Logging is implemented using Microsoft.Extensions.Logging interface with Serilog as the provider.

Swagger has also been implemented but in order to view the endpoints, a database needs to be created & connection strings in the API project/appsettings.json file will need to be modified to reflect your database environment. The SQL scripts to create the database tables are included in the "docs" folder.  

Clean Architecture in this example is obtained through architecture & the implementation of SOLID OOP principles including the following:  

* Separation of Concerns/Single Responsibility: API (REST endpoints), Infrastructure (Data Access), Core (Domain models & generic repository interface)
* Dependency Inversion Principle: All concrete class packages are referenced through abstractions (interfaces or abstract classes ) 
* Explicit Dependencies Principle/Dependency Injection: All dependencies requested via constructor  

The end result is a loosely coupled solution that is easily extensible, & which each layer can be tested independenly.  

### Project Structure  

* The API project contains the REST endpoints, validation filters, logging &  files, AutoMapper maps, DTO models, & configuration.  
* The Infrastructure project contains the EntityFramework database context component & generic repository implementation.  
* The Core project contains the business domain entities, and generic repository inteface.  

![Clean Architecture Diagram](clean_architecture.png)  
Clean Architecture Diagram

From the diagram above, all projects depend on the core project; all dependencies point inward to this core. Inner projects define interfaces, outer projects implement the interfaces. None of the projects reference outward-positioned projects - inward references only.

1.) The API project has references to the Infrastructure and Core projects.  
2.) The Infrastructure project only references the Core project.  
3.) The Core project has no other project references.  

### Prerequisites

AutoMapper (9.0.0)  
AutoMapper.Extensions.Microsoft.DependencyInjection (7.0.0)  
Microsoft.AspNetCore.App (3.0.1)  
Microsoft.NETCore.App(3.0.0)  
Microsoft.EntityFrameworkCore.SqlServer (3.0.1)  
Microsoft.EntityFrameworkCore.Tools (3.0.1)  
Microsoft.Extensions.Logging (3.1.0)  
Newtonsoft.Json (12.0.3)  
Serilog.AspNetCore (3.2.0)  
Serilog.Enrichers.Environment (2.1.3)  
Serilog.Sinks.Async (1.4.0)  
Serilog.Sinks.RollingFile (3.3.0)  
Swashbuckle.AspNetCore (5.0.0-rc4)  
System.Configuration.ConfigurationManager (4.6.0)  
Microsoft.NET.Test.Sdk (16.2.0)  
xunit (2.4.0)  
xunit.runner.visualstudio (2.4.0)

### Installing

1.) Clone or download the project  
2.) Open the solution in VisualStudio 2019  
3.) [Optional] If you have access to an MSSQL server, create a database called "OptBot"  
4.) [Optional] Run the commands in the CREATE Tables script.txt file in the "docs" folder  
5.) [Optional] Modify the solution connection strings to reflect your MSSQL environment  
6.) Build the solution  
7.) Run the solution  
8.) The Swagger implementation can be veiwed at: [host url]/swagger/  

## Running the tests

To run all of the solution tests, click "Test" >>> "Run All Tests"

### Test Composition

There are two types of tests included in the solution. These tests use an in-memory database for tests. Here is the test breakdown:

1.) Integration Tests: Tests the implementations of the asynchronous data repositories.  
2.) Functional Tests: Tests the REST API controllers.  

## Built With

* VisualStudio 2019
* MSSQL Server 2017

## Disclaimer

This solution is provided as a simple implementation of clean architecture using .Net. It is not meant to be used in any environment other than a development environment for learning purposes. By downloading, cloning, or any other means of implementing this solution, you agree to indemnify the author of all liability resulting from the use of this code.

## Author

* **Skip Gregory** - https://github.com/sgregory32
