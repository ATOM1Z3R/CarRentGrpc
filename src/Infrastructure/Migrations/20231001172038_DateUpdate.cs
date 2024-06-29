using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DateUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    City = table.Column<string>(type: "Varchar", maxLength: 150, nullable: false),
                    Street = table.Column<string>(type: "Varchar", maxLength: 250, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FirstName = table.Column<string>(type: "Varchar", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "Varchar", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<byte[]>(type: "bytea", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FirstName = table.Column<string>(type: "Varchar", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "Varchar", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "localizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    City = table.Column<string>(type: "Varchar", maxLength: 150, nullable: false),
                    Street = table.Column<string>(type: "Varchar", maxLength: 250, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_localizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NumberPlate = table.Column<string>(type: "Varchar", maxLength: 15, nullable: false),
                    Manufacturer = table.Column<string>(type: "Varchar", maxLength: 125, nullable: false),
                    Model = table.Column<string>(type: "Varchar", maxLength: 125, nullable: false),
                    Color = table.Column<string>(type: "Varchar", maxLength: 75, nullable: false),
                    Power = table.Column<long>(type: "bigint", maxLength: 75, nullable: false),
                    NumberOfSeats = table.Column<long>(type: "bigint", nullable: false),
                    ManufactureYear = table.Column<long>(type: "bigint", nullable: false),
                    Availability = table.Column<bool>(type: "boolean", nullable: false),
                    PriceMultiplier = table.Column<long>(type: "bigint", nullable: false),
                    LocalizationId = table.Column<int>(type: "integer", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cars_localizations_LocalizationId",
                        column: x => x.LocalizationId,
                        principalTable: "localizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ExpectingRetrieveDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ActualRetrieveDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CarId = table.Column<int>(type: "integer", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reservations_cars_CarId",
                        column: x => x.CarId,
                        principalTable: "cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservations_customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cars_LocalizationId",
                table: "cars",
                column: "LocalizationId");

            migrationBuilder.CreateIndex(
                name: "IX_cars_NumberPlate",
                table: "cars",
                column: "NumberPlate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_reservations_CarId",
                table: "reservations",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_CustomerId",
                table: "reservations",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "cars");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "localizations");
        }
    }
}
