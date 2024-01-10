CREATE TABLE [dbo].[Vorgang]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [LieferscheinId] INT NOT NULL, 
    [EingangsTS] TIMESTAMP NOT NULL, 
    [MontageTS] TIMESTAMP NULL, 
    [VersandTS] TIMESTAMP NULL, 
    [MitarbeiterId] INT NULL
)
