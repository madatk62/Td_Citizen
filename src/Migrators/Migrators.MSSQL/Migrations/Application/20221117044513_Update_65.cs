using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class Update_65 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaLinhVuc",
                schema: "Catalog",
                table: "HoSoDienTus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenLinhVuc",
                schema: "Catalog",
                table: "HoSoDienTus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GiayToCaNhanID",
                schema: "Catalog",
                table: "GiayToHoSoDienTus",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaLinhVuc",
                schema: "Catalog",
                table: "HoSoDienTus");

            migrationBuilder.DropColumn(
                name: "TenLinhVuc",
                schema: "Catalog",
                table: "HoSoDienTus");

            migrationBuilder.DropColumn(
                name: "GiayToCaNhanID",
                schema: "Catalog",
                table: "GiayToHoSoDienTus");
        }
    }
}
