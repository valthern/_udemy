using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelacionesConEFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnComentarioAprobado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Aprobado",
                schema: "blog",
                table: "Comentarios",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aprobado",
                schema: "blog",
                table: "Comentarios");
        }
    }
}
