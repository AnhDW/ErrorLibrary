using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErrorLibrary.Migrations
{
    /// <inheritdoc />
    public partial class editNamePreFinalDateColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PreFinalPreFinalDate3",
                table: "ReportFinalFactoryDetails",
                newName: "PreFinalDate3");

            migrationBuilder.RenameColumn(
                name: "PreFinalPreFinalDate2",
                table: "ReportFinalFactoryDetails",
                newName: "PreFinalDate2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PreFinalDate3",
                table: "ReportFinalFactoryDetails",
                newName: "PreFinalPreFinalDate3");

            migrationBuilder.RenameColumn(
                name: "PreFinalDate2",
                table: "ReportFinalFactoryDetails",
                newName: "PreFinalPreFinalDate2");
        }
    }
}
