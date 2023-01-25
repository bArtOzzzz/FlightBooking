using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlightBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FlightBookingMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airlines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AirlineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airlines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Airplanes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaximumSeats = table.Column<int>(type: "int", nullable: false),
                    MaximumWeight = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airplanes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AirlineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AirplaneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Departurer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Arrival = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_Airlines_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airlines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flights_Airplanes_AirplaneId",
                        column: x => x.AirplaneId,
                        principalTable: "Airplanes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonInformations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Citizenship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirePasportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonInformations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardingPasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Prise = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    isExpired = table.Column<bool>(type: "bit", nullable: false),
                    BookingExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardingPasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardingPasses_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardingPasses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Airlines",
                columns: new[] { "Id", "AirlineName", "CreatedDate", "CreatedTime", "Rating" },
                values: new object[,]
                {
                    { new Guid("3c8c69b4-c5f7-420c-ad9a-f35cab7bd0cf"), "Belavia", "01-25-23", "12:4:17", 4.2999999999999998 },
                    { new Guid("96e1738a-568d-4a23-be7d-f2f3382f1cf9"), "American Airlines", "01-25-23", "12:4:17", 4.7000000000000002 },
                    { new Guid("b01e92b9-2c8b-4437-b290-eae73e1017f0"), "Airnorth", "01-25-23", "12:4:17", 3.8999999999999999 }
                });

            migrationBuilder.InsertData(
                table: "Airplanes",
                columns: new[] { "Id", "CreatedDate", "CreatedTime", "MaximumSeats", "MaximumWeight", "ModelName" },
                values: new object[,]
                {
                    { new Guid("032df96e-9d2b-4321-a8f0-c5ab1d062b76"), "01-25-23", "12:4:17", 150, 22600, "Comac C919" },
                    { new Guid("21e5d331-780f-439c-9060-96bf96d31e27"), "01-25-23", "12:4:17", 300, 31000, "Airbus A350" },
                    { new Guid("44dcfa17-b97d-4bd9-828b-ab498f565c9d"), "01-25-23", "12:4:17", 525, 46750, "ATR EVO" },
                    { new Guid("50331fce-d5f5-494e-97ba-b89909601281"), "01-25-23", "12:4:17", 180, 22600, "Comac C919" },
                    { new Guid("6f39bb24-d2ef-4383-872d-abb02751ca7f"), "01-25-23", "12:4:17", 525, 46750, "ATR EVO" },
                    { new Guid("7793ab1b-7600-4734-8139-64d7ed41d367"), "01-25-23", "12:4:17", 125, 35600, "Airbus A220" },
                    { new Guid("b642faa8-115d-4eeb-88e6-74e7c5eeca7a"), "01-25-23", "12:4:17", 320, 32400, "CRIAC CR929" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "CreatedTime" },
                values: new object[,]
                {
                    { new Guid("5071c449-d4ed-4b35-9aff-8bbbda78be2b"), "01-25-23", "12:4:17" },
                    { new Guid("6d0b4263-c3ab-4c84-8f42-7abda64789b9"), "01-25-23", "12:4:17" },
                    { new Guid("ed7a6b39-79e8-43a4-b7cf-96d0d16df0d3"), "01-25-23", "12:4:17" }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "AirlineId", "AirplaneId", "Arrival", "ArrivingDate", "CreatedDate", "CreatedTime", "DepartureDate", "Departurer" },
                values: new object[,]
                {
                    { new Guid("46797e84-5438-41e4-b36f-e68744fbf290"), new Guid("3c8c69b4-c5f7-420c-ad9a-f35cab7bd0cf"), new Guid("b642faa8-115d-4eeb-88e6-74e7c5eeca7a"), "Paris", new DateTime(2023, 5, 13, 18, 55, 0, 0, DateTimeKind.Unspecified), "01-25-23", "12:4:17", new DateTime(2023, 5, 13, 10, 30, 0, 0, DateTimeKind.Unspecified), "Pekin" },
                    { new Guid("48c30f5d-cfef-428b-bfdf-a7650e7cec6f"), new Guid("3c8c69b4-c5f7-420c-ad9a-f35cab7bd0cf"), new Guid("44dcfa17-b97d-4bd9-828b-ab498f565c9d"), "Boston", new DateTime(2023, 2, 19, 18, 20, 0, 0, DateTimeKind.Unspecified), "01-25-23", "12:4:17", new DateTime(2023, 2, 17, 22, 15, 0, 0, DateTimeKind.Unspecified), "Minsk" },
                    { new Guid("96209a9d-78f8-4d7f-9adb-3f8b4c747066"), new Guid("96e1738a-568d-4a23-be7d-f2f3382f1cf9"), new Guid("50331fce-d5f5-494e-97ba-b89909601281"), "Mykonos", new DateTime(2023, 3, 23, 11, 15, 0, 0, DateTimeKind.Unspecified), "01-25-23", "12:4:17", new DateTime(2023, 3, 20, 20, 55, 0, 0, DateTimeKind.Unspecified), "Singapore" },
                    { new Guid("d687cb42-a4b4-4df9-ba34-634bd8c87983"), new Guid("b01e92b9-2c8b-4437-b290-eae73e1017f0"), new Guid("7793ab1b-7600-4734-8139-64d7ed41d367"), "yaunde", new DateTime(2023, 7, 6, 3, 50, 0, 0, DateTimeKind.Unspecified), "01-25-23", "12:4:17", new DateTime(2023, 7, 3, 6, 50, 0, 0, DateTimeKind.Unspecified), "frankfurt" }
                });

            migrationBuilder.InsertData(
                table: "PersonInformations",
                columns: new[] { "Id", "BirthDate", "Citizenship", "CreatedDate", "CreatedTime", "ExpirePasportDate", "Gender", "IdentificationNumber", "Name", "Surname", "UserId" },
                values: new object[,]
                {
                    { new Guid("05b231a2-c31d-4dfc-955a-6097102b1456"), new DateTime(1993, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "America", "01-25-23", "12:4:17", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "М", "2763984J836PB3", "Владислав", "Лазарев", new Guid("6d0b4263-c3ab-4c84-8f42-7abda64789b9") },
                    { new Guid("257fa5d1-d5b0-4faf-af1a-0185b3cfefab"), new DateTime(1983, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Belarus", "01-25-23", "12:4:17", new DateTime(2027, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "М", "2763984J836PB3", "Александр", "Иванов", new Guid("5071c449-d4ed-4b35-9aff-8bbbda78be2b") },
                    { new Guid("e72b6d0c-a0ba-471a-bf43-cabca8ebbaf6"), new DateTime(1998, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Poland", "01-25-23", "12:4:17", new DateTime(2025, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ж", "2763984J836PB3", "Мария", "Лебедева", new Guid("ed7a6b39-79e8-43a4-b7cf-96d0d16df0d3") }
                });

            migrationBuilder.InsertData(
                table: "BoardingPasses",
                columns: new[] { "Id", "BookingExpireDate", "CreatedDate", "CreatedTime", "FlightId", "Prise", "UserId", "isExpired" },
                values: new object[,]
                {
                    { new Guid("5ace6361-4908-4dff-8d6e-7201ae071ae3"), new DateTime(2023, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "03-16-23", "15:22:48", new Guid("d687cb42-a4b4-4df9-ba34-634bd8c87983"), 1401m, new Guid("ed7a6b39-79e8-43a4-b7cf-96d0d16df0d3"), false },
                    { new Guid("5b646986-d9f8-4cee-b080-c22cfa9115e9"), new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "05-12-23", "12:17:3", new Guid("48c30f5d-cfef-428b-bfdf-a7650e7cec6f"), 778m, new Guid("5071c449-d4ed-4b35-9aff-8bbbda78be2b"), false },
                    { new Guid("bada3bb6-0621-4f09-8ada-07bfcc0b4de3"), new DateTime(2023, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "02-06-23", "08:43:17", new Guid("46797e84-5438-41e4-b36f-e68744fbf290"), 1661m, new Guid("5071c449-d4ed-4b35-9aff-8bbbda78be2b"), false },
                    { new Guid("c2ccfc2c-76c5-454c-9ad7-a51127023ca2"), new DateTime(2023, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "07-24-23", "19:36:16", new Guid("96209a9d-78f8-4d7f-9adb-3f8b4c747066"), 1672m, new Guid("6d0b4263-c3ab-4c84-8f42-7abda64789b9"), false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardingPasses_FlightId",
                table: "BoardingPasses",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardingPasses_UserId",
                table: "BoardingPasses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirlineId",
                table: "Flights",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirplaneId",
                table: "Flights",
                column: "AirplaneId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonInformations_UserId",
                table: "PersonInformations",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardingPasses");

            migrationBuilder.DropTable(
                name: "PersonInformations");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Airlines");

            migrationBuilder.DropTable(
                name: "Airplanes");
        }
    }
}
