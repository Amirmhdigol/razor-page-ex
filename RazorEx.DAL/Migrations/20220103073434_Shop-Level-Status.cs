using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorEx.DAL.Migrations
{
    public partial class ShopLevelStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Statuses",
                table: "Products",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "Levels",
                table: "Products",
                newName: "LevelId");

            migrationBuilder.AddColumn<int>(
                name: "ProductLevelLevelId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductStatusStatusId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductLevel",
                columns: table => new
                {
                    LevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLevel", x => x.LevelId);
                });

            migrationBuilder.CreateTable(
                name: "ProductStatus",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStatus", x => x.StatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductLevelLevelId",
                table: "Products",
                column: "ProductLevelLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductStatusStatusId",
                table: "Products",
                column: "ProductStatusStatusId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductLevel_ProductLevelLevelId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductStatus_ProductStatusStatusId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductLevel");

            migrationBuilder.DropTable(
                name: "ProductStatus");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductLevelLevelId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductStatusStatusId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductLevelLevelId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductStatusStatusId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Products",
                newName: "Statuses");

            migrationBuilder.RenameColumn(
                name: "LevelId",
                table: "Products",
                newName: "Levels");
        }
    }
}
