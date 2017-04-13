CREATE TABLE [dbo].[tblVaccineHistoric]
(
	[vachistID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] NVARCHAR(50) NOT NULL, 
    [date] DATE NOT NULL, 
    [details] NVARCHAR(50) NULL
)
