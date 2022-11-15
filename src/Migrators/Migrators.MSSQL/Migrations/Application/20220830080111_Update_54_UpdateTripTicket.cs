using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class Update_54_UpdateTripTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                schema: "Catalog",
                table: "Seat",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ticket",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_Trips_TripId",
                        column: x => x.TripId,
                        principalSchema: "Catalog",
                        principalTable: "Trips",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seat_TicketId",
                schema: "Catalog",
                table: "Seat",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TripId",
                schema: "Catalog",
                table: "Ticket",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Ticket_TicketId",
                schema: "Catalog",
                table: "Seat",
                column: "TicketId",
                principalSchema: "Catalog",
                principalTable: "Ticket",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Ticket_TicketId",
                schema: "Catalog",
                table: "Seat");

            migrationBuilder.DropTable(
                name: "Ticket",
                schema: "Catalog");

            migrationBuilder.DropIndex(
                name: "IX_Seat_TicketId",
                schema: "Catalog",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "TicketId",
                schema: "Catalog",
                table: "Seat");
        }
    }
}
