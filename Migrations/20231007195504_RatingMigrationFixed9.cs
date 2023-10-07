using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeApiV2.Migrations
{
    /// <inheritdoc />
    public partial class RatingMigrationFixed9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coffees_Orders_OrderId",
                table: "Coffees");

            migrationBuilder.DropIndex(
                name: "IX_Coffees_OrderId",
                table: "Coffees");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Coffees");

            migrationBuilder.CreateTable(
                name: "CoffeeOrder",
                columns: table => new
                {
                    CoffeesId = table.Column<int>(type: "integer", nullable: false),
                    OrdersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoffeeOrder", x => new { x.CoffeesId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_CoffeeOrder_Coffees_CoffeesId",
                        column: x => x.CoffeesId,
                        principalTable: "Coffees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoffeeOrder_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoffeeOrder_OrdersId",
                table: "CoffeeOrder",
                column: "OrdersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoffeeOrder");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Coffees",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coffees_OrderId",
                table: "Coffees",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coffees_Orders_OrderId",
                table: "Coffees",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
