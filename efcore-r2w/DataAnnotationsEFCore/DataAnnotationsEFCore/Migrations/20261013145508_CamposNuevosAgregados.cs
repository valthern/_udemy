using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAnnotationsEFCore.Migrations
{
    /// <inheritdoc />
    public partial class CamposNuevosAgregados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Categorias",
                schema: "blog",
                comment: "Tabla de Categorías del Blog");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "blog",
                table: "Usuarios",
                type: "varchar(150)",
                unicode: false,
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                schema: "blog",
                table: "Categorias",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                comment: "Nombre de la categoría del blog",
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublicadoEn",
                table: "Articulos",
                type: "datetime2(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioPublicidad",
                table: "Articulos",
                type: "decimal(12,2)",
                precision: 12,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_Slug",
                schema: "blog",
                table: "Categorias",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categorias_Slug",
                schema: "blog",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "PrecioPublicidad",
                table: "Articulos");

            migrationBuilder.AlterTable(
                name: "Categorias",
                schema: "blog",
                oldComment: "Tabla de Categorías del Blog");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "blog",
                table: "Usuarios",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldUnicode: false,
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                schema: "blog",
                table: "Categorias",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80,
                oldComment: "Nombre de la categoría del blog");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublicadoEn",
                table: "Articulos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldPrecision: 0);
        }
    }
}
