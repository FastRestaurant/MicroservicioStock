using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStockConsumption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Id_Drink = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Id_Stock = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredient_Stock_Id_Stock",
                        column: x => x.Id_Stock,
                        principalTable: "Stock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockMovements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    MovementType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockMovements_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IngredientDish",
                columns: table => new
                {
                    IdIngredientDish = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_Ingredient = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id_Dish = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequiredQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientDish", x => x.IdIngredientDish);
                    table.ForeignKey(
                        name: "FK_IngredientDish_Ingredient_Id_Ingredient",
                        column: x => x.Id_Ingredient,
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_Id_Stock",
                table: "Ingredient",
                column: "Id_Stock");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_Name",
                table: "Ingredient",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IngredientDish_Id_Dish_Id_Ingredient",
                table: "IngredientDish",
                columns: new[] { "Id_Dish", "Id_Ingredient" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IngredientDish_Id_Ingredient",
                table: "IngredientDish",
                column: "Id_Ingredient");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_Id_Drink",
                table: "Stock",
                column: "Id_Drink");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_OrderId_OrderItemId",
                table: "StockMovements",
                columns: new[] { "OrderId", "OrderItemId" });

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_OrderItemId_StockId_MovementType",
                table: "StockMovements",
                columns: new[] { "OrderItemId", "StockId", "MovementType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_StockId",
                table: "StockMovements",
                column: "StockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientDish");

            migrationBuilder.DropTable(
                name: "StockMovements");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Stock");
        }
    }
}
