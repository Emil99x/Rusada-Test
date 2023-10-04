using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rusada.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fkaircraftimg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AircraftId",
                table: "AircraftImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AircraftId1",
                table: "AircraftImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AircraftImages_AircraftId1",
                table: "AircraftImages",
                column: "AircraftId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AircraftImages_Aircrafts_AircraftId1",
                table: "AircraftImages",
                column: "AircraftId1",
                principalTable: "Aircrafts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AircraftImages_Aircrafts_AircraftId1",
                table: "AircraftImages");

            migrationBuilder.DropIndex(
                name: "IX_AircraftImages_AircraftId1",
                table: "AircraftImages");

            migrationBuilder.DropColumn(
                name: "AircraftId",
                table: "AircraftImages");

            migrationBuilder.DropColumn(
                name: "AircraftId1",
                table: "AircraftImages");
        }
    }
}
