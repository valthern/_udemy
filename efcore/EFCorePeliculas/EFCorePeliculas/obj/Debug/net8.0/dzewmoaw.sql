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
GO

CREATE TABLE [Actores] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(150) NOT NULL,
    [Biografia] nvarchar(max) NULL,
    [FechaNacimiento] date NULL,
    CONSTRAINT [PK_Actores] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Cines] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(150) NOT NULL,
    [Ubicacion] geography NULL,
    CONSTRAINT [PK_Cines] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Generos] (
    [Identificador] int NOT NULL IDENTITY,
    [Nombre] nvarchar(150) NOT NULL,
    CONSTRAINT [PK_Generos] PRIMARY KEY ([Identificador])
);
GO

CREATE TABLE [Peliculas] (
    [Id] int NOT NULL IDENTITY,
    [Titulo] nvarchar(250) NOT NULL,
    [EnCartelera] bit NOT NULL,
    [FechaEstreno] date NOT NULL,
    [PosterURL] varchar(500) NULL,
    CONSTRAINT [PK_Peliculas] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CinesOfertas] (
    [Id] int NOT NULL IDENTITY,
    [FechaInicio] date NOT NULL,
    [FechaFin] date NOT NULL,
    [PorcentajeDescuento] decimal(5,2) NOT NULL,
    [CineId] int NOT NULL,
    CONSTRAINT [PK_CinesOfertas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CinesOfertas_Cines_CineId] FOREIGN KEY ([CineId]) REFERENCES [Cines] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [SalasDeCine] (
    [Id] int NOT NULL IDENTITY,
    [TipoSalaDeCine] int NOT NULL DEFAULT 1,
    [Precio] decimal(9,2) NOT NULL,
    [CineId] int NOT NULL,
    CONSTRAINT [PK_SalasDeCine] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SalasDeCine_Cines_CineId] FOREIGN KEY ([CineId]) REFERENCES [Cines] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [GeneroPelicula] (
    [GenerosIdentificador] int NOT NULL,
    [PeliculasId] int NOT NULL,
    CONSTRAINT [PK_GeneroPelicula] PRIMARY KEY ([GenerosIdentificador], [PeliculasId]),
    CONSTRAINT [FK_GeneroPelicula_Generos_GenerosIdentificador] FOREIGN KEY ([GenerosIdentificador]) REFERENCES [Generos] ([Identificador]) ON DELETE CASCADE,
    CONSTRAINT [FK_GeneroPelicula_Peliculas_PeliculasId] FOREIGN KEY ([PeliculasId]) REFERENCES [Peliculas] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [PeliculasActores] (
    [PeliculaId] int NOT NULL,
    [ActorId] int NOT NULL,
    [Personaje] nvarchar(150) NULL,
    [Orden] int NOT NULL,
    CONSTRAINT [PK_PeliculasActores] PRIMARY KEY ([PeliculaId], [ActorId]),
    CONSTRAINT [FK_PeliculasActores_Actores_ActorId] FOREIGN KEY ([ActorId]) REFERENCES [Actores] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PeliculasActores_Peliculas_PeliculaId] FOREIGN KEY ([PeliculaId]) REFERENCES [Peliculas] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [PeliculaSalaDeCine] (
    [PeliculasId] int NOT NULL,
    [SalasDeCineId] int NOT NULL,
    CONSTRAINT [PK_PeliculaSalaDeCine] PRIMARY KEY ([PeliculasId], [SalasDeCineId]),
    CONSTRAINT [FK_PeliculaSalaDeCine_Peliculas_PeliculasId] FOREIGN KEY ([PeliculasId]) REFERENCES [Peliculas] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PeliculaSalaDeCine_SalasDeCine_SalasDeCineId] FOREIGN KEY ([SalasDeCineId]) REFERENCES [SalasDeCine] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_CinesOfertas_CineId] ON [CinesOfertas] ([CineId]);
GO

CREATE INDEX [IX_GeneroPelicula_PeliculasId] ON [GeneroPelicula] ([PeliculasId]);
GO

CREATE INDEX [IX_PeliculasActores_ActorId] ON [PeliculasActores] ([ActorId]);
GO

CREATE INDEX [IX_PeliculaSalaDeCine_SalasDeCineId] ON [PeliculaSalaDeCine] ([SalasDeCineId]);
GO

CREATE INDEX [IX_SalasDeCine_CineId] ON [SalasDeCine] ([CineId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241018195020_Inicial', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Biografia', N'FechaNacimiento', N'Nombre') AND [object_id] = OBJECT_ID(N'[Actores]'))
    SET IDENTITY_INSERT [Actores] ON;
INSERT INTO [Actores] ([Id], [Biografia], [FechaNacimiento], [Nombre])
VALUES (1, N'Thomas Stanley Holland (Kingston upon Thames, Londres; 1 de junio de 1996), conocido simplemente como Tom Holland, es un actor, actor de voz y bailarín británico.', '1996-06-01', N'Tom Holland'),
(2, N'Samuel Leroy Jackson (Washington D. C., 21 de diciembre de 1948), conocido como Samuel L. Jackson, es un actor y productor de cine, televisión y teatro estadounidense. Ha sido candidato al premio Óscar, a los Globos de Oro y al Premio del Sindicato de Actores, así como ganador de un BAFTA al mejor actor de reparto.', '1948-12-21', N'Samuel L. Jackson'),
(3, N'Robert John Downey Jr. (Nueva York, 4 de abril de 1965) es un actor, actor de voz, productor y cantante estadounidense. Inició su carrera como actor a temprana edad apareciendo en varios filmes dirigidos por su padre, Robert Downey Sr., y en su infancia estudió actuación en varias academias de Nueva York.', '1965-04-04', N'Robert Downey Jr.'),
(4, NULL, '1981-06-13', N'Chris Evans'),
(5, NULL, '1972-05-02', N'Dwayne Johnson'),
(6, NULL, '2000-11-22', N'Auli''i Cravalho'),
(7, NULL, '1984-11-22', N'Scarlett Johansson'),
(8, NULL, '1964-09-02', N'Keanu Reeves');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Biografia', N'FechaNacimiento', N'Nombre') AND [object_id] = OBJECT_ID(N'[Actores]'))
    SET IDENTITY_INSERT [Actores] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nombre', N'Ubicacion') AND [object_id] = OBJECT_ID(N'[Cines]'))
    SET IDENTITY_INSERT [Cines] ON;
INSERT INTO [Cines] ([Id], [Nombre], [Ubicacion])
VALUES (1, N'Agora Mall', geography::Parse('POINT (-69.9388777 18.4839233)')),
(2, N'Sambil', geography::Parse('POINT (-69.911582 18.482455)')),
(3, N'Megacentro', geography::Parse('POINT (-69.856309 18.506662)')),
(4, N'Acropolis', geography::Parse('POINT (-69.939248 18.469649)'));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nombre', N'Ubicacion') AND [object_id] = OBJECT_ID(N'[Cines]'))
    SET IDENTITY_INSERT [Cines] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Identificador', N'Nombre') AND [object_id] = OBJECT_ID(N'[Generos]'))
    SET IDENTITY_INSERT [Generos] ON;
INSERT INTO [Generos] ([Identificador], [Nombre])
VALUES (1, N'Acción'),
(2, N'Animación'),
(3, N'Comedia'),
(4, N'Ciencia ficción'),
(5, N'Drama');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Identificador', N'Nombre') AND [object_id] = OBJECT_ID(N'[Generos]'))
    SET IDENTITY_INSERT [Generos] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'EnCartelera', N'FechaEstreno', N'PosterURL', N'Titulo') AND [object_id] = OBJECT_ID(N'[Peliculas]'))
    SET IDENTITY_INSERT [Peliculas] ON;
INSERT INTO [Peliculas] ([Id], [EnCartelera], [FechaEstreno], [PosterURL], [Titulo])
VALUES (1, CAST(0 AS bit), '2012-04-11', 'https://upload.wikimedia.org/wikipedia/en/8/8a/The_Avengers_%282012_film%29_poster.jpg', N'Avengers'),
(2, CAST(0 AS bit), '2017-11-22', 'https://upload.wikimedia.org/wikipedia/en/9/98/Coco_%282017_film%29_poster.jpg', N'Coco'),
(3, CAST(0 AS bit), '2021-12-17', 'https://upload.wikimedia.org/wikipedia/en/0/00/Spider-Man_No_Way_Home_poster.jpg', N'Spider-Man: No way home'),
(4, CAST(0 AS bit), '2019-07-02', 'https://upload.wikimedia.org/wikipedia/en/0/00/Spider-Man_No_Way_Home_poster.jpg', N'Spider-Man: Far From Home'),
(5, CAST(1 AS bit), '2100-01-01', 'https://upload.wikimedia.org/wikipedia/en/5/50/The_Matrix_Resurrections.jpg', N'The Matrix Resurrections');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'EnCartelera', N'FechaEstreno', N'PosterURL', N'Titulo') AND [object_id] = OBJECT_ID(N'[Peliculas]'))
    SET IDENTITY_INSERT [Peliculas] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CineId', N'FechaFin', N'FechaInicio', N'PorcentajeDescuento') AND [object_id] = OBJECT_ID(N'[CinesOfertas]'))
    SET IDENTITY_INSERT [CinesOfertas] ON;
INSERT INTO [CinesOfertas] ([Id], [CineId], [FechaFin], [FechaInicio], [PorcentajeDescuento])
VALUES (1, 1, '2024-10-25', '2024-10-18', 10.0),
(2, 4, '2024-10-23', '2024-10-18', 15.0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CineId', N'FechaFin', N'FechaInicio', N'PorcentajeDescuento') AND [object_id] = OBJECT_ID(N'[CinesOfertas]'))
    SET IDENTITY_INSERT [CinesOfertas] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GenerosIdentificador', N'PeliculasId') AND [object_id] = OBJECT_ID(N'[GeneroPelicula]'))
    SET IDENTITY_INSERT [GeneroPelicula] ON;
INSERT INTO [GeneroPelicula] ([GenerosIdentificador], [PeliculasId])
VALUES (1, 1),
(1, 3),
(1, 4),
(1, 5),
(2, 2),
(3, 3),
(3, 4),
(4, 1),
(4, 3),
(4, 4),
(4, 5),
(5, 5);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GenerosIdentificador', N'PeliculasId') AND [object_id] = OBJECT_ID(N'[GeneroPelicula]'))
    SET IDENTITY_INSERT [GeneroPelicula] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ActorId', N'PeliculaId', N'Orden', N'Personaje') AND [object_id] = OBJECT_ID(N'[PeliculasActores]'))
    SET IDENTITY_INSERT [PeliculasActores] ON;
INSERT INTO [PeliculasActores] ([ActorId], [PeliculaId], [Orden], [Personaje])
VALUES (3, 1, 2, N'Iron Man'),
(4, 1, 1, N'Capitán América'),
(7, 1, 3, N'Black Widow'),
(1, 3, 1, N'Peter Parker'),
(1, 4, 1, N'Peter Parker'),
(2, 4, 2, N'Samuel L. Jackson'),
(8, 5, 1, N'Neo');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ActorId', N'PeliculaId', N'Orden', N'Personaje') AND [object_id] = OBJECT_ID(N'[PeliculasActores]'))
    SET IDENTITY_INSERT [PeliculasActores] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CineId', N'Precio', N'TipoSalaDeCine') AND [object_id] = OBJECT_ID(N'[SalasDeCine]'))
    SET IDENTITY_INSERT [SalasDeCine] ON;
INSERT INTO [SalasDeCine] ([Id], [CineId], [Precio], [TipoSalaDeCine])
VALUES (1, 1, 220.0, 1),
(2, 1, 320.0, 2),
(3, 2, 200.0, 1),
(4, 2, 290.0, 2),
(5, 3, 250.0, 1),
(6, 3, 330.0, 2),
(7, 3, 450.0, 3),
(8, 4, 250.0, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CineId', N'Precio', N'TipoSalaDeCine') AND [object_id] = OBJECT_ID(N'[SalasDeCine]'))
    SET IDENTITY_INSERT [SalasDeCine] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PeliculasId', N'SalasDeCineId') AND [object_id] = OBJECT_ID(N'[PeliculaSalaDeCine]'))
    SET IDENTITY_INSERT [PeliculaSalaDeCine] ON;
INSERT INTO [PeliculaSalaDeCine] ([PeliculasId], [SalasDeCineId])
VALUES (5, 1),
(5, 2),
(5, 3),
(5, 4),
(5, 5),
(5, 6),
(5, 7);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PeliculasId', N'SalasDeCineId') AND [object_id] = OBJECT_ID(N'[PeliculaSalaDeCine]'))
    SET IDENTITY_INSERT [PeliculaSalaDeCine] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241018195359_DatosDePrueba', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Generos] ADD [EstaBorrado] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241029171421_GenerosBorradoSuave', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Logs] (
    [Id] uniqueidentifier NOT NULL,
    [Mensaje] nvarchar(max) NULL,
    CONSTRAINT [PK_Logs] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241029193052_Logs', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE UNIQUE INDEX [IX_Generos_Nombre] ON [Generos] ([Nombre]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241030155045_GeneroIndice', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP INDEX [IX_Generos_Nombre] ON [Generos];
GO

CREATE UNIQUE INDEX [IX_Generos_Nombre] ON [Generos] ([Nombre]) WHERE EstaBorrado = 'false';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241030190657_IndiceNombreFiltro', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalasDeCine]') AND [c].[name] = N'TipoSalaDeCine');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [SalasDeCine] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [SalasDeCine] ALTER COLUMN [TipoSalaDeCine] nvarchar(max) NOT NULL;
ALTER TABLE [SalasDeCine] ADD DEFAULT N'DosDimensiones' FOR [TipoSalaDeCine];
GO

UPDATE [SalasDeCine] SET [TipoSalaDeCine] = N'DosDimensiones'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [SalasDeCine] SET [TipoSalaDeCine] = N'TresDimensiones'
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

UPDATE [SalasDeCine] SET [TipoSalaDeCine] = N'DosDimensiones'
WHERE [Id] = 3;
SELECT @@ROWCOUNT;

GO

UPDATE [SalasDeCine] SET [TipoSalaDeCine] = N'TresDimensiones'
WHERE [Id] = 4;
SELECT @@ROWCOUNT;

GO

UPDATE [SalasDeCine] SET [TipoSalaDeCine] = N'DosDimensiones'
WHERE [Id] = 5;
SELECT @@ROWCOUNT;

GO

UPDATE [SalasDeCine] SET [TipoSalaDeCine] = N'TresDimensiones'
WHERE [Id] = 6;
SELECT @@ROWCOUNT;

GO

UPDATE [SalasDeCine] SET [TipoSalaDeCine] = N'CXC'
WHERE [Id] = 7;
SELECT @@ROWCOUNT;

GO

UPDATE [SalasDeCine] SET [TipoSalaDeCine] = N'DosDimensiones'
WHERE [Id] = 8;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241030193749_EjemploConversion', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalasDeCine] ADD [Moneda] nvarchar(max) NOT NULL DEFAULT N'';
GO

UPDATE [SalasDeCine] SET [Moneda] = N''
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [SalasDeCine] SET [Moneda] = N''
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

UPDATE [SalasDeCine] SET [Moneda] = N''
WHERE [Id] = 3;
SELECT @@ROWCOUNT;

GO

UPDATE [SalasDeCine] SET [Moneda] = N''
WHERE [Id] = 4;
SELECT @@ROWCOUNT;

GO

UPDATE [SalasDeCine] SET [Moneda] = N''
WHERE [Id] = 5;
SELECT @@ROWCOUNT;

GO

UPDATE [SalasDeCine] SET [Moneda] = N''
WHERE [Id] = 6;
SELECT @@ROWCOUNT;

GO

UPDATE [SalasDeCine] SET [Moneda] = N''
WHERE [Id] = 7;
SELECT @@ROWCOUNT;

GO

UPDATE [SalasDeCine] SET [Moneda] = N''
WHERE [Id] = 8;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241030232828_CampoMoneda', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO


                CREATE VIEW [dbo].[PeliculasConConteos]
                AS
                SELECT id, titulo,
                (SELECT count(*)
                FROM GeneroPelicula
                WHERE PeliculasId = Peliculas.Id) as CantidadGeneros,
                (SELECT count(DISTINCT CineId)
                FROM PeliculaSalaDeCine
                inner join SalasDeCine
                ON SalasDeCine.Id = PeliculaSalaDeCine.SalasDeCineId
                WHERE PeliculasId = Peliculas.Id) as CantidadCines,
                (SELECT count(*)
                FROM PeliculasActores
                WHERE PeliculaId = Peliculas.Id) as CantidadActores
                FROM Peliculas
                
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241105152911_VistaConteoPeliculas', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Generos] ADD [FechaCreacion] datetime2 NOT NULL DEFAULT (GetDate());
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241105165318_FechaCreacionGenero', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Actores] ADD [FotoURL] varchar(500) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241105190759_FotoActor', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [CinesOfertas] DROP CONSTRAINT [FK_CinesOfertas_Cines_CineId];
GO

DROP INDEX [IX_CinesOfertas_CineId] ON [CinesOfertas];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CinesOfertas]') AND [c].[name] = N'CineId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [CinesOfertas] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [CinesOfertas] ALTER COLUMN [CineId] int NULL;
GO

CREATE UNIQUE INDEX [IX_CinesOfertas_CineId] ON [CinesOfertas] ([CineId]) WHERE [CineId] IS NOT NULL;
GO

ALTER TABLE [CinesOfertas] ADD CONSTRAINT [FK_CinesOfertas_Cines_CineId] FOREIGN KEY ([CineId]) REFERENCES [Cines] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241105195116_CineIdNullable', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalasDeCine] DROP CONSTRAINT [FK_SalasDeCine_Cines_CineId];
GO

EXEC sp_rename N'[SalasDeCine].[CineId]', N'ElCine', N'COLUMN';
GO

EXEC sp_rename N'[SalasDeCine].[IX_SalasDeCine_CineId]', N'IX_SalasDeCine_ElCine', N'INDEX';
GO

UPDATE [CinesOfertas] SET [FechaFin] = '2024-11-13', [FechaInicio] = '2024-11-06'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [CinesOfertas] SET [FechaFin] = '2024-11-11', [FechaInicio] = '2024-11-06'
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

ALTER TABLE [SalasDeCine] ADD CONSTRAINT [FK_SalasDeCine_Cines_ElCine] FOREIGN KEY ([ElCine]) REFERENCES [Cines] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241106151758_CampoElCine', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Personas] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NULL,
    CONSTRAINT [PK_Personas] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Mensajes] (
    [Id] int NOT NULL IDENTITY,
    [Contenido] nvarchar(max) NULL,
    [EmisorId] int NOT NULL,
    [ReceptorId] int NOT NULL,
    CONSTRAINT [PK_Mensajes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Mensajes_Personas_EmisorId] FOREIGN KEY ([EmisorId]) REFERENCES [Personas] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Mensajes_Personas_ReceptorId] FOREIGN KEY ([ReceptorId]) REFERENCES [Personas] ([Id]) ON DELETE NO ACTION
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nombre') AND [object_id] = OBJECT_ID(N'[Personas]'))
    SET IDENTITY_INSERT [Personas] ON;
INSERT INTO [Personas] ([Id], [Nombre])
VALUES (1, N'Felipe'),
(2, N'Claudia');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nombre') AND [object_id] = OBJECT_ID(N'[Personas]'))
    SET IDENTITY_INSERT [Personas] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Contenido', N'EmisorId', N'ReceptorId') AND [object_id] = OBJECT_ID(N'[Mensajes]'))
    SET IDENTITY_INSERT [Mensajes] ON;
INSERT INTO [Mensajes] ([Id], [Contenido], [EmisorId], [ReceptorId])
VALUES (1, N'Hola, Claudia!', 1, 2),
(2, N'Hola, Felipe, ¿Cómo te va?', 2, 1),
(3, N'Todo bien, ¿Y tú?', 1, 2),
(4, N'Muy bien :)', 2, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Contenido', N'EmisorId', N'ReceptorId') AND [object_id] = OBJECT_ID(N'[Mensajes]'))
    SET IDENTITY_INSERT [Mensajes] OFF;
GO

CREATE INDEX [IX_Mensajes_EmisorId] ON [Mensajes] ([EmisorId]);
GO

CREATE INDEX [IX_Mensajes_ReceptorId] ON [Mensajes] ([ReceptorId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241106165024_EjemploPersona', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SalasDeCine] DROP CONSTRAINT [FK_SalasDeCine_Cines_ElCine];
GO

ALTER TABLE [SalasDeCine] ADD CONSTRAINT [FK_SalasDeCine_Cines_ElCine] FOREIGN KEY ([ElCine]) REFERENCES [Cines] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241106224959_NoPodemosBorrarCineConSalasDeCine', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Cines] ADD [CodigoDeEtica] nvarchar(max) NULL;
GO

ALTER TABLE [Cines] ADD [Historia] nvarchar(max) NULL;
GO

ALTER TABLE [Cines] ADD [Misiones] nvarchar(max) NULL;
GO

ALTER TABLE [Cines] ADD [Valores] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241107001117_CineDetalleTableSplitting', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Cines] ADD [Calle] nvarchar(max) NULL;
GO

ALTER TABLE [Cines] ADD [Pais] nvarchar(max) NULL;
GO

ALTER TABLE [Cines] ADD [Provincia] nvarchar(max) NULL;
GO

ALTER TABLE [Actores] ADD [BillingAddress_Calle] nvarchar(max) NULL;
GO

ALTER TABLE [Actores] ADD [BillingAddress_Pais] nvarchar(max) NULL;
GO

ALTER TABLE [Actores] ADD [BillingAddress_Provincia] nvarchar(max) NULL;
GO

ALTER TABLE [Actores] ADD [Calle] nvarchar(max) NULL;
GO

ALTER TABLE [Actores] ADD [Pais] nvarchar(max) NULL;
GO

ALTER TABLE [Actores] ADD [Provincia] nvarchar(max) NULL;
GO

UPDATE [CinesOfertas] SET [FechaFin] = '2024-11-14', [FechaInicio] = '2024-11-07'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [CinesOfertas] SET [FechaFin] = '2024-11-12', [FechaInicio] = '2024-11-07'
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241107190954_EjemploOwned', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Pagos] (
    [Id] int NOT NULL IDENTITY,
    [Monto] decimal(18,2) NOT NULL,
    [FechaTransaccion] date NOT NULL,
    [TipoPago] int NOT NULL,
    [CorreoElectronico] nvarchar(150) NULL,
    [Ultimos4Digitos] char(4) NULL,
    CONSTRAINT [PK_Pagos] PRIMARY KEY ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'FechaTransaccion', N'Monto', N'TipoPago', N'Ultimos4Digitos') AND [object_id] = OBJECT_ID(N'[Pagos]'))
    SET IDENTITY_INSERT [Pagos] ON;
INSERT INTO [Pagos] ([Id], [FechaTransaccion], [Monto], [TipoPago], [Ultimos4Digitos])
VALUES (1, '2022-01-06', 500.0, 2, '0123'),
(2, '2022-01-06', 120.0, 2, '1234');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'FechaTransaccion', N'Monto', N'TipoPago', N'Ultimos4Digitos') AND [object_id] = OBJECT_ID(N'[Pagos]'))
    SET IDENTITY_INSERT [Pagos] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CorreoElectronico', N'FechaTransaccion', N'Monto', N'TipoPago') AND [object_id] = OBJECT_ID(N'[Pagos]'))
    SET IDENTITY_INSERT [Pagos] ON;
INSERT INTO [Pagos] ([Id], [CorreoElectronico], [FechaTransaccion], [Monto], [TipoPago])
VALUES (3, N'felipe@hotmail.com', '2022-01-07', 157.0, 1),
(4, N'claudia@hotmail.com', '2022-01-07', 9.99, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CorreoElectronico', N'FechaTransaccion', N'Monto', N'TipoPago') AND [object_id] = OBJECT_ID(N'[Pagos]'))
    SET IDENTITY_INSERT [Pagos] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241107234544_HerenciaTPH', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Productos] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NULL,
    [Precio] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Productos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Merchandising] (
    [Id] int NOT NULL,
    [DisponibleEnInventario] bit NOT NULL,
    [Peso] float NOT NULL,
    [Volumen] float NOT NULL,
    [EsRopa] bit NOT NULL,
    [EsColeccionable] bit NOT NULL,
    CONSTRAINT [PK_Merchandising] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Merchandising_Productos_Id] FOREIGN KEY ([Id]) REFERENCES [Productos] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [PeliculasAlquilables] (
    [Id] int NOT NULL,
    [PeliculaId] int NOT NULL,
    CONSTRAINT [PK_PeliculasAlquilables] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PeliculasAlquilables_Productos_Id] FOREIGN KEY ([Id]) REFERENCES [Productos] ([Id]) ON DELETE CASCADE
);
GO

UPDATE [CinesOfertas] SET [FechaFin] = '2024-11-15', [FechaInicio] = '2024-11-08'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [CinesOfertas] SET [FechaFin] = '2024-11-13', [FechaInicio] = '2024-11-08'
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nombre', N'Precio') AND [object_id] = OBJECT_ID(N'[Productos]'))
    SET IDENTITY_INSERT [Productos] ON;
INSERT INTO [Productos] ([Id], [Nombre], [Precio])
VALUES (1, N'Spider-Man', 5.99),
(2, N'T-Shirt One Piece', 11.0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Nombre', N'Precio') AND [object_id] = OBJECT_ID(N'[Productos]'))
    SET IDENTITY_INSERT [Productos] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DisponibleEnInventario', N'EsColeccionable', N'EsRopa', N'Peso', N'Volumen') AND [object_id] = OBJECT_ID(N'[Merchandising]'))
    SET IDENTITY_INSERT [Merchandising] ON;
INSERT INTO [Merchandising] ([Id], [DisponibleEnInventario], [EsColeccionable], [EsRopa], [Peso], [Volumen])
VALUES (2, CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), 1.0E0, 1.0E0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DisponibleEnInventario', N'EsColeccionable', N'EsRopa', N'Peso', N'Volumen') AND [object_id] = OBJECT_ID(N'[Merchandising]'))
    SET IDENTITY_INSERT [Merchandising] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'PeliculaId') AND [object_id] = OBJECT_ID(N'[PeliculasAlquilables]'))
    SET IDENTITY_INSERT [PeliculasAlquilables] ON;
INSERT INTO [PeliculasAlquilables] ([Id], [PeliculaId])
VALUES (1, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'PeliculaId') AND [object_id] = OBJECT_ID(N'[PeliculasAlquilables]'))
    SET IDENTITY_INSERT [PeliculasAlquilables] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241108161735_HerenciaTPT', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Generos] ADD [Ejemplo] nvarchar(max) NULL;
GO

UPDATE [Generos] SET [Ejemplo] = NULL
WHERE [Identificador] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [Generos] SET [Ejemplo] = NULL
WHERE [Identificador] = 2;
SELECT @@ROWCOUNT;

GO

UPDATE [Generos] SET [Ejemplo] = NULL
WHERE [Identificador] = 3;
SELECT @@ROWCOUNT;

GO

UPDATE [Generos] SET [Ejemplo] = NULL
WHERE [Identificador] = 4;
SELECT @@ROWCOUNT;

GO

UPDATE [Generos] SET [Ejemplo] = NULL
WHERE [Identificador] = 5;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241108183827_GeneroEjemplo', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241108184604_EjemploNuevaCarpeta', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241108191918_Primera', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241108192143_Segunda', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Generos]') AND [c].[name] = N'Ejemplo');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Generos] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Generos] DROP COLUMN [Ejemplo];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241108234309_RemoverColumnaEjemplo', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Generos] ADD [Ejemplo] nvarchar(max) NULL;
GO

UPDATE [Generos] SET [Ejemplo] = NULL
WHERE [Identificador] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [Generos] SET [Ejemplo] = NULL
WHERE [Identificador] = 2;
SELECT @@ROWCOUNT;

GO

UPDATE [Generos] SET [Ejemplo] = NULL
WHERE [Identificador] = 3;
SELECT @@ROWCOUNT;

GO

UPDATE [Generos] SET [Ejemplo] = NULL
WHERE [Identificador] = 4;
SELECT @@ROWCOUNT;

GO

UPDATE [Generos] SET [Ejemplo] = NULL
WHERE [Identificador] = 5;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241109001717_ColumnaEjemplo', N'8.0.10');
GO

COMMIT;
GO

