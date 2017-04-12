CREATE TABLE [dbo].[tblEmployee]
(
	[employeeID] INT NOT NULL PRIMARY KEY IDENTITY,
	[picture]    VARBINARY (MAX) NOT NULL,
    [firstName]       NVARCHAR (50)   NOT NULL,
    [middleName]      NVARCHAR (50)   NULL,
    [lastName]        NVARCHAR (50)   NOT NULL,
    [number]          NVARCHAR (50)   NOT NULL,
    [address]         NVARCHAR (50)   NOT NULL,
    [complement]      NVARCHAR (50)   NULL,
    [city]            NVARCHAR (50)   NOT NULL,
    [provinceID]      INT             NOT NULL,
    [postalCode]      NVARCHAR (50)   NOT NULL,
    [phoneNumber]     NVARCHAR (50)   NOT NULL,
    [otherNumber]     NVARCHAR (50)   NULL,
    [email]           NVARCHAR (50)   NULL, 
    [hireDate] DATE NULL, 
    [termDate] DATE NOT NULL, 
    [sin] INT NULL, 
    [positionID] INT NULL,
	[observations] TEXT NULL

)
