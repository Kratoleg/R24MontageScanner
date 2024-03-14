CREATE TABLE [dbo].[Vorgang]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Lieferschein] NCHAR(7) NOT NULL, 
    [EingangsTS] DATETIME NOT NULL, 
    [MontageTS] DATETIME NULL, 
    [VersandTS] DATETIME NULL, 
    [MitarbeiterId] INT NULL
)
