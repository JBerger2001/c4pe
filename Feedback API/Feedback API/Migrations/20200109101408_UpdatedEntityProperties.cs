using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Feedback_API.Migrations
{
    public partial class UpdatedEntityProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PlaceTypes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Places",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Places",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ID",
                keyValue: 1L,
                column: "Time",
                value: new DateTime(2020, 1, 9, 11, 14, 7, 704, DateTimeKind.Local).AddTicks(3781));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ID",
                keyValue: 2L,
                column: "Time",
                value: new DateTime(2020, 1, 9, 11, 14, 7, 707, DateTimeKind.Local).AddTicks(5033));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PlaceTypes",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Places",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Places",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ID",
                keyValue: 1L,
                column: "Time",
                value: new DateTime(2020, 1, 9, 10, 55, 21, 832, DateTimeKind.Local).AddTicks(8812));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ID",
                keyValue: 2L,
                column: "Time",
                value: new DateTime(2020, 1, 9, 10, 55, 21, 836, DateTimeKind.Local).AddTicks(1651));
        }
    }
}
