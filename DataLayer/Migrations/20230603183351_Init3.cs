using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Routes_IdRoute",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "IdRoute",
                table: "Flights",
                newName: "IdAirline_Route");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_IdRoute",
                table: "Flights",
                newName: "IX_Flights_IdAirline_Route");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airline_Routes_IdAirline_Route",
                table: "Flights",
                column: "IdAirline_Route",
                principalTable: "Airline_Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airline_Routes_IdAirline_Route",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "IdAirline_Route",
                table: "Flights",
                newName: "IdRoute");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_IdAirline_Route",
                table: "Flights",
                newName: "IX_Flights_IdRoute");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Routes_IdRoute",
                table: "Flights",
                column: "IdRoute",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
