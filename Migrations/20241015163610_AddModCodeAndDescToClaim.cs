using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10150702_PROG6212_POE.Migrations
{
    /// <inheritdoc />
    public partial class AddModCodeAndDescToClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModCode",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "desc",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModCode",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "desc",
                table: "Claims");
        }
    }
}
