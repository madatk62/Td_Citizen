using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class Update_51_UpdateTrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TripId",
                schema: "Catalog",
                table: "Seat",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seat_TripId",
                schema: "Catalog",
                table: "Seat",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Trips_TripId",
                schema: "Catalog",
                table: "Seat",
                column: "TripId",
                principalSchema: "Catalog",
                principalTable: "Trips",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Trips_TripId",
                schema: "Catalog",
                table: "Seat");

            migrationBuilder.DropIndex(
                name: "IX_Seat_TripId",
                schema: "Catalog",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "TripId",
                schema: "Catalog",
                table: "Seat");
        }
    }
}
