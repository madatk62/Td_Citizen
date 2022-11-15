using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class Update_63 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IDCongDan",
                schema: "Catalog",
                table: "GiayToHoSoDienTus",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDCongDan",
                schema: "Catalog",
                table: "GiayToHoSoDienTus");
        }
    }
}
