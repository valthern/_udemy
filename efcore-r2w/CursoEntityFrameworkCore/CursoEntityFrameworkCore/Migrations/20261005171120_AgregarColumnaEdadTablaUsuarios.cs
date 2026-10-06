using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoEntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class AgregarColumnaEdadTablaUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Edad",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Edad",
                table: "Usuarios");
        }
    }
}
