using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Date : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "customers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RentDate",
                table: "reservations",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpectingRetrieveDate",
                table: "reservations",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActualRetrieveDate",
                table: "reservations",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "RentDate",
                table: "reservations",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ExpectingRetrieveDate",
                table: "reservations",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ActualRetrieveDate",
                table: "reservations",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDate",
                table: "customers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}
