using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarpoolManagement.Migrations
{
    /// <inheritdoc />
    public partial class CarpoolDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Plate = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IsDriver = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RideShare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartLocation = table.Column<string>(type: "TEXT", nullable: false),
                    EndLocation = table.Column<string>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    EndDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    DriverId = table.Column<int>(type: "INTEGER", nullable: false),
                    CarId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RideShare", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CAR_ID",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RideShareEmployee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RideShareId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RideShareEmployee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RIDE_SHARE_EMPLOYEE",
                        column: x => x.RideShareId,
                        principalTable: "RideShare",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RIDE_SHARE_EMPLOYEE_01",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Car",
                columns: new[] { "Id", "Color", "Name", "NumberOfSeats", "Plate", "Type" },
                values: new object[,]
                {
                    { 1, "Blue", "Blue Beetle - Commute Transport", 4, "AB 123-CD", "VW Beetle" },
                    { 2, "Gray", "Mustang - Quick support", 4, "CD 456-EF", "Ford Mustang" },
                    { 3, "Black", "Octavia - Travel", 5, "EF 789-GH", "Skoda Octavia" },
                    { 4, "Red", "Carnival - Team Travel", 7, "GH 123-IJ", "Kia Carnival" },
                    { 5, "Green", "Tacoma - Off Road Travel", 4, "IJ 456-KL", "Toyota Tacoma" },
                    { 6, "White", "Fabia #1 - Basic Travel", 5, "KL 789-MN", "Skoda Fabia" },
                    { 7, "White", "Fabia #2 - Basic Travel", 5, "MN 123-OP", "Skoda Fabia" },
                    { 8, "White", "Fabia #3 - Basic Travel", 5, "OP 465-QR", "Skoda Fabia" },
                    { 9, "Yellow", "Camaro - Quick support", 4, "QR 789-ST", "Chevrolet Camaro" },
                    { 10, "Other", "Bus - Interurban transport", 63, "ST 123-UV", "Iveco Crossway" }
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "IsDriver", "Name" },
                values: new object[,]
                {
                    { 1, true, "Sebastiana Chaudhari" },
                    { 2, true, "Garbán De Santiago" },
                    { 3, true, "Verginia McCallum" },
                    { 4, true, "Joleen Storstrand" },
                    { 5, true, "Durga Robbins" },
                    { 6, false, "Aeliana Grant" },
                    { 7, false, "Hamo Kumar" },
                    { 8, false, "Oskar Arnaud" },
                    { 9, false, "Rolando Waller" },
                    { 10, false, "Adam Dreessen" },
                    { 11, true, "Elyse Ray" },
                    { 12, true, "Arlo Tyler" },
                    { 13, true, "Helena Houston" },
                    { 14, true, "Sylas Vo" },
                    { 15, true, "Artemis Maxwell" },
                    { 16, false, "Eden Leblanc" },
                    { 17, false, "Novalee Dennis" },
                    { 18, false, "Joanna Mendez" },
                    { 19, false, "Arthur Poole" },
                    { 20, false, "Bonnie Flynn" },
                    { 21, true, "Kannon Boyer" },
                    { 22, true, "Chaya Ashley" },
                    { 23, true, "Kylen Woods" },
                    { 24, true, "Reese Dougherty" },
                    { 25, true, "Brett Sierra" },
                    { 26, false, "Kohen Hill" },
                    { 27, false, "Lillie Becker" },
                    { 28, false, "Andy Mata" },
                    { 29, false, "Zev Alvarez" },
                    { 30, false, "Raylan Lane" }
                });

            migrationBuilder.CreateIndex(
                name: "PK_CAR",
                table: "Car",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "PK_EMPLOYEE",
                table: "Employee",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "FK_CAR_ID",
                table: "RideShare",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "PK_RIDE_SHARE",
                table: "RideShare",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FK_RIDE_SHARE_EMPLOYEE",
                table: "RideShareEmployee",
                column: "RideShareId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_RIDE_SHARE_EMPLOYEE_01",
                table: "RideShareEmployee",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "PK_RIDE_SHARE_EMPLOYEE",
                table: "RideShareEmployee",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RideShareEmployee");

            migrationBuilder.DropTable(
                name: "RideShare");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Car");
        }
    }
}
