using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class Update_56 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Seat",
                schema: "Catalog",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "Catalog",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seat",
                schema: "Catalog",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "Catalog",
                table: "Trips");
        }
    }
}
