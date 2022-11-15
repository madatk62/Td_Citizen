using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class Update_53_UpdateTrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Seat",
                schema: "Catalog",
                table: "Seat");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Catalog",
                table: "Seat",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "Catalog",
                table: "Seat",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seat",
                schema: "Catalog",
                table: "Seat",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Seat",
                schema: "Catalog",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Catalog",
                table: "Seat");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Catalog",
                table: "Seat",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seat",
                schema: "Catalog",
                table: "Seat",
                column: "Code");
        }
    }
}
