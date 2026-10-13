using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAnnotationsEFCore.Migrations
{
    /// <inheritdoc />
    public partial class CreacionEntidadComentario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPublicacion",
                schema: "blog",
                table: "Categorias",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InformacionAdicional",
                table: "Articulos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Comentarios",
                schema: "blog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Autor = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios",
                schema: "blog");

            migrationBuilder.DropColumn(
                name: "FechaPublicacion",
                schema: "blog",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "InformacionAdicional",
                table: "Articulos");
        }
    }
}
