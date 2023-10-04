using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rusada.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fkaircraftimgtypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AircraftImages_Aircrafts_AircraftId1",
                table: "AircraftImages");

            migrationBuilder.DropIndex(
                name: "IX_AircraftImages_AircraftId1",
                table: "AircraftImages");

            migrationBuilder.DropColumn(
                name: "AircraftId1",
                table: "AircraftImages");

            migrationBuilder.AlterColumn<int>(
                name: "AircraftId",
                table: "AircraftImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AircraftImages_Aircrafts_AircraftId",
                table: "AircraftImages");

            migrationBuilder.DropIndex(
                name: "IX_AircraftImages_AircraftId",
                table: "AircraftImages");

            migrationBuilder.AlterColumn<string>(
                name: "AircraftId",
                table: "AircraftImages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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
    }
}
