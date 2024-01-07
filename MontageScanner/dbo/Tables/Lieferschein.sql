CREATE TABLE [dbo].[Lieferschein]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Lieferscheinnummer] NCHAR(7) NOT NULL, 
    [Status] TINYINT NOT NULL
)
