using Microsoft.EntityFrameworkCore.Migrations;

namespace NestLinkV2.Data.Migrations
{
    public partial class AddedProductTypeAndProductPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "VAT");

            migrationBuilder.AddColumn<decimal>(
                name: "NetPrice",
                table: "Products",
                type: "Decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "Products",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "Product" });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "ID", "Name" },
                values: new object[] { 2, "Part" });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "ID", "Name" },
                values: new object[] { 3, "Labour" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "NetPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "VAT",
                table: "Products",
                newName: "Price");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Description", "Name", "Price" },
                values: new object[] { 1, "This is a test product", "Test Product", 2.15m });
        }
    }
}
