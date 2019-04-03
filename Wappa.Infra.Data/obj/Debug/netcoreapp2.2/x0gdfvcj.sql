IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [StoredEvent] (
    [Id] uniqueidentifier NOT NULL,
    [Action] varchar(100) NULL,
    [AggregateId] uniqueidentifier NOT NULL,
    [CreationDate] datetime2 NOT NULL,
    [Data] nvarchar(max) NULL,
    [User] nvarchar(max) NULL,
    CONSTRAINT [PK_StoredEvent] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190403032557_InitialCreateEvent', N'2.2.3-servicing-35854');

GO

