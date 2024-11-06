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
