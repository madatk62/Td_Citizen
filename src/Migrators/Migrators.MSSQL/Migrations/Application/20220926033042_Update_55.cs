using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class Update_55 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Seat",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Ticket",
                schema: "Catalog");

            migrationBuilder.CreateTable(
                name: "Passengers",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PassengerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Seat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalSchema: "Catalog",
                        principalTable: "Passengers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Trips_TripId",
                        column: x => x.TripId,
                        principalSchema: "Catalog",
                        principalTable: "Trips",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PassengerId",
                schema: "Catalog",
                table: "Tickets",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TripId",
                schema: "Catalog",
                table: "Tickets",
                column: "TripId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Passengers",
                schema: "Catalog");

            migrationBuilder.CreateTable(
                name: "Ticket",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Seat",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: true),
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seat_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalSchema: "Catalog",
                        principalTable: "Ticket",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Seat_Trips_TripId",
                        column: x => x.TripId,
                        principalSchema: "Catalog",
                        principalTable: "Trips",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Seat_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalSchema: "Catalog",
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seat_TicketId",
                schema: "Catalog",
                table: "Seat",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_TripId",
                schema: "Catalog",
                table: "Seat",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_VehicleId",
                schema: "Catalog",
                table: "Seat",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TripId",
                schema: "Catalog",
                table: "Ticket",
                column: "TripId");
        }
    }
}
