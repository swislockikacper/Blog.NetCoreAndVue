DROP TABLE IF EXISTS [dbo].[Post]
GO;

CREATE TABLE [dbo].[Post]
(
    [Id] INT IDENTITY PRIMARY KEY NOT NULL,
    [Title] NVARCHAR(1000) NOT NULL,
    [Body] NVARCHAR(MAX) NOT NULL
)
GO;
