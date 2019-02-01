using Microsoft.EntityFrameworkCore.Migrations;

namespace PredictionModelTrainer.Migrations
{
    public partial class RoomRatesMAx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Iscomplimentary",
                table: "RoomRates");

            migrationBuilder.DropColumn(
                name: "RateCode",
                table: "RoomRates");

            migrationBuilder.DropColumn(
                name: "RateCodeId",
                table: "RoomRates");

            migrationBuilder.DropColumn(
                name: "RoomRateIsSet",
                table: "RoomRates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Iscomplimentary",
                table: "RoomRates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RateCode",
                table: "RoomRates",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RateCodeId",
                table: "RoomRates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "RoomRateIsSet",
                table: "RoomRates",
                nullable: false,
                defaultValue: false);
        }
    }
}
