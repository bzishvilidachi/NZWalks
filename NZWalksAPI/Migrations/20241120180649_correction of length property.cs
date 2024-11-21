using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalksAPI.Migrations
{
    /// <inheritdoc />
    public partial class correctionoflengthproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LenghtInKm",
                table: "Walks",
                newName: "LengthInKm");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LengthInKm",
                table: "Walks",
                newName: "LenghtInKm");
        }
    }
}
