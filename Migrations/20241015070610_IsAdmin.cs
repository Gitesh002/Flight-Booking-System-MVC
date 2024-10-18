using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fbs.Migrations
{
    public partial class IsAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_Reservations_ReservationId",
                table: "Passengers");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "UserAccounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "ReservationId",
                table: "Passengers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_Reservations_ReservationId",
                table: "Passengers",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "ReservationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_Reservations_ReservationId",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "UserAccounts");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationId",
                table: "Passengers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_Reservations_ReservationId",
                table: "Passengers",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "ReservationId");
        }
    }
}
