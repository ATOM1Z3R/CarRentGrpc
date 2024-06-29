using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Power",
                table: "cars");

            migrationBuilder.RenameColumn(
                name: "ManufactureYear",
                table: "cars",
                newName: "Year");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Year",
                table: "cars",
                newName: "ManufactureYear");

            migrationBuilder.AddColumn<long>(
                name: "Power",
                table: "cars",
                type: "bigint",
                maxLength: 75,
                nullable: false,
                defaultValue: 0L);
        }
    }
}
