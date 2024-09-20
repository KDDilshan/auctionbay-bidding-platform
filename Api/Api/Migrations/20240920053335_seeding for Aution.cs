using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class seedingforAution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "BidPrice",
                table: "Bids",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "Price",
                table: "Auctions",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "EndDate", "NftId", "Price", "StartDate", "Title", "UserID" },
                values: new object[] { 1, "This is a sample auction for an NFT.", new DateTime(2024, 9, 27, 11, 3, 34, 926, DateTimeKind.Local).AddTicks(1721), 1, 500L, new DateTime(2024, 9, 20, 11, 3, 34, 926, DateTimeKind.Local).AddTicks(1705), "Sample Auction", "52d7665b-c5d8-4324-8975-0641870a4b53" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<double>(
                name: "BidPrice",
                table: "Bids",
                type: "float",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Auctions",
                type: "float",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
