using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErrorLibrary.Migrations
{
    /// <inheritdoc />
    public partial class editInlineFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InLineDetails_TimeFrameColors_TimeFrameColorId",
                table: "InLineDetails");

            migrationBuilder.RenameColumn(
                name: "TimeFrameColorId",
                table: "InLineDetails",
                newName: "TimeFrameId");

            migrationBuilder.RenameIndex(
                name: "IX_InLineDetails_TimeFrameColorId",
                table: "InLineDetails",
                newName: "IX_InLineDetails_TimeFrameId");

            migrationBuilder.AddForeignKey(
                name: "FK_InLineDetails_TimeFrames_TimeFrameId",
                table: "InLineDetails",
                column: "TimeFrameId",
                principalTable: "TimeFrames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InLineDetails_TimeFrames_TimeFrameId",
                table: "InLineDetails");

            migrationBuilder.RenameColumn(
                name: "TimeFrameId",
                table: "InLineDetails",
                newName: "TimeFrameColorId");

            migrationBuilder.RenameIndex(
                name: "IX_InLineDetails_TimeFrameId",
                table: "InLineDetails",
                newName: "IX_InLineDetails_TimeFrameColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_InLineDetails_TimeFrameColors_TimeFrameColorId",
                table: "InLineDetails",
                column: "TimeFrameColorId",
                principalTable: "TimeFrameColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
