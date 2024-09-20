using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class addedseedforbids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 9, 27, 11, 17, 18, 401, DateTimeKind.Local).AddTicks(617), new DateTime(2024, 9, 20, 11, 17, 18, 401, DateTimeKind.Local).AddTicks(606) });

            migrationBuilder.InsertData(
                table: "Bids",
                columns: new[] { "Id", "AuctionID", "BidDate", "BidPrice", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 20, 11, 27, 18, 401, DateTimeKind.Local).AddTicks(646), 2500L, "ac20c689-a227-41e9-a7e2-c475194510ab" },
                    { 2, 1, new DateTime(2024, 9, 20, 11, 32, 18, 401, DateTimeKind.Local).AddTicks(648), 3000L, "c0eba42d-ff03-449f-bf08-b3d650c5dbeb" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 9, 27, 11, 3, 34, 926, DateTimeKind.Local).AddTicks(1721), new DateTime(2024, 9, 20, 11, 3, 34, 926, DateTimeKind.Local).AddTicks(1705) });
        }
    }
}
