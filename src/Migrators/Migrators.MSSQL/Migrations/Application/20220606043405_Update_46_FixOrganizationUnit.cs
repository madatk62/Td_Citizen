using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class Update_46_FixOrganizationUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AreaId",
                schema: "Catalog",
                table: "OrganizationUnits",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "Catalog",
                table: "OrganizationUnits",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUnits_AreaId",
                schema: "Catalog",
                table: "OrganizationUnits",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUnits_Areas_AreaId",
                schema: "Catalog",
                table: "OrganizationUnits",
                column: "AreaId",
                principalSchema: "Catalog",
                principalTable: "Areas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUnits_Areas_AreaId",
                schema: "Catalog",
                table: "OrganizationUnits");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationUnits_AreaId",
                schema: "Catalog",
                table: "OrganizationUnits");

            migrationBuilder.DropColumn(
                name: "AreaId",
                schema: "Catalog",
                table: "OrganizationUnits");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "Catalog",
                table: "OrganizationUnits");
        }
    }
}
