using Microsoft.EntityFrameworkCore.Migrations;

namespace NestLinkV2.Data.Migrations
{
    public partial class UpdateAttributesOnJobProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Products_ProductId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_ProductId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Jobs");

            migrationBuilder.CreateTable(
                name: "JobProducts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    NetPrice = table.Column<decimal>(type: "Decimal(18, 2)", nullable: false),
                    VAT = table.Column<decimal>(type: "Decimal(18, 2)", nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_JobProducts_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobProducts_JobId",
                table: "JobProducts",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobProducts_ProductId",
                table: "JobProducts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobProducts");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_ProductId",
                table: "Jobs",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Products_ProductId",
                table: "Jobs",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
