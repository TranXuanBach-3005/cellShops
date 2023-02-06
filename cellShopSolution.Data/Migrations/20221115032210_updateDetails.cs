using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cellShopSolution.Data.Migrations
{
    public partial class updateDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SeoAlias",
                table: "ProductTranslations",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "ProductTranslations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 11, 15, 10, 22, 9, 588, DateTimeKind.Local).AddTicks(1106));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("55402ddf-8528-4dda-a2cc-2d2fa73909fe"),
                column: "ConcurrencyStamp",
                value: "15b0e130-1012-4581-962e-15d284589d1e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8049a3c9-f944-425a-b991-ffc5c2594218"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "23099cac-5fbb-4465-ad22-69d904a0232e", "AQAAAAEAACcQAAAAEIO9+Lr73D5Qo4SL3VYPrmaWqBFJDrocFuHkwZGwRnEBKHh3rX+/4cuk0p+A0lKPww==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SeoAlias",
                table: "ProductTranslations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "ProductTranslations",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 10, 11, 21, 50, 2, 702, DateTimeKind.Local).AddTicks(6344));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("55402ddf-8528-4dda-a2cc-2d2fa73909fe"),
                column: "ConcurrencyStamp",
                value: "c88eac1d-3915-4ed0-b07b-abfc6048eaa6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8049a3c9-f944-425a-b991-ffc5c2594218"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0e2fafc8-8192-4c08-a32c-16be09fe4a51", "AQAAAAEAACcQAAAAEOunMadIKctO9sPx2v4ASPkol5Y2xmCjhdBSYMG+oxq51bW+WDkLnvb+WGZ1Mc/YBQ==" });
        }
    }
}
