using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS.Migrations
{
    public partial class OccCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Occupancy",
                columns: table => new
                {
                    DailyOccupancyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
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
                    AssignedReservation = table.Column<int>(nullable: false),
                    UnassignedReservation = table.Column<int>(nullable: false),
                    AdultNo = table.Column<int>(nullable: false),
                    ChildrenNo = table.Column<int>(nullable: false),
                    TotalCancellation = table.Column<int>(nullable: false),
                    NoShow = table.Column<int>(nullable: false),
                    UserCancellation = table.Column<int>(nullable: false),
                    TentativeCancellation = table.Column<int>(nullable: false),
                    Vip = table.Column<int>(nullable: false),
                    Departing = table.Column<int>(nullable: false),
                    Inhouse = table.Column<int>(nullable: false),
                    Arriving = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occupancy", x => x.DailyOccupancyId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Occupancy");
        }
    }
}
