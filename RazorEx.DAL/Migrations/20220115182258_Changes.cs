using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorEx.DAL.Migrations
{
    public partial class Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Posts_PostId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Questions",
                newName: "ProductsId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_PostId",
                table: "Questions",
                newName: "IX_Questions_ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Products_ProductsId",
                table: "Questions",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Products_ProductsId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "Questions",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_ProductsId",
                table: "Questions",
                newName: "IX_Questions_PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Posts_PostId",
                table: "Questions",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
