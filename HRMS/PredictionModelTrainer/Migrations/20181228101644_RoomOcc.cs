using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PredictionModelTrainer.Migrations
{
    public partial class RoomOcc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Occuapancy",
                table: "Occuapancy");

            migrationBuilder.RenameTable(
                name: "Occuapancy",
                newName: "Occupancy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Occupancy",
                table: "Occupancy",
                column: "DailyOccupancyId");

            migrationBuilder.CreateTable(
                name: "RoomTypeOccupancy",
                columns: table => new
                {
                    DailyOccupancyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    RoomTypeId = table.Column<int>(nullable: false),
                    RoomTypeCode = table.Column<string>(nullable: true),
                    RoomTypeDesc = table.Column<string>(nullable: true),
                    ReservationNo = table.Column<int>(nullable: false),
                    RoomOccupied = table.Column<int>(nullable: false),
                    RoomSold = table.Column<int>(nullable: false),
                    Complimentary = table.Column<int>(nullable: false),
                    DayUse = table.Column<int>(nullable: false),
                    Tentative = table.Column<int>(nullable: false),
                    Unavailable = table.Column<int>(nullable: false),
                    TotalRoom = table.Column<int>(nullable: false),
                    RoomInventory = table.Column<int>(nullable: false),
                    VacantRoom = table.Column<int>(nullable: false),
                    UnassignedReservation = table.Column<int>(nullable: false),
                    AdultNo = table.Column<int>(nullable: false),
                    ChildrenNo = table.Column<int>(nullable: false),
                    TotalCancellation = table.Column<int>(nullable: false),
                    NoShow = table.Column<int>(nullable: false),
                    UserCancellation = table.Column<int>(nullable: false),
                    TentativeCancellation = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypeOccupancy", x => x.DailyOccupancyId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomTypeOccupancy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Occupancy",
                table: "Occupancy");

            migrationBuilder.RenameTable(
                name: "Occupancy",
                newName: "Occuapancy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Occuapancy",
                table: "Occuapancy",
                column: "DailyOccupancyId");
        }
    }
}
