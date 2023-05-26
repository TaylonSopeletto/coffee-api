using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeApiV2.Migrations
{
    /// <inheritdoc />
    public partial class addCoffeeShopCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "CoffeeShops",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "CoffeeShops");
        }
    }
}
