using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kursa4.Migrations
{
    /// <inheritdoc />
    public partial class DeleteHasWebCamAndKeybordBacklight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasWebcam",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "KeyboardBackLight",
                table: "Laptops");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasWebcam",
                table: "Laptops",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "KeyboardBackLight",
                table: "Laptops",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
