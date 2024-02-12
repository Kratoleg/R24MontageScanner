CREATE TABLE [dbo].[Vorgang]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Lieferschein] NCHAR(7) NOT NULL, 
    [EingangsTS] TIMESTAMP NOT NULL, 
    [MontageTS] TIMESTAMP NULL, 
    [VersandTS] TIMESTAMP NULL, 
    [MitarbeiterId] INT NULL
)
