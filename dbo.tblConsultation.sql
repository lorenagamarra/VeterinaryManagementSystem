CREATE TABLE [dbo].[tblConsultation]
(
	[consultationID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [animalID] INT NOT NULL, 
    [employeeID] INT NOT NULL, 
    [vaccineID] INT NULL, 
    [ServProdID] INT NOT NULL, 
    [date] DATE NOT NULL, 
    [record] TEXT NOT NULL, 
    [prescription] TEXT NULL, 
    [quantity] INT NOT NULL, 
    [cost] MONEY NOT NULL, 
    CONSTRAINT [FK_tblConsultation_tblAnimal] FOREIGN KEY ([animalID]) REFERENCES [tblAnimal]([animalID]), 
    CONSTRAINT [FK_tblConsultation_tblEmployee] FOREIGN KEY ([employeeID]) REFERENCES [tblEmployee]([employeeID]), 
    CONSTRAINT [FK_tblConsultation_tblVaccine] FOREIGN KEY ([vaccineID]) REFERENCES [tblVaccine]([vaccineID]), 
    CONSTRAINT [FK_tblConsultation_tblService&Products] FOREIGN KEY ([ServProdID]) REFERENCES [tblServices&Products]([ServProdID])
)
