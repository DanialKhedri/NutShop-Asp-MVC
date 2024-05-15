using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class whyus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WhyNutsDescription",
                table: "Shop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhyNutsTitle",
                table: "Shop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhyUsDescription",
                table: "Shop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhyUsDescription2",
                table: "Shop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhyUsTitle",
                table: "Shop",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhyUsTitle2",
                table: "Shop",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WhyNutsDescription",
                table: "Shop");

            migrationBuilder.DropColumn(
                name: "WhyNutsTitle",
                table: "Shop");

            migrationBuilder.DropColumn(
                name: "WhyUsDescription",
                table: "Shop");

            migrationBuilder.DropColumn(
                name: "WhyUsDescription2",
                table: "Shop");

            migrationBuilder.DropColumn(
                name: "WhyUsTitle",
                table: "Shop");

            migrationBuilder.DropColumn(
                name: "WhyUsTitle2",
                table: "Shop");
        }
    }
}
