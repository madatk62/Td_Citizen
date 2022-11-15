using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class Update_36_Education : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Schools_CommuneId",
                schema: "Catalog",
                table: "Schools",
                column: "CommuneId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_DistrictId",
                schema: "Catalog",
                table: "Schools",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_ProvinceId",
                schema: "Catalog",
                table: "Schools",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Areas_CommuneId",
                schema: "Catalog",
                table: "Schools",
                column: "CommuneId",
                principalSchema: "Catalog",
                principalTable: "Areas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Areas_DistrictId",
                schema: "Catalog",
                table: "Schools",
                column: "DistrictId",
                principalSchema: "Catalog",
                principalTable: "Areas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Areas_ProvinceId",
                schema: "Catalog",
                table: "Schools",
                column: "ProvinceId",
                principalSchema: "Catalog",
                principalTable: "Areas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Areas_CommuneId",
                schema: "Catalog",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Areas_DistrictId",
                schema: "Catalog",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Areas_ProvinceId",
                schema: "Catalog",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Schools_CommuneId",
                schema: "Catalog",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Schools_DistrictId",
                schema: "Catalog",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Schools_ProvinceId",
                schema: "Catalog",
                table: "Schools");
        }
    }
}
