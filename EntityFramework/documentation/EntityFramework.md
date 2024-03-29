# Entity Framework

## What is ORM?
Object Relational Mapping (ORM) is a layer that connects object oriented programming to relational databases. It's purpose is to simplify the interaction between the two. A few advantages of using ORM tools:
- Handles the logic required to interact with databases
- Improves security
  - Built to eliminate possible SQL injection attacks
- Write less code

Some drawbacks:
- Can be time consuming to learn ORM tools
- Likely to not perform better with complex queries
- Generally slower than using SQL directly

## What is Entity Framework?
Entity Framework is a Microsoft ORM tool for .NET applications. It enables developers to work with data using .NET objects without focusing on the underlying database tables and columns where this data is stored. With the Entity Framework, developers can work at a higher level of abstraction when they deal with data.

![diagram](https://www.entityframeworktutorial.net/images/basics/ef-in-app-architecture.png)

### Features of Entity Framework
- Modelling
  - Entity Data Models with get/set properties
- Cross-platform
- Change tracking
  - Track changes made to instances of property values
- Querying
  - Retrieve data from a database
- Saving
  - Insert/Update/Delete data in a database
- Concurrency
  - Protect overwriting changes
- Transaction
  - Automate transactions when querying/saving data
- Caching
  - Increases speed of querying data
- Built-in convention
  - Automatically configure the EF Model
- Configuration
  - Allows developers to configure the Entity Framework Model
- Migration
  - Built-in commands for creating and managing database migrations
  - Built-in commands for creating and managing database migrations

### DbContext
DbContext associates with a Model and handles running queries. It is registered with a connection string to your database. Represents a database "session" - should use within a `using` statement to let .NET safely dispose of the database connection afterwards. If you're creating a `DbContext` instance within a dependency injection container, then this is not needed. It is the DI's framework that will handle the scope of the `DbContext`, and safely dispose of it.

By default, the context manages connections to the database. The context opens and closes connections as needed. For example, the context opens a connection to execute a query, and then closes the connection when all the result sets have been processed.

## Steps

1. Install SQLServerExpress onto your machine
    - Includes a LocalDB feature - this will put a minimal set of files necessary to start the SQL Server Database Engine
1. Test you can connect to your localdb instance
    - `Server=(localdb)\MSSQLLocalDB;Integrated Security=true`
    - Windows auth will work as it was installed on your user account
1. Open up the skeleton EntityFramework solution (in Mentoring repository in GitHub)
1. Install `EntityFrameworkCore` and `EntityFrameworkCore.SqlServer` nuget packages
1. Create a `MovieContext` class inheriting `DbContext`
    - This represents a database session
1. Add your connection string into `appsettings.Development.json`
1. Register the database connection context in `Program.cs` by running: 
    ```c#
    builder.Services.AddDbContext<YourDbContextTypeHere>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("YourConnectionStringHere")));   
    ```
    > The default argument for lifetime registration is `Scoped` - this is what we want. The connection to the database will remain open for the lifetime of the request, and will close once the request finishes.
1. Run the PMC (Package Manager Console) command `Add-Migration -Name <someName> -Context <YourDbContextType>,`
    - Creates migration script based on your entity framework models
1. Run the PMC command `update-database`
     - This will apply your migration scripts

Now we've set up the schema and models, we can now write the code to update and query the database

1. Inject your `MovieContext` into the `MovieController`
1. Implement a `[HttpPost]` method to add a movie into the database
    - Check out [saving in EF](https://learn.microsoft.com/en-us/ef/core/saving/)
1. Implement a `[HttpGet]` method to return all movies from the database
    - Check out [querying in EF](https://learn.microsoft.com/en-us/ef/core/querying/)
1. Implement a `[HttpDelete]` method to delete a movie for a given ID from the database

## Resources
- [Migration scripts](https://www.entityframeworktutorial.net/efcore/entity-framework-core-migration.aspx) (in EF)
- [DbContext](https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/)
