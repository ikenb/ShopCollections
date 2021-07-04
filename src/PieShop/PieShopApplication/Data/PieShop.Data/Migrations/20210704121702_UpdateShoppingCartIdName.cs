using Microsoft.EntityFrameworkCore.Migrations;

namespace PieShop.Data.Migrations
{
    public partial class UpdateShoppingCartIdName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShopingCardId",
                table: "ShoppingCartItems",
                newName: "ShoppingCartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShoppingCartId",
                table: "ShoppingCartItems",
                newName: "ShopingCardId");
        }
    }
}
