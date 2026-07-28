IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Leagues] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Leagues] PRIMARY KEY ([Id])
);

CREATE TABLE [Teams] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [LeagueId] int NOT NULL,
    CONSTRAINT [PK_Teams] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Teams_Leagues_LeagueId] FOREIGN KEY ([LeagueId]) REFERENCES [Leagues] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Teams_LeagueId] ON [Teams] ([LeagueId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260728053029_InitialCreate', N'10.0.10');

COMMIT;
GO

