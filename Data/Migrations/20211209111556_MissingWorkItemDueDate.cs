using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace NestLinkV2.Data.Migrations
{
    public partial class MissingWorkItemDueDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
               name: "DueWhen",
               table: "WorkItems",
               nullable: false,
               defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueWhen",
                table: "WorkItems");
        }
    }
}
