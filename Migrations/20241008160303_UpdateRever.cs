using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fbs.Migrations
{
    public partial class UpdateRever : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartureCity",
                table: "Reservations",
                newName: "DepartureCityCode");

            migrationBuilder.RenameColumn(
                name: "ArrivalCity",
                table: "Reservations",
                newName: "ArrivalCityCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartureCityCode",
                table: "Reservations",
                newName: "DepartureCity");

            migrationBuilder.RenameColumn(
                name: "ArrivalCityCode",
                table: "Reservations",
                newName: "ArrivalCity");
        }
    }
}
