using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeApiV2.Migrations
{
    /// <inheritdoc />
    public partial class Cart12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coffees_Carts_CartId",
                table: "Coffees");

            migrationBuilder.DropIndex(
                name: "IX_Coffees_CartId",
                table: "Coffees");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Coffees");

            migrationBuilder.AddColumn<int>(
                name: "Tip",
                table: "Carts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "deliverPrice",
                table: "Carts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tip",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "deliverPrice",
                table: "Carts");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Coffees",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coffees_CartId",
                table: "Coffees",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coffees_Carts_CartId",
                table: "Coffees",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");
        }
    }
}
