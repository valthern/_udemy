using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculas.Migraciones
{
    /// <inheritdoc />
    public partial class TVF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
				CREATE FUNCTION [dbo].[PeliculaConConteos]
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
			");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP FUNCTION [dbo].[PeliculaConConteos]");
        }
    }
}
