CREATE TABLE [dbo].[tblServices&Products]
(
	[ServProdID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] NVARCHAR(50) NOT NULL UNIQUE, 
    [price] MONEY NOT NULL
)
