using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class Update_62 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TenThuTuc",
                schema: "Catalog",
                table: "HoSoDienTus",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MaThuTuc",
                schema: "Catalog",
                table: "HoSoDienTus",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "MaLoaiHoSo",
                schema: "Catalog",
                table: "HoSoDienTus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaNhomHoSo",
                schema: "Catalog",
                table: "HoSoDienTus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenLoaiHoSo",
                schema: "Catalog",
                table: "HoSoDienTus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenNhomHoSo",
                schema: "Catalog",
                table: "HoSoDienTus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LoaiGiayToID",
                schema: "Catalog",
                table: "GiayToHoSoDienTus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NhomGiayToID",
                schema: "Catalog",
                table: "GiayToHoSoDienTus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoGiayTo",
                schema: "Catalog",
                table: "GiayToHoSoDienTus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenLoaiGiayTo",
                schema: "Catalog",
                table: "GiayToHoSoDienTus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenNhomGiayTo",
                schema: "Catalog",
                table: "GiayToHoSoDienTus",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaLoaiHoSo",
                schema: "Catalog",
                table: "HoSoDienTus");

            migrationBuilder.DropColumn(
                name: "MaNhomHoSo",
                schema: "Catalog",
                table: "HoSoDienTus");

            migrationBuilder.DropColumn(
                name: "TenLoaiHoSo",
                schema: "Catalog",
                table: "HoSoDienTus");

            migrationBuilder.DropColumn(
                name: "TenNhomHoSo",
                schema: "Catalog",
                table: "HoSoDienTus");

            migrationBuilder.DropColumn(
                name: "LoaiGiayToID",
                schema: "Catalog",
                table: "GiayToHoSoDienTus");

            migrationBuilder.DropColumn(
                name: "NhomGiayToID",
                schema: "Catalog",
                table: "GiayToHoSoDienTus");

            migrationBuilder.DropColumn(
                name: "SoGiayTo",
                schema: "Catalog",
                table: "GiayToHoSoDienTus");

            migrationBuilder.DropColumn(
                name: "TenLoaiGiayTo",
                schema: "Catalog",
                table: "GiayToHoSoDienTus");

            migrationBuilder.DropColumn(
                name: "TenNhomGiayTo",
                schema: "Catalog",
                table: "GiayToHoSoDienTus");

            migrationBuilder.AlterColumn<string>(
                name: "TenThuTuc",
                schema: "Catalog",
                table: "HoSoDienTus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaThuTuc",
                schema: "Catalog",
                table: "HoSoDienTus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
