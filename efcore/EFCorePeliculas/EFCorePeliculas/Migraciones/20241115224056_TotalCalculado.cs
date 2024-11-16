using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculas.Migraciones
{
    /// <inheritdoc />
    public partial class TotalCalculado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "FacturaDetalles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "FacturaDetalles",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                computedColumnSql: "Precio * Cantidad");

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 5,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 6,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 7,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 8,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 9,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 10,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 11,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 12,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 13,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 14,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 15,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 16,
                column: "Cantidad",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "FacturaDetalles");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "FacturaDetalles");
        }
    }
}
