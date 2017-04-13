CREATE TABLE [dbo].[tblVaccine]
(
	[vaccineID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] NVARCHAR(50) NOT NULL UNIQUE, 
    [price] MONEY NOT NULL
)
