using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelacionesConEFCore.Migrations
{
    /// <inheritdoc />
    public partial class RelationArticuloComentario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArticuloId",
                schema: "blog",
                table: "Comentarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_ArticuloId",
                schema: "blog",
                table: "Comentarios",
                column: "ArticuloId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Articulos_ArticuloId",
                schema: "blog",
                table: "Comentarios",
                column: "ArticuloId",
                principalSchema: "blog",
                principalTable: "Articulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Articulos_ArticuloId",
                schema: "blog",
                table: "Comentarios");

            migrationBuilder.DropIndex(
                name: "IX_Comentarios_ArticuloId",
                schema: "blog",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "ArticuloId",
                schema: "blog",
                table: "Comentarios");
        }
    }
}
