using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class Update_58 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HoSoDienTus",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDCongDan = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TaiKhoanTao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenHoSo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaHoSo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenThuTuc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaThuTuc = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_HoSoDienTus", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoSoDienTus",
                schema: "Catalog");
        }
    }
}
