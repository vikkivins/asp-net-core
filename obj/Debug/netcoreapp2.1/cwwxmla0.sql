IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Departamentos] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NULL,
    [Sigla] nvarchar(max) NULL,
    CONSTRAINT [PK_Departamentos] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Funcionarios] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NULL,
    [RG] nvarchar(max) NULL,
    [Foto] nvarchar(max) NULL,
    [DepartamentoId] int NOT NULL,
    CONSTRAINT [PK_Funcionarios] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Funcionarios_Departamentos_DepartamentoId] FOREIGN KEY ([DepartamentoId]) REFERENCES [Departamentos] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Funcionarios_DepartamentoId] ON [Funcionarios] ([DepartamentoId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241217044448_initial', N'2.1.14-servicing-32113');

GO

