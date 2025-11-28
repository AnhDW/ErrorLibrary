using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErrorLibrary.Migrations
{
    /// <inheritdoc />
    public partial class addErrorCategoryTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErrorCategory",
                table: "Errors");

            migrationBuilder.AddColumn<int>(
                name: "ErrorCategoryId",
                table: "Errors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ErrorCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorCategories", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Errors_ErrorCategoryId",
                table: "Errors",
                column: "ErrorCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Errors_ErrorCategories_ErrorCategoryId",
                table: "Errors",
                column: "ErrorCategoryId",
                principalTable: "ErrorCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Errors_ErrorCategories_ErrorCategoryId",
                table: "Errors");

            migrationBuilder.DropTable(
                name: "ErrorCategories");

            migrationBuilder.DropIndex(
                name: "IX_Errors_ErrorCategoryId",
                table: "Errors");

            migrationBuilder.DropColumn(
                name: "ErrorCategoryId",
                table: "Errors");

            migrationBuilder.AddColumn<string>(
                name: "ErrorCategory",
                table: "Errors",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
