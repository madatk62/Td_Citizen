using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class Update_61 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Ma",
                schema: "Catalog",
                table: "LoaiHoSoDienTus",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "Catalog",
                table: "LoaiHoSoDienTus",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "Catalog",
                table: "LoaiHoSoDienTus");

            migrationBuilder.AlterColumn<string>(
                name: "Ma",
                schema: "Catalog",
                table: "LoaiHoSoDienTus",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);
        }
    }
}
