using Microsoft.EntityFrameworkCore.Migrations;

namespace NestLinkV2.Data.Migrations
{
    public partial class AddQuoteStatusesToEventTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EventTypes",
                columns: new[] { "ID", "Name" },
                values: new object[] { 6, "Quote Created" });

            migrationBuilder.InsertData(
                table: "EventTypes",
                columns: new[] { "ID", "Name" },
                values: new object[] { 7, "Quote Survey Scheduled" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "ID",
                keyValue: 7);
        }
    }
}
