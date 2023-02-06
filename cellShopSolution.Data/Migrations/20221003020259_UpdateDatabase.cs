using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cellShopSolution.Data.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 8, 30, 22, 32, 6, 709, DateTimeKind.Local).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("55402ddf-8528-4dda-a2cc-2d2fa73909fe"),
                column: "ConcurrencyStamp",
                value: "cc5163d8-0b1e-41ac-b3e6-b6cbd03fa438");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8049a3c9-f944-425a-b991-ffc5c2594218"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "132affef-10b4-4faa-ab53-503e40c1f620", "AQAAAAEAACcQAAAAEF+9kkVpxyDDc1PMaRrCRqgIQ/jwX6X6lAPzUEsrw7Zd/TD9JXAsQB4OJnccr8fFqg==" });
        }
    }
}
