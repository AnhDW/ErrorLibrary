using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErrorLibrary.Migrations
{
    /// <inheritdoc />
    public partial class DeleteInspectionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InspectionRounds");

            migrationBuilder.DropTable(
                name: "Inspections");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinalDate1",
                table: "ReportFinalFactoryDetails",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinalDate2",
                table: "ReportFinalFactoryDetails",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinalDate3",
                table: "ReportFinalFactoryDetails",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinalMajor",
                table: "ReportFinalFactoryDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FinalMinor",
                table: "ReportFinalFactoryDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FinalResult1",
                table: "ReportFinalFactoryDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinalResult2",
                table: "ReportFinalFactoryDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinalResult3",
                table: "ReportFinalFactoryDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PreFinalDate1",
                table: "ReportFinalFactoryDetails",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreFinalMajor",
                table: "ReportFinalFactoryDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PreFinalMinor",
                table: "ReportFinalFactoryDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PreFinalPreFinalDate2",
                table: "ReportFinalFactoryDetails",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PreFinalPreFinalDate3",
                table: "ReportFinalFactoryDetails",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreFinalResult1",
                table: "ReportFinalFactoryDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreFinalResult2",
                table: "ReportFinalFactoryDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreFinalResult3",
                table: "ReportFinalFactoryDetails",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalDate1",
                table: "ReportFinalFactoryDetails");

            migrationBuilder.DropColumn(
                name: "FinalDate2",
                table: "ReportFinalFactoryDetails");

            migrationBuilder.DropColumn(
                name: "FinalDate3",
                table: "ReportFinalFactoryDetails");

            migrationBuilder.DropColumn(
                name: "FinalMajor",
                table: "ReportFinalFactoryDetails");

            migrationBuilder.DropColumn(
                name: "FinalMinor",
                table: "ReportFinalFactoryDetails");

            migrationBuilder.DropColumn(
                name: "FinalResult1",
                table: "ReportFinalFactoryDetails");

            migrationBuilder.DropColumn(
                name: "FinalResult2",
                table: "ReportFinalFactoryDetails");

            migrationBuilder.DropColumn(
                name: "FinalResult3",
                table: "ReportFinalFactoryDetails");

            migrationBuilder.DropColumn(
                name: "PreFinalDate1",
                table: "ReportFinalFactoryDetails");

            migrationBuilder.DropColumn(
                name: "PreFinalMajor",
                table: "ReportFinalFactoryDetails");

            migrationBuilder.DropColumn(
                name: "PreFinalMinor",
                table: "ReportFinalFactoryDetails");

            migrationBuilder.DropColumn(
                name: "PreFinalPreFinalDate2",
                table: "ReportFinalFactoryDetails");

            migrationBuilder.DropColumn(
                name: "PreFinalPreFinalDate3",
                table: "ReportFinalFactoryDetails");

            migrationBuilder.DropColumn(
                name: "PreFinalResult1",
                table: "ReportFinalFactoryDetails");

            migrationBuilder.DropColumn(
                name: "PreFinalResult2",
                table: "ReportFinalFactoryDetails");

            migrationBuilder.DropColumn(
                name: "PreFinalResult3",
                table: "ReportFinalFactoryDetails");

            migrationBuilder.CreateTable(
                name: "Inspections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReportFinalFactoryDetailId = table.Column<int>(type: "int", nullable: false),
                    InspectionType = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Major = table.Column<int>(type: "int", nullable: false),
                    Minor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inspections_ReportFinalFactoryDetails_ReportFinalFactoryDeta~",
                        column: x => x.ReportFinalFactoryDetailId,
                        principalTable: "ReportFinalFactoryDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InspectionRounds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InspectionId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Result = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionRounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectionRounds_Inspections_InspectionId",
                        column: x => x.InspectionId,
                        principalTable: "Inspections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionRounds_InspectionId",
                table: "InspectionRounds",
                column: "InspectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Inspections_ReportFinalFactoryDetailId",
                table: "Inspections",
                column: "ReportFinalFactoryDetailId");
        }
    }
}
