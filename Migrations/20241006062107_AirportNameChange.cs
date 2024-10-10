using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fbs.Migrations
{
    public partial class AirportNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CityName",
                table: "Airports",
                newName: "city_name");

            migrationBuilder.RenameColumn(
                name: "AirportName",
                table: "Airports",
                newName: "airport_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "city_name",
                table: "Airports",
                newName: "CityName");

            migrationBuilder.RenameColumn(
                name: "airport_name",
                table: "Airports",
                newName: "AirportName");
        }
    }
}
