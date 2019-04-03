CREATE DATABASE Wappa

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Drivers] (
    [Id] uniqueidentifier NOT NULL,
    [Name] varchar(100) NOT NULL,
    [LastName] varchar(100) NOT NULL,
    [CarModel] varchar(100) NOT NULL,
    [CarBrand] varchar(100) NOT NULL,
    [CarPlate] varchar(7) NOT NULL,
    [ZipCode] varchar(9) NOT NULL,
    [Address] varchar(1000) NOT NULL,
    [Coordinates] varchar(500) NOT NULL,
    CONSTRAINT [PK_Drivers] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190403031215_InitialCreate', N'2.2.3-servicing-35854');

GO

