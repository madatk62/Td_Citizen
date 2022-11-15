using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class Update_41_ThueNha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "Catalog",
                table: "ThueNhas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "Catalog",
                table: "ThueNhas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Catalog",
                table: "ThueNhas");

            migrationBuilder.DropColumn(
                name: "UserName",
                schema: "Catalog",
                table: "ThueNhas");
        }
    }
}
