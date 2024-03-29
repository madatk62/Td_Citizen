﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class Update_64 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IDCongDan",
                schema: "Catalog",
                table: "NhomHoSoDienTus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IDCongDan",
                schema: "Catalog",
                table: "LoaiHoSoDienTus",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDCongDan",
                schema: "Catalog",
                table: "NhomHoSoDienTus");

            migrationBuilder.DropColumn(
                name: "IDCongDan",
                schema: "Catalog",
                table: "LoaiHoSoDienTus");
        }
    }
}
