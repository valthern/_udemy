using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelacionesConEFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityEtiqueta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Etiquetas",
                schema: "blog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etiquetas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticuloEtiqueta",
                schema: "blog",
                columns: table => new
                {
                    ArticulosId = table.Column<int>(type: "int", nullable: false),
                    EtiquetasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticuloEtiqueta", x => new { x.ArticulosId, x.EtiquetasId });
                    table.ForeignKey(
                        name: "FK_ArticuloEtiqueta_Articulos_ArticulosId",
                        column: x => x.ArticulosId,
                        principalSchema: "blog",
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticuloEtiqueta_Etiquetas_EtiquetasId",
                        column: x => x.EtiquetasId,
                        principalSchema: "blog",
                        principalTable: "Etiquetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticuloEtiqueta_EtiquetasId",
                schema: "blog",
                table: "ArticuloEtiqueta",
                column: "EtiquetasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticuloEtiqueta",
                schema: "blog");

            migrationBuilder.DropTable(
                name: "Etiquetas",
                schema: "blog");
        }
    }
}
