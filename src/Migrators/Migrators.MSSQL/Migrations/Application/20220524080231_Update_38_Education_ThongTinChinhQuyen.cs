using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class Update_38_Education_ThongTinChinhQuyen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EduDocumentCatalogues",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    CoverImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Order = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_EduDocumentCatalogues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EduDocumentTypes",
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
                    table.PrimaryKey("PK_EduDocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GovNewCategories",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Order = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_GovNewCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EduDocumentCategories",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    CoverImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Order = table.Column<int>(type: "int", nullable: true),
                    EduDocumentCatalogueId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_EduDocumentCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EduDocumentCategories_EduDocumentCatalogues_EduDocumentCatalogueId",
                        column: x => x.EduDocumentCatalogueId,
                        principalSchema: "Catalog",
                        principalTable: "EduDocumentCatalogues",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GovNews",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Actor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Files = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewQuantity = table.Column<int>(type: "int", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: true),
                    IsStar = table.Column<bool>(type: "bit", nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: true),
                    IsNotification = table.Column<bool>(type: "bit", nullable: true),
                    GovNewCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProvinceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DistrictId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommuneId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_GovNews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GovNews_Areas_CommuneId",
                        column: x => x.CommuneId,
                        principalSchema: "Catalog",
                        principalTable: "Areas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GovNews_Areas_DistrictId",
                        column: x => x.DistrictId,
                        principalSchema: "Catalog",
                        principalTable: "Areas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GovNews_Areas_ProvinceId",
                        column: x => x.ProvinceId,
                        principalSchema: "Catalog",
                        principalTable: "Areas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GovNews_GovNewCategories_GovNewCategoryId",
                        column: x => x.GovNewCategoryId,
                        principalSchema: "Catalog",
                        principalTable: "GovNewCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EduDocument",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ViewQuantity = table.Column<int>(type: "int", nullable: true),
                    DownloadQuantity = table.Column<int>(type: "int", nullable: true),
                    IsStar = table.Column<bool>(type: "bit", nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: true),
                    EduDocumentCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EduDocumentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_EduDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EduDocument_EduDocumentCategories_EduDocumentCategoryId",
                        column: x => x.EduDocumentCategoryId,
                        principalSchema: "Catalog",
                        principalTable: "EduDocumentCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EduDocument_EduDocumentTypes_EduDocumentTypeId",
                        column: x => x.EduDocumentTypeId,
                        principalSchema: "Catalog",
                        principalTable: "EduDocumentTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EduDocument_EduDocumentCategoryId",
                schema: "Catalog",
                table: "EduDocument",
                column: "EduDocumentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EduDocument_EduDocumentTypeId",
                schema: "Catalog",
                table: "EduDocument",
                column: "EduDocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EduDocumentCategories_EduDocumentCatalogueId",
                schema: "Catalog",
                table: "EduDocumentCategories",
                column: "EduDocumentCatalogueId");

            migrationBuilder.CreateIndex(
                name: "IX_GovNews_CommuneId",
                schema: "Catalog",
                table: "GovNews",
                column: "CommuneId");

            migrationBuilder.CreateIndex(
                name: "IX_GovNews_DistrictId",
                schema: "Catalog",
                table: "GovNews",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_GovNews_GovNewCategoryId",
                schema: "Catalog",
                table: "GovNews",
                column: "GovNewCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GovNews_ProvinceId",
                schema: "Catalog",
                table: "GovNews",
                column: "ProvinceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EduDocument",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "GovNews",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "EduDocumentCategories",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "EduDocumentTypes",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "GovNewCategories",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "EduDocumentCatalogues",
                schema: "Catalog");
        }
    }
}
