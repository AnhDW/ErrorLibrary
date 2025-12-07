using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErrorLibrary.Migrations
{
    /// <inheritdoc />
    public partial class editInLineDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "InLines",
                newName: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "InLines",
                newName: "CreateDate");
        }
    }
}
