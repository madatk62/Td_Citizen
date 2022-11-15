using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class Update_49_UpdateVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Seat",
                schema: "Catalog",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seat", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Seat_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalSchema: "Catalog",
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seat_VehicleId",
                schema: "Catalog",
                table: "Seat",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Seat",
                schema: "Catalog");
        }
    }
}
