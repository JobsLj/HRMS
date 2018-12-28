using Microsoft.EntityFrameworkCore.Migrations;

namespace HRMS.Migrations
{
    public partial class RoomOccEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedReservation",
                table: "RoomTypeOccupancy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedReservation",
                table: "RoomTypeOccupancy",
                nullable: false,
                defaultValue: 0);
        }
    }
}
