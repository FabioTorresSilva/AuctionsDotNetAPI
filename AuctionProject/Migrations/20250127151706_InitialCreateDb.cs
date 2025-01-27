using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemCategories_Categories_CategoriesId",
                table: "ItemCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemCategories_Items_ItemsId",
                table: "ItemCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemCategories",
                table: "ItemCategories");

            migrationBuilder.RenameTable(
                name: "ItemCategories",
                newName: "CategoryItem");

            migrationBuilder.RenameIndex(
                name: "IX_ItemCategories_ItemsId",
                table: "CategoryItem",
                newName: "IX_CategoryItem_ItemsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryItem",
                table: "CategoryItem",
                columns: new[] { "CategoriesId", "ItemsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryItem_Categories_CategoriesId",
                table: "CategoryItem",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryItem_Items_ItemsId",
                table: "CategoryItem",
                column: "ItemsId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryItem_Categories_CategoriesId",
                table: "CategoryItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryItem_Items_ItemsId",
                table: "CategoryItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryItem",
                table: "CategoryItem");

            migrationBuilder.RenameTable(
                name: "CategoryItem",
                newName: "ItemCategories");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryItem_ItemsId",
                table: "ItemCategories",
                newName: "IX_ItemCategories_ItemsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemCategories",
                table: "ItemCategories",
                columns: new[] { "CategoriesId", "ItemsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCategories_Categories_CategoriesId",
                table: "ItemCategories",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCategories_Items_ItemsId",
                table: "ItemCategories",
                column: "ItemsId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
