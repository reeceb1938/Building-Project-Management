using Microsoft.EntityFrameworkCore.Migrations;

namespace NestLinkV2.Data.Migrations
{
    public partial class AddProductToQuote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Quotes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_ProductId",
                table: "Quotes",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Products_ProductId",
                table: "Quotes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Products_ProductId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_ProductId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Quotes");
        }
    }
}
