using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS.Migrations
{
    public partial class adjustedrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "AdjDlxRoomRate",
                table: "Predictions",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AdjFamRoomRate",
                table: "Predictions",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AdjSprRoomRate",
                table: "Predictions",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AdjStdRoomRate",
                table: "Predictions",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AdjSuiteRoomRate",
                table: "Predictions",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "SelectedRoomRate",
                table: "Predictions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdjDlxRoomRate",
                table: "Predictions");

            migrationBuilder.DropColumn(
                name: "AdjFamRoomRate",
                table: "Predictions");

            migrationBuilder.DropColumn(
                name: "AdjSprRoomRate",
                table: "Predictions");

            migrationBuilder.DropColumn(
                name: "AdjStdRoomRate",
                table: "Predictions");

            migrationBuilder.DropColumn(
                name: "AdjSuiteRoomRate",
                table: "Predictions");

            migrationBuilder.DropColumn(
                name: "SelectedRoomRate",
                table: "Predictions");
        }
    }
}
