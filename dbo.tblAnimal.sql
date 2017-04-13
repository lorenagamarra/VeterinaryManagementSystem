CREATE TABLE [dbo].[tblAnimal] (
    [animalID]    INT NOT NULL IDENTITY,
	[ownerID]     INT NOT NULL,
    [breedID]     INT NOT NULL,
    [vachistID]   INT NULL,
    [datereg]     DATE NOT NULL,
    [name]        NVARCHAR(50) NOT NULL,
    [gender]      TINYINT NOT NULL,
	[dateofbirth] DATE NULL,
    [weight]      DECIMAL NOT NULL,
    [specie]      NVARCHAR(50) NOT NULL,
    [identification] NVARCHAR(50) NULL, 
    [food] NVARCHAR(50) NULL, 
    [phobia] NVARCHAR(50) NULL, 
    [flagset] NVARCHAR(200) NULL, 
    [vethistoric] TEXT NULL, 
    PRIMARY KEY CLUSTERED ([animalID] ASC), 
    CONSTRAINT [FK_tblAnimal_tblOwner] FOREIGN KEY ([ownerID]) REFERENCES [tblOwner]([ownerID]), 
    CONSTRAINT [FK_tblAnimal_tblBreed] FOREIGN KEY ([breedID]) REFERENCES [tblBreed]([breedID]),
	CONSTRAINT [FK_tblAnimal_tblVaccineHistoric] FOREIGN KEY ([vachistID]) REFERENCES [tblVaccineHistoric]([vachistID])
);

