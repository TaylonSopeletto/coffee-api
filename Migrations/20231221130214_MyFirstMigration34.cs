using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeApiV2.Migrations
{
    /// <inheritdoc />
    public partial class MyFirstMigration34 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoffeeShopId",
                table: "Coffees",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coffees_CoffeeShopId",
                table: "Coffees",
                column: "CoffeeShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coffees_CoffeeShops_CoffeeShopId",
                table: "Coffees",
                column: "CoffeeShopId",
                principalTable: "CoffeeShops",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coffees_CoffeeShops_CoffeeShopId",
                table: "Coffees");

            migrationBuilder.DropIndex(
                name: "IX_Coffees_CoffeeShopId",
                table: "Coffees");

            migrationBuilder.DropColumn(
                name: "CoffeeShopId",
                table: "Coffees");
        }
    }
}
