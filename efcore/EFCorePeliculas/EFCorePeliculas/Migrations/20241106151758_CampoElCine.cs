﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculas.Migrations
{
    /// <inheritdoc />
    public partial class CampoElCine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalasDeCine_Cines_CineId",
                table: "SalasDeCine");

            migrationBuilder.RenameColumn(
                name: "CineId",
                table: "SalasDeCine",
                newName: "ElCine");

            migrationBuilder.RenameIndex(
                name: "IX_SalasDeCine_CineId",
                table: "SalasDeCine",
                newName: "IX_SalasDeCine_ElCine");

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2024, 11, 13, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 11, 6, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 11, 6, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.AddForeignKey(
                name: "FK_SalasDeCine_Cines_ElCine",
                table: "SalasDeCine",
                column: "ElCine",
                principalTable: "Cines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalasDeCine_Cines_ElCine",
                table: "SalasDeCine");

            migrationBuilder.RenameColumn(
                name: "ElCine",
                table: "SalasDeCine",
                newName: "CineId");

            migrationBuilder.RenameIndex(
                name: "IX_SalasDeCine_ElCine",
                table: "SalasDeCine",
                newName: "IX_SalasDeCine_CineId");

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2024, 11, 12, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2024, 11, 10, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.AddForeignKey(
                name: "FK_SalasDeCine_Cines_CineId",
                table: "SalasDeCine",
                column: "CineId",
                principalTable: "Cines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
