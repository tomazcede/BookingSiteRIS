using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingSite.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PRIMARY",
                schema: "bookingdatabase",
                table: "uporabniki");

            migrationBuilder.RenameTable(
                name: "uporabniki",
                schema: "bookingdatabase",
                newName: "uporabniki");

            migrationBuilder.RenameIndex(
                name: "tip_uporabnika_id",
                table: "uporabniki",
                newName: "IX_uporabniki_tip_uporabnika_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_uporabniki",
                table: "uporabniki",
                column: "uporabnik_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_uporabniki",
                table: "uporabniki");

            migrationBuilder.RenameTable(
                name: "uporabniki",
                newName: "uporabniki",
                newSchema: "bookingdatabase");

            migrationBuilder.RenameIndex(
                name: "IX_uporabniki_tip_uporabnika_id",
                schema: "bookingdatabase",
                table: "uporabniki",
                newName: "tip_uporabnika_id");

            migrationBuilder.AddPrimaryKey(
                name: "PRIMARY",
                schema: "bookingdatabase",
                table: "uporabniki",
                column: "uporabnik_id");
        }
    }
}
