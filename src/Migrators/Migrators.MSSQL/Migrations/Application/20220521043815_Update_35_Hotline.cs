using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class Update_35_Hotline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CommuneId",
                schema: "Catalog",
                table: "Hotlines",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Catalog",
                table: "Hotlines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DistrictId",
                schema: "Catalog",
                table: "Hotlines",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProvinceId",
                schema: "Catalog",
                table: "Hotlines",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotlines_CommuneId",
                schema: "Catalog",
                table: "Hotlines",
                column: "CommuneId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotlines_DistrictId",
                schema: "Catalog",
                table: "Hotlines",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotlines_ProvinceId",
                schema: "Catalog",
                table: "Hotlines",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotlines_Areas_CommuneId",
                schema: "Catalog",
                table: "Hotlines",
                column: "CommuneId",
                principalSchema: "Catalog",
                principalTable: "Areas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotlines_Areas_DistrictId",
                schema: "Catalog",
                table: "Hotlines",
                column: "DistrictId",
                principalSchema: "Catalog",
                principalTable: "Areas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotlines_Areas_ProvinceId",
                schema: "Catalog",
                table: "Hotlines",
                column: "ProvinceId",
                principalSchema: "Catalog",
                principalTable: "Areas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotlines_Areas_CommuneId",
                schema: "Catalog",
                table: "Hotlines");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotlines_Areas_DistrictId",
                schema: "Catalog",
                table: "Hotlines");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotlines_Areas_ProvinceId",
                schema: "Catalog",
                table: "Hotlines");

            migrationBuilder.DropIndex(
                name: "IX_Hotlines_CommuneId",
                schema: "Catalog",
                table: "Hotlines");

            migrationBuilder.DropIndex(
                name: "IX_Hotlines_DistrictId",
                schema: "Catalog",
                table: "Hotlines");

            migrationBuilder.DropIndex(
                name: "IX_Hotlines_ProvinceId",
                schema: "Catalog",
                table: "Hotlines");

            migrationBuilder.DropColumn(
                name: "CommuneId",
                schema: "Catalog",
                table: "Hotlines");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Catalog",
                table: "Hotlines");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                schema: "Catalog",
                table: "Hotlines");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                schema: "Catalog",
                table: "Hotlines");
        }
    }
}
