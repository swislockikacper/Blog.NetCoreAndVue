DROP TABLE IF EXISTS [dbo].[Post]
GO;

DROP TABLE IF EXISTS [dbo].[User]
GO;

CREATE TABLE [dbo].[Post]
(
    [Id] INT IDENTITY PRIMARY KEY NOT NULL,
    [Title] NVARCHAR(1000) NOT NULL,
    [Body] NVARCHAR(MAX) NOT NULL
)
GO;

CREATE TABLE [dbo].[User]
(
    [Id] nvarchar(100) NOT NULL PRIMARY KEY,
    [EmailAddress] nvarchar(100) NOT NULL,
    [Password] varbinary(MAX) NOT NULL,
    [Salt] varbinary(MAX) NOT NULL,
    [FullName] nvarchar(200)
)
GO;