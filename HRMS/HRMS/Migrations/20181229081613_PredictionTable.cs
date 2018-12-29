using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS.Migrations
{
    public partial class PredictionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomPrice",
                table: "Predictions");

            migrationBuilder.AddColumn<float>(
                name: "DlxOccupancy",
                table: "Predictions",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "DlxRoomRate",
                table: "Predictions",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "FamOccupancy",
                table: "Predictions",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "FamRoomRate",
                table: "Predictions",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "SprOccupancy",
                table: "Predictions",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "SprRoomRate",
                table: "Predictions",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "StdOccupancy",
                table: "Predictions",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "StdRoomRate",
                table: "Predictions",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "SuiteOccupancy",
                table: "Predictions",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "SuiteRoomRate",
                table: "Predictions",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DlxOccupancy",
                table: "Predictions");

            migrationBuilder.DropColumn(
                name: "DlxRoomRate",
                table: "Predictions");

            migrationBuilder.DropColumn(
                name: "FamOccupancy",
                table: "Predictions");

            migrationBuilder.DropColumn(
                name: "FamRoomRate",
                table: "Predictions");

            migrationBuilder.DropColumn(
                name: "SprOccupancy",
                table: "Predictions");

            migrationBuilder.DropColumn(
                name: "SprRoomRate",
                table: "Predictions");

            migrationBuilder.DropColumn(
                name: "StdOccupancy",
                table: "Predictions");

            migrationBuilder.DropColumn(
                name: "StdRoomRate",
                table: "Predictions");

            migrationBuilder.DropColumn(
                name: "SuiteOccupancy",
                table: "Predictions");

            migrationBuilder.DropColumn(
                name: "SuiteRoomRate",
                table: "Predictions");

            migrationBuilder.AddColumn<int>(
                name: "RoomPrice",
                table: "Predictions",
                nullable: false,
                defaultValue: 0);
        }
    }
}
