using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorEx.DAL.Migrations
{
    public partial class shop2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductEpisodes_ProductId",
                table: "ProductEpisodes");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEpisodes_ProductId",
                table: "ProductEpisodes",
                column: "ProductId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductEpisodes_ProductId",
                table: "ProductEpisodes");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEpisodes_ProductId",
                table: "ProductEpisodes",
                column: "ProductId");
        }
    }
}
