using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rusada.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fkaircraftimgtoaircraft : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AircraftImages_AircraftId",
                table: "AircraftImages");

            migrationBuilder.CreateIndex(
                name: "IX_AircraftImages_AircraftId",
                table: "AircraftImages",
                column: "AircraftId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AircraftImages_AircraftId",
                table: "AircraftImages");

            migrationBuilder.CreateIndex(
                name: "IX_AircraftImages_AircraftId",
                table: "AircraftImages",
                column: "AircraftId");
        }
    }
}
