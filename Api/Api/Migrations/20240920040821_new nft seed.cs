using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class newnftseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price",
                table: "Nfts",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Nfts",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Auctions",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Auctions",
                newName: "Description");

            migrationBuilder.InsertData(
                table: "Nfts",
                columns: new[] { "Id", "Description", "Price", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "Product 1 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", 1000L, "Product 1", "52d7665b-c5d8-4324-8975-0641870a4b53" },
                    { 2, "Product 2 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", 2000L, "Product 2", "52d7665b-c5d8-4324-8975-0641870a4b53" },
                    { 3, "Product 3 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", 3000L, "Product 3", "52d7665b-c5d8-4324-8975-0641870a4b53" },
                    { 4, "Product 4 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", 4000L, "Product 4", "52d7665b-c5d8-4324-8975-0641870a4b53" },
                    { 5, "Product 5 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", 5000L, "Product 5", "52d7665b-c5d8-4324-8975-0641870a4b53" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Nfts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Nfts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Nfts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Nfts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Nfts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Nfts",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Nfts",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Auctions",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Auctions",
                newName: "description");
        }
    }
}
