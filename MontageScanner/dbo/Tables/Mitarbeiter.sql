﻿CREATE TABLE [dbo].[Mitarbeiter]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Vorname] VARCHAR(20) NOT NULL, 
    [Nachname] VARBINARY(20) NOT NULL, 
    [ChipId] VARCHAR(20) NOT NULL
)
