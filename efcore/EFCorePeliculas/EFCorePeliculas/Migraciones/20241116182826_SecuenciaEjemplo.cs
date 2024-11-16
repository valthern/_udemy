using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculas.Migraciones
{
    /// <inheritdoc />
    public partial class SecuenciaEjemplo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "factura");

            migrationBuilder.CreateSequence<int>(
                name: "NumeroFactura",
                schema: "factura");

            migrationBuilder.AddColumn<int>(
                name: "NumeroFactura",
                table: "Facturas",
                type: "int",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR factura.NumeroFactura");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroFactura",
                table: "Facturas");

            migrationBuilder.DropSequence(
                name: "NumeroFactura",
                schema: "factura");
        }
    }
}
