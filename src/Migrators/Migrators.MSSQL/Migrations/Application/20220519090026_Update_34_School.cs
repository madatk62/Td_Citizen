using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class Update_34_School : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District",
                schema: "Catalog",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "Province",
                schema: "Catalog",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "Ward",
                schema: "Catalog",
                table: "Schools");

            migrationBuilder.AddColumn<Guid>(
                name: "CommuneId",
                schema: "Catalog",
                table: "Schools",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DistrictId",
                schema: "Catalog",
                table: "Schools",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProvinceId",
                schema: "Catalog",
                table: "Schools",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolTypeId",
                schema: "Catalog",
                table: "Schools",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SchoolTypes",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
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
                    table.PrimaryKey("PK_SchoolTypes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolTypes",
                schema: "Catalog");

            migrationBuilder.DropColumn(
                name: "CommuneId",
                schema: "Catalog",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                schema: "Catalog",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                schema: "Catalog",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "SchoolTypeId",
                schema: "Catalog",
                table: "Schools");

            migrationBuilder.AddColumn<string>(
                name: "District",
                schema: "Catalog",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                schema: "Catalog",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ward",
                schema: "Catalog",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
