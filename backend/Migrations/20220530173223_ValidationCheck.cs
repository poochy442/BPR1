using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class ValidationCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 24, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 24, 9, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 24, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 24, 12, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 6, 24, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 24, 9, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate", "TableId" },
                values: new object[] { new DateTime(2022, 6, 24, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "EndDate", "StartDate", "TableId" },
                values: new object[] { new DateTime(2022, 6, 24, 20, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 24, 17, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "Date", "EndDate", "GuestNo", "Note", "RestaurantId", "StartDate", "TableId", "UserId" },
                values: new object[,]
                {
                    { 6, new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 1, new DateTime(2022, 6, 24, 10, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 },
                    { 7, new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 24, 17, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 1, new DateTime(2022, 6, 24, 15, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 24, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 24, 9, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 24, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 24, 12, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 6, 24, 20, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 24, 17, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate", "TableId" },
                values: new object[] { new DateTime(2022, 6, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 24, 10, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "EndDate", "StartDate", "TableId" },
                values: new object[] { new DateTime(2022, 6, 24, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 24, 15, 0, 0, 0, DateTimeKind.Unspecified), 2 });
        }
    }
}
