CREATE TABLE [dbo].[Lieferschein]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Lieferscheinnummer] NCHAR(7) NOT NULL, 
    [Status] TINYINT NOT NULL
)
