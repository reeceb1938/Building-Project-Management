using Microsoft.EntityFrameworkCore.Migrations;

namespace NestLinkV2.Data.Migrations
{
    public partial class UpdatedQuoteStatusesForSurvey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EventTypes",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 8, "Quote Surveyed" },
                    { 9, "Quote Approved" },
                    { 10, "Quote Declined" }
                });

            migrationBuilder.UpdateData(
                table: "QuoteStatuses",
                keyColumn: "ID",
                keyValue: 3,
                column: "Name",
                value: "Awaiting Approval");

            migrationBuilder.InsertData(
                table: "QuoteStatuses",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 4, "Quote Approved" },
                    { 5, "Quote Declined" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "QuoteStatuses",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "QuoteStatuses",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "QuoteStatuses",
                keyColumn: "ID",
                keyValue: 3,
                column: "Name",
                value: "Completed");
        }
    }
}
