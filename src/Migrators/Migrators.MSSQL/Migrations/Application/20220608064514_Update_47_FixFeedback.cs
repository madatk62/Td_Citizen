using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class Update_47_FixFeedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DocId",
                schema: "Catalog",
                table: "Feedbacks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "Catalog",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocId",
                schema: "Catalog",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "Catalog",
                table: "Feedbacks");
        }
    }
}
