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

### DbContext
Manages the connections to the database. Represents a database "session" - should use within a `using` statement to let .NET safely dispose of the database connection afterwards. Not secure to not dispose!!! 

## Steps

1. Install SQLServerExpress onto your machine
1. Open up the skeleton EntityFramework solution (in Mentoring repository in GitHub)
1. Install EntityFramework nuget package
1. Create a `MovieContext` class inheriting `DbContext`
  - This represents a database session
1. Add your connection string into appsettings.Development.json