using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorEx.DAL.Migrations
{
    public partial class ShopLevelStatus1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductLevel_ProductLevelLevelId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductStatus_ProductStatusStatusId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductStatus",
                table: "ProductStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductLevel",
                table: "ProductLevel");

            migrationBuilder.RenameTable(
                name: "ProductStatus",
                newName: "ProductStatuses");

            migrationBuilder.RenameTable(
                name: "ProductLevel",
                newName: "ProductLevels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductStatuses",
                table: "ProductStatuses",
                column: "StatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductLevels",
                table: "ProductLevels",
                column: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductLevels_ProductLevelLevelId",
                table: "Products",
                column: "ProductLevelLevelId",
                principalTable: "ProductLevels",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductStatuses_ProductStatusStatusId",
                table: "Products",
                column: "ProductStatusStatusId",
                principalTable: "ProductStatuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductLevels_ProductLevelLevelId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductStatuses_ProductStatusStatusId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductStatuses",
                table: "ProductStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductLevels",
                table: "ProductLevels");

            migrationBuilder.RenameTable(
                name: "ProductStatuses",
                newName: "ProductStatus");

            migrationBuilder.RenameTable(
                name: "ProductLevels",
                newName: "ProductLevel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductStatus",
                table: "ProductStatus",
                column: "StatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductLevel",
                table: "ProductLevel",
                column: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductLevel_ProductLevelLevelId",
                table: "Products",
                column: "ProductLevelLevelId",
                principalTable: "ProductLevel",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductStatus_ProductStatusStatusId",
                table: "Products",
                column: "ProductStatusStatusId",
                principalTable: "ProductStatus",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
