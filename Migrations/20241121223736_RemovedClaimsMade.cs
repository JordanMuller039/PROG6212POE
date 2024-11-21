using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10150702_PROG6212_POE.Migrations
{
    /// <inheritdoc />
    public partial class RemovedClaimsMade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Claims",
                table: "Lecturers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Claims",
                table: "Lecturers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
