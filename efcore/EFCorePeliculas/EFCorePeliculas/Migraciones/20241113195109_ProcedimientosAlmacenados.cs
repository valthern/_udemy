using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculas.Migraciones
{
    /// <inheritdoc />
    public partial class ProcedimientosAlmacenados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE PROCEDURE Generos_ObtenerPorId
            @id int
            AS
            BEGIN
	            SET NOCOUNT ON;

	            SELECT 
		            *
	            FROM Generos
	            WHERE Identificador = @Id;
            END
            ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE Generos_Insertar
	        @nombre nvarchar(150),
	        @id int output
	        AS
	        BEGIN
		        SET NOCOUNT ON;

		        INSERT INTO Generos
			        (Nombre)
			        VALUES
			        (@nombre);

		        SELECT @id = SCOPE_IDENTITY();
	        END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[Generos_ObtenerPorId]");
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[Generos_Insertar]");
        }
    }
}
