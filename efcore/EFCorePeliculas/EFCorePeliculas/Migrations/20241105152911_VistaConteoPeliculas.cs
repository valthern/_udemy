using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculas.Migrations
{
    /// <inheritdoc />
    public partial class VistaConteoPeliculas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
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
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW [dbo].[PeliculasConConteos]");
        }
    }
}
