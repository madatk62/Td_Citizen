using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class Update_44_CoCauToChuc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrganizationUnits",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    FullCode = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    ParentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_OrganizationUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationUnits_OrganizationUnits_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Catalog",
                        principalTable: "OrganizationUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrganizationUnitUsers",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
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
                    table.PrimaryKey("PK_OrganizationUnitUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationUnitUsers_OrganizationUnits_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalSchema: "Catalog",
                        principalTable: "OrganizationUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUnits_ParentId",
                schema: "Catalog",
                table: "OrganizationUnits",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUnitUsers_OrganizationUnitId",
                schema: "Catalog",
                table: "OrganizationUnitUsers",
                column: "OrganizationUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationUnitUsers",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "OrganizationUnits",
                schema: "Catalog");
        }
    }
}
