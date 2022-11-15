using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class Update_59 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GiayToHoSoDienTus",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoSoDienTuID = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    MaHoSoDienTu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenGiayTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaGiayTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DinhKem = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_GiayToHoSoDienTus", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiayToHoSoDienTus",
                schema: "Catalog");
        }
    }
}
