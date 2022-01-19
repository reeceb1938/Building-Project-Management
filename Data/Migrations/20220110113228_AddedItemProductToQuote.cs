using Microsoft.EntityFrameworkCore.Migrations;

namespace NestLinkV2.Data.Migrations
{
    public partial class AddedItemProductToQuote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Products_ProductId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_ProductId",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Quotes");

            migrationBuilder.CreateTable(
                name: "QuoteProducts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    NetPrice = table.Column<decimal>(type: "Decimal(18, 2)", nullable: false),
                    VAT = table.Column<decimal>(type: "Decimal(18, 2)", nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    QuoteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuoteProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuoteProducts_Quotes_QuoteId",
                        column: x => x.QuoteId,
                        principalTable: "Quotes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuoteProducts_ProductId",
                table: "QuoteProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteProducts_QuoteId",
                table: "QuoteProducts",
                column: "QuoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuoteProducts");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Quotes",
                type: "Decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Quotes",
                type: "int",
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
    }
}
