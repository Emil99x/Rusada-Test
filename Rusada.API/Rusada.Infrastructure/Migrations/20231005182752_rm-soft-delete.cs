using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rusada.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class rmsoftdelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "AircraftImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "AircraftImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
