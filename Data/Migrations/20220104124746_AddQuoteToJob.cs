using Microsoft.EntityFrameworkCore.Migrations;

namespace NestLinkV2.Data.Migrations
{
    public partial class AddQuoteToJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuoteId",
                table: "Jobs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_QuoteId",
                table: "Jobs",
                column: "QuoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Quotes_QuoteId",
                table: "Jobs",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Quotes_QuoteId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_QuoteId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "QuoteId",
                table: "Jobs");
        }
    }
}
