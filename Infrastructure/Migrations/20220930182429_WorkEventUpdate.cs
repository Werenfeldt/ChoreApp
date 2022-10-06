using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class WorkEventUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkEvents",
                table: "WorkEvents");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateDone",
                table: "WorkEvents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "WorkEvents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkEvents",
                table: "WorkEvents",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkEvents",
                table: "WorkEvents");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "WorkEvents");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateDone",
                table: "WorkEvents",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkEvents",
                table: "WorkEvents",
                columns: new[] { "Id", "AssignedToUserId", "ChoreId" });
        }
    }
}
