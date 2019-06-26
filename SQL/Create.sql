DROP PROCEDURE IF EXISTS [dbo].[DeletePost]
GO

ALTER TABLE [dbo].[PostElement] DROP CONSTRAINT IF EXISTS [FK_POST_POSTELEMENT]
GO

ALTER TABLE [dbo].[Post] DROP CONSTRAINT IF EXISTS [FK_POST_USER]
GO

DROP TABLE IF EXISTS [dbo].[PostElement]
GO

DROP TABLE IF EXISTS [dbo].[Post]
GO

DROP TABLE IF EXISTS [dbo].[User]
GO

CREATE TABLE [dbo].[Post]
(
    [Id] INT IDENTITY PRIMARY KEY NOT NULL,
    [Title] NVARCHAR(MAX) NOT NULL,
    [UserId] NVARCHAR(100) NOT NULL,
    [Created] BIGINT NOT NULL
)
GO

CREATE TABLE [dbo].[PostElement]
(
    [Id] INT IDENTITY PRIMARY KEY NOT NULL,
    [Type] SMALLINT NOT NULL,
    [Number] SMALLINT NOT NULL,
    [Content] NVARCHAR(MAX) NOT NULL,
    [PostId] INT NOT NULL 
)
GO

CREATE TABLE [dbo].[User]
(
    [Id] NVARCHAR(100) NOT NULL PRIMARY KEY,
    [EmailAddress] NVARCHAR(500) NOT NULL,
    [Password] VARBINARY(MAX) NOT NULL,
    [Salt] VARBINARY(MAX) NOT NULL,
    [FullName] NVARCHAR(200)
)
GO

ALTER TABLE [dbo].[PostElement] ADD CONSTRAINT [FK_POST_POSTELEMENT] FOREIGN KEY ([PostId]) REFERENCES [dbo].[Post]([Id])
GO

ALTER TABLE [dbo].[Post] ADD CONSTRAINT [FK_POST_USER] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([Id])
GO

CREATE PROCEDURE [dbo].[DeletePost] @Id AS INT
AS
    BEGIN
        DELETE FROM [dbo].[PostElement] WHERE [PostId] = @Id
        DELETE FROM [dbo].[Post] WHERE [Id] = @Id
    END
GO