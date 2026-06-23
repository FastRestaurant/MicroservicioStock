using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UniqueDrinkStockIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stock_Id_Drink",
                table: "Stock");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_Id_Drink",
                table: "Stock",
                column: "Id_Drink",
                unique: true,
                filter: "[Id_Drink] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stock_Id_Drink",
                table: "Stock");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_Id_Drink",
                table: "Stock",
                column: "Id_Drink");
        }
    }
}
