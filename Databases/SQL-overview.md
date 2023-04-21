# Structured Query Language

## Terms
**Database**: Organised collection of data
  - Operational db: primarily used to collect, modify, and maintain data on a day-to-day basis. The type of data stored is dynamic, meaning that it changes constantly and always reflects up-to-the-minute information.
  - Analytical db: stores and tracks historical and time-dependent data. It is a valuable asset for tracking trends, viewing statistical data over a long period, or making tactical or strategic business projections. Static data.

**Relational database**: data stored with relations to one another, in the style of tables, records and attributes. A relational database is made up of:
  - Tables: represents a single specific subject
  - Columns: attributes of the table subject
  - Rows: a record, representing a unique instance of the table subject. Has to be uniquely defined (primary key)
  - Keys: special columns
  - Primary key: consists of one or more columns that uniquely identify each row within a table. It is mandatory to have at least 1 column described with a primary key. Helps guarantee table-level integrity by ensuring non-duplicate records.
  - Foreign key: establishes a relation from one table to another, by referencing the other table's primary key within it's own table, called a foreign key
  - Views: a virtual table composed of columns from one or more tables
  - Relationships:
    - one-to-one: single row in `tableA` is related to single row in `tableB`. This is established by taking primary key of `tableA` (primary table) and inserting it as a foreign key into `tableB` (secondary table). Secondary table often has the foreign key as it's own primary key.
    - one-to-many: single row in `tableA` relates to many rows in `tableB`. single row in `tableB` relates to single row in `tableA`.
    This is established by taking primary key of `tableA` and inserting it as a foreign key into `tableB`.
    - many-to-many: single row in `tableA` relates to many rows in `table2`. single row in `tableB` relates to many rows in `tableA`. This is established with a linking table, which has the primary keys of both `tableA` and `tableB`, forming a composite primary key of the linking table, or serving as 2 foreign keys
  - Composite primary key: created by combining 2 (or more) columns in a table to uniquely identify each row

**Relational Database Management System** (RDBMS): software application program you use to create, maintain, modify, and manipulate a relational database (e.g. Azure Data Studio (ADS), Sql Server Management Studio (SSMS))

**Structured Query Language** (SQL) is a programming language designed to manage data stored in RDBMS.

## Steps
1. Connect to your localhost MSSQL database
1. Create a new database and ensure your query window is set to run against it
    - `CREATE DATABASE HRDatabase`
1. Create your schema using sql script [here](https://www.sqltutorial.org/wp-content/uploads/2020/04/sqlserver.txt)
1. Insert data into the tables using sql script [here](https://www.sqltutorial.org/wp-content/uploads/2020/04/sqlserver-data.txt)
1. Take a look at the database structure in ADS
1. Follow the tutorial [here](https://www.sqltutorial.org/), starting at Section 2: Querying Data

## Resources
- https://www.sqltutorial.org
- https://learning.oreilly.com/library/view/sql-queries-for/9780134858432/