using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeApiV2.Migrations
{
    /// <inheritdoc />
    public partial class addCoffeeDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Coffees",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Coffees");
        }
    }
}
