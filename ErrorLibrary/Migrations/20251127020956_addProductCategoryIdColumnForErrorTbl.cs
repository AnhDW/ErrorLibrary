using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErrorLibrary.Migrations
{
    /// <inheritdoc />
    public partial class addProductCategoryIdColumnForErrorTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryId",
                table: "Errors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Errors_ProductCategoryId",
                table: "Errors",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Errors_ProductCategories_ProductCategoryId",
                table: "Errors",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Errors_ProductCategories_ProductCategoryId",
                table: "Errors");

            migrationBuilder.DropIndex(
                name: "IX_Errors_ProductCategoryId",
                table: "Errors");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                table: "Errors");
        }
    }
}
