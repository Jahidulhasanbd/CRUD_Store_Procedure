CREATE DATABASE TestDB
GO

USE TestDB
GO

CREATE TABLE  Customers
(
CustomerId INT IDENTITY PRIMARY KEY,
name VARCHAR(100)NOT NULL,
Country VARCHAR(50)NOT NULL
)
GO

INSERT INTO Customers
SELECT TOP 10 FirstName,Country FROM [DESKTOP-FJSE08F].[Northwind].[dbo].[Employees]
GO

SELECT *FROM Customers
GO

--STORE PROCEDURE
CREATE PROC spSearchCustomer @name VARCHAR(50)

AS 
BEGIN
SELECT *FROM Customers WHERE name LIKE '%'+@name+'%'
END
GO
--FOR INSERT
CREATE PROC spInsertCustomer @name VARCHAR(50),
							 @Country VARCHAR(50)
AS
BEGIN
INSERT INTO Customers VALUES (@name,@Country)
END
GO
--TEST INSERT
EXEC spInsertCustomer 'Jahidul hasan','Bangladesh'
GO
--FOR DELETE
CREATE PROC spDeleteCustomer @customerId int
AS
BEGIN
DELETE FROM Customers WHERE CustomerId=@customerId
END
GO

EXEC spDeleteCustomer 11
GO
--UPDATE
CREATE PROC spUpdateCustomer @customerId int,
							 @name varchar(50),
							 @country varchar(50)
AS
BEGIN
UPDATE Customers SET name=@name,Country=@country
WHERE CustomerId=@customerId
END
GO

