## .Net Core 3.0 C# Api example using Clean Architecture

This solution consists of a .Net Core 3.0 C# Api developed using Clean Architecture principles. The solution contains the Api, Infrastructure, Core, and Test projects. The solution utilizes the EntityFramework ORM with a Repository pattern for data access which is accomplished by a single generic, asychronous repository interface. Logging is also implemented using Serilog.

Swagger has also been implemented but in order to view the endpoints, a database needs to be created & the connection strings in the api project/appsettings.json file will need to be modified to reflect your database implementation. The SQL scripts to create the database tables are included in the "docs" folder.  

Clean Architecture in this example is obtained through the implementation of the following:  

* Separation of Concerns/Single Responsibility: Api, Infrastructure (Data Access), Core (Domain models & generic, asynchronous repository interface)
* Dependency Inversion Principle: All concrete class packages connect only through abstractions (interface or abstract class packages ) 
* Explicit Dependencies Principle: All dependencies requested via constructor (except Serilog)  

### Project Structure  

* The Api project contains the REST endpoints, validation filters, log files, AutoMapper maps, DTO models, & configuration.  
* The Infrastructure project contains the EntityFramework database context component & the generic, asynchronous EntityFramework repository implementation.  
* The Core project contains the business domain entities, and generic, asynchronous repository inteface.  

![Clean Architecture Diagram](clean_architecture.png)  
Clean Architecture Diagram

From the diagram above, all projects depend on the core project; all dependencies point inward to this core. Inner projects define interfaces, outer projects implement the interfaces. None of the projects reference outward-positioned projects - inward references only.

1.) The Api project has references to the Infrastructure and Core projects.  
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
2.) Functional Tests: Test the Api REST controllers.  

## Built With

* VisualStudio 2019
* MSSQL Server 2017

## Disclaimer

This solution is provided as a simple implementation of clean architecture using .Net. It is not meant to be used in any environment other than a development environment for learning purposes. By downloading, cloning, or any other means of implementing this solution, you agree to indemnify the author of all liability resulting from the use of this code.

## Author

* **Skip Gregory** - https://github.com/sgregory32
