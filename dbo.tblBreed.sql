CREATE TABLE [dbo].[tblBreed] (
    [breedID] INT           IDENTITY (1, 1) NOT NULL,
    [specie]  NVARCHAR (50) NOT NULL,
    [name]    NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([breedID] ASC)
);

