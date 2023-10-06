using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeApiV2.Migrations
{
    /// <inheritdoc />
    public partial class RatingMigrationFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Coffees_CoffeeId",
                table: "Ratings");

            migrationBuilder.RenameColumn(
                name: "CoffeeId",
                table: "Ratings",
                newName: "CoffeeShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_CoffeeId",
                table: "Ratings",
                newName: "IX_Ratings_CoffeeShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_CoffeeShops_CoffeeShopId",
                table: "Ratings",
                column: "CoffeeShopId",
                principalTable: "CoffeeShops",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_CoffeeShops_CoffeeShopId",
                table: "Ratings");

            migrationBuilder.RenameColumn(
                name: "CoffeeShopId",
                table: "Ratings",
                newName: "CoffeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_CoffeeShopId",
                table: "Ratings",
                newName: "IX_Ratings_CoffeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Coffees_CoffeeId",
                table: "Ratings",
                column: "CoffeeId",
                principalTable: "Coffees",
                principalColumn: "Id");
        }
    }
}
