using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10150702_PROG6212_POE.Migrations
{
    /// <inheritdoc />
    public partial class NewRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHR",
                table: "Lecturers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLecturer",
                table: "Lecturers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsManager",
                table: "Lecturers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHR",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "IsLecturer",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "IsManager",
                table: "Lecturers");
        }
    }
}
