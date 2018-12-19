using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PredictionModelTrainer.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomRates",
                columns: table => new
                {
                    DailyRoomRateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    RateCodeId = table.Column<int>(nullable: false),
                    RateCode = table.Column<string>(nullable: true),
                    RoomTypeId = table.Column<int>(nullable: false),
                    RoomTypeCode = table.Column<string>(nullable: true),
                    AmountTypeInclusive = table.Column<int>(nullable: false),
                    AmountTypeExclusive = table.Column<int>(nullable: false),
                    TaxAmount = table.Column<int>(nullable: false),
                    RoomRateIsSet = table.Column<bool>(nullable: false),
                    Iscomplimentary = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomRates", x => x.DailyRoomRateId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomRates");
        }
    }
}
