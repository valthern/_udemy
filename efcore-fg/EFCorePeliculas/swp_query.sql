CREATE FUNCTION [dbo].[PeliculaConConteo]
(
	@peliculaId int
)
RETURNS TABLE
AS
RETURN
(
	SELECT 
		id,
		titulo,
		(SELECT 
			count(*)
		FROM GeneroPelicula
		WHERE PeliculasId = Peliculas.Id
		) as CantidadGeneros,
		(SELECT 
			count(DISTINCT ElCine)
		FROM PeliculaSalaDeCine
		INNER JOIN SalasDeCine
			ON SalasDeCine.Id = PeliculaSalaDeCine.SalasDeCineId
		WHERE PeliculasId = Peliculas.Id
		) as CantidadCines,
		(SELECT 
			count(*)
		FROM PeliculasActores
		WHERE PeliculaId = Peliculas.Id
		) as CantidadActores
	FROM Peliculas
	WHERE Id = @peliculaId
)