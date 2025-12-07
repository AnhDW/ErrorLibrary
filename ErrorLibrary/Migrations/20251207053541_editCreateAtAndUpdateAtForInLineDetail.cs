using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErrorLibrary.Migrations
{
    /// <inheritdoc />
    public partial class editCreateAtAndUpdateAtForInLineDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateTime",
                table: "InLineDetails",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "CreateTime",
                table: "InLineDetails",
                newName: "CreateAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "InLineDetails",
                newName: "UpdateTime");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "InLineDetails",
                newName: "CreateTime");
        }
    }
}
