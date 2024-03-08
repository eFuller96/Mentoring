CREATE DATABASE [product-db]
GO

USE [product-db];
GO

CREATE TABLE people (
    Id INT NOT NULL IDENTITY,
    FirstName TEXT NOT NULL,
    LastName TEXT NOT NULL,
    PRIMARY KEY (Id)
);
GO

INSERT INTO [people] (FirstName, LastName)
VALUES 
('Noelia', 'Carrasquilla'),
('Ellie', 'Fuller'); 
GO