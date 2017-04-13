CREATE TABLE [dbo].[tblEmployee] (
    [employeeID]   INT             IDENTITY (1, 1) NOT NULL,
    [picture]      VARBINARY (MAX) NOT NULL,
    [firstName]    NVARCHAR (50)   NOT NULL,
    [middleName]   NVARCHAR (50)   NULL,
    [lastName]     NVARCHAR (50)   NOT NULL,
    [number]       NVARCHAR (50)   NOT NULL,
    [address]      NVARCHAR (50)   NOT NULL,
    [complement]   NVARCHAR (50)   NULL,
    [city]         NVARCHAR (50)   NOT NULL,
    [province]   NVARCHAR(50)             NOT NULL,
    [postalCode]   NVARCHAR (50)   NOT NULL,
    [phoneNumber]  INT   NOT NULL,
    [otherNumber]  INT   NULL,
    [email]        NVARCHAR (50)   NULL UNIQUE,
    [hireDate]     DATE            NOT NULL,
    [termDate]     DATE            NULL,
    [sin]          INT             NOT NULL UNIQUE,
    [position]   NVARCHAR(50)             NOT NULL,
    [observations] TEXT            NULL,
    PRIMARY KEY CLUSTERED ([employeeID] ASC)
);

