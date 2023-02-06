using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cellShopSolution.Data.Migrations
{
    public partial class UpdateCodeSile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Products",
                type: "bit",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 10, 7, 14, 44, 52, 17, DateTimeKind.Local).AddTicks(2077));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("55402ddf-8528-4dda-a2cc-2d2fa73909fe"),
                column: "ConcurrencyStamp",
                value: "9ec204c5-fd5d-45b7-9076-ed6611f3d831");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8049a3c9-f944-425a-b991-ffc5c2594218"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1e6c62b0-bbff-44e4-9711-e6d58bffa43b", "AQAAAAEAACcQAAAAEP6vrVAviiNr2uABPYFY110S+9xxnqBEkUvr87lO0TK86Yfwikh2LXCoNz6Q6zMafA==" });
        }
    }
}
