using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Repository.Migrations
{
    public partial class secondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInShoppingCarts",
                table: "ProductInShoppingCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInOrder",
                table: "ProductInOrder");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Bileti",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Format",
                table: "Bileti",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInShoppingCarts",
                table: "ProductInShoppingCarts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInOrder",
                table: "ProductInOrder",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInShoppingCarts_BiletId",
                table: "ProductInShoppingCarts",
                column: "BiletId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInOrder_TicketId",
                table: "ProductInOrder",
                column: "TicketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInShoppingCarts",
                table: "ProductInShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ProductInShoppingCarts_BiletId",
                table: "ProductInShoppingCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInOrder",
                table: "ProductInOrder");

            migrationBuilder.DropIndex(
                name: "IX_ProductInOrder_TicketId",
                table: "ProductInOrder");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Bileti",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Format",
                table: "Bileti",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInShoppingCarts",
                table: "ProductInShoppingCarts",
                columns: new[] { "BiletId", "ShoppingCartId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInOrder",
                table: "ProductInOrder",
                columns: new[] { "TicketId", "OrderId" });
        }
    }
}
