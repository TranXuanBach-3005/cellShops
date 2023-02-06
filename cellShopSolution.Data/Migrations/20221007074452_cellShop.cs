using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cellShopSolution.Data.Migrations
{
    public partial class cellShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 10, 3, 9, 2, 58, 951, DateTimeKind.Local).AddTicks(824));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("55402ddf-8528-4dda-a2cc-2d2fa73909fe"),
                column: "ConcurrencyStamp",
                value: "e5395ed0-0263-4b9e-a3f6-1ecfa447ddf0");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8049a3c9-f944-425a-b991-ffc5c2594218"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7cc6f701-4ee9-436f-9db9-e30c5177509d", "AQAAAAEAACcQAAAAEKDMaa4iBAeTkPd2yw3q8Ik3JZk92G4+9/JRphwszdgkTgNdyF0Q3hxvX8iijH5CZw==" });
        }
    }
}
