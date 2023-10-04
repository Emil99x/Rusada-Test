using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rusada.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class rmfkaircraft : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AircraftImages_Aircrafts_AircraftId",
                table: "AircraftImages");

            migrationBuilder.DropIndex(
                name: "IX_AircraftImages_AircraftId",
                table: "AircraftImages");

            migrationBuilder.DropColumn(
                name: "AircraftId",
                table: "AircraftImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AircraftId",
                table: "AircraftImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AircraftImages_AircraftId",
                table: "AircraftImages",
                column: "AircraftId");

            migrationBuilder.AddForeignKey(
                name: "FK_AircraftImages_Aircrafts_AircraftId",
                table: "AircraftImages",
                column: "AircraftId",
                principalTable: "Aircrafts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
