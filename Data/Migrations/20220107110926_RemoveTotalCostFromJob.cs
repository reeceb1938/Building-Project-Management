using Microsoft.EntityFrameworkCore.Migrations;

namespace NestLinkV2.Data.Migrations
{
    public partial class RemoveTotalCostFromJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCost",
                table: "Jobs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalCost",
                table: "Jobs",
                type: "Decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
