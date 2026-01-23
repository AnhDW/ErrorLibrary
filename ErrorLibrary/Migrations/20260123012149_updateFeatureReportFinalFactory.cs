using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErrorLibrary.Migrations
{
    /// <inheritdoc />
    public partial class updateFeatureReportFinalFactory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InspectionDefects");

            migrationBuilder.CreateTable(
                name: "ReportFinalFactoryDetailDefects",
                columns: table => new
                {
                    ReportFinalFactoryDetailId = table.Column<int>(type: "int", nullable: false),
                    DefectId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportFinalFactoryDetailDefects", x => new { x.ReportFinalFactoryDetailId, x.DefectId });
                    table.ForeignKey(
                        name: "FK_ReportFinalFactoryDetailDefects_Defects_DefectId",
                        column: x => x.DefectId,
                        principalTable: "Defects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportFinalFactoryDetailDefects_ReportFinalFactoryDetails_Re~",
                        column: x => x.ReportFinalFactoryDetailId,
                        principalTable: "ReportFinalFactoryDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFinalFactoryDetailDefects_DefectId",
                table: "ReportFinalFactoryDetailDefects",
                column: "DefectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportFinalFactoryDetailDefects");

            migrationBuilder.CreateTable(
                name: "InspectionDefects",
                columns: table => new
                {
                    ReportFinalFactoryDetailId = table.Column<int>(type: "int", nullable: false),
                    DefectId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionDefects", x => new { x.ReportFinalFactoryDetailId, x.DefectId });
                    table.ForeignKey(
                        name: "FK_InspectionDefects_Defects_DefectId",
                        column: x => x.DefectId,
                        principalTable: "Defects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InspectionDefects_ReportFinalFactoryDetails_ReportFinalFacto~",
                        column: x => x.ReportFinalFactoryDetailId,
                        principalTable: "ReportFinalFactoryDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionDefects_DefectId",
                table: "InspectionDefects",
                column: "DefectId");
        }
    }
}
