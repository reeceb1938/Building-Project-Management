using Microsoft.EntityFrameworkCore.Migrations;

namespace NestLinkV2.Data.Migrations
{
    public partial class AddedAssignmentRequireds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Sites_SiteId",
                table: "Assignments");

            migrationBuilder.AlterColumn<int>(
                name: "SiteId",
                table: "Assignments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Assignments",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Sites_SiteId",
                table: "Assignments",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Sites_SiteId",
                table: "Assignments");

            migrationBuilder.AlterColumn<int>(
                name: "SiteId",
                table: "Assignments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Assignments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Sites_SiteId",
                table: "Assignments",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
