using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlightBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
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
                    { new Guid("078249c5-d887-4e17-966f-bbe1838b36f2"), "American Airlines", "01-13-23", "11:35:31", 4.7000000000000002 },
                    { new Guid("5d114b2f-3dd0-4798-91b2-480bb3a51ced"), "Belavia", "01-13-23", "11:35:31", 4.2999999999999998 },
                    { new Guid("ad13f8da-9fb1-4b78-ae4a-9c5209b9ea8b"), "Airnorth", "01-13-23", "11:35:31", 3.8999999999999999 }
                });

            migrationBuilder.InsertData(
                table: "Airplanes",
                columns: new[] { "Id", "CreatedDate", "CreatedTime", "MaximumSeats", "MaximumWeight", "ModelName" },
                values: new object[,]
                {
                    { new Guid("00bbcc7d-784a-4be4-8f92-3cbe95875e8c"), "01-13-23", "11:35:31", 525, 46750, "ATR EVO" },
                    { new Guid("1bc34e17-572b-45fa-93db-d5d155930fec"), "01-13-23", "11:35:31", 300, 31000, "Airbus A350" },
                    { new Guid("44033746-83fa-4729-b217-a07cac635358"), "01-13-23", "11:35:31", 320, 32400, "CRIAC CR929" },
                    { new Guid("87cad132-b126-4094-8341-c5c4c050a76c"), "01-13-23", "11:35:31", 125, 35600, "Airbus A220" },
                    { new Guid("c4b29207-26bf-4fcf-b082-68013a355703"), "01-13-23", "11:35:31", 180, 22600, "Comac C919" },
                    { new Guid("e54eea91-63bd-4b8e-acac-cee3c240531a"), "01-13-23", "11:35:31", 150, 22600, "Comac C919" },
                    { new Guid("ec992954-1dd0-47ed-9c3a-3d332007664f"), "01-13-23", "11:35:31", 525, 46750, "ATR EVO" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "CreatedTime" },
                values: new object[,]
                {
                    { new Guid("0f54863e-733b-47a4-87dd-2fdb494b2cd4"), "01-13-23", "11:35:31" },
                    { new Guid("40e28ec4-a526-4330-b333-d70627c2d8ef"), "01-13-23", "11:35:31" },
                    { new Guid("997bd07f-65ab-4eca-a95e-3dda96f315de"), "01-13-23", "11:35:31" }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "AirlineId", "AirplaneId", "Arrival", "ArrivingDate", "CreatedDate", "CreatedTime", "DepartureDate", "Departurer" },
                values: new object[,]
                {
                    { new Guid("05511a7b-3d32-4e15-8479-0d835486fd9e"), new Guid("078249c5-d887-4e17-966f-bbe1838b36f2"), new Guid("c4b29207-26bf-4fcf-b082-68013a355703"), "Mykonos", new DateTime(2023, 3, 23, 11, 15, 0, 0, DateTimeKind.Unspecified), "01-13-23", "11:35:31", new DateTime(2023, 3, 20, 20, 55, 0, 0, DateTimeKind.Unspecified), "Singapore" },
                    { new Guid("1fc32289-4054-4135-891c-38d104c23043"), new Guid("ad13f8da-9fb1-4b78-ae4a-9c5209b9ea8b"), new Guid("87cad132-b126-4094-8341-c5c4c050a76c"), "yaunde", new DateTime(2023, 7, 6, 3, 50, 0, 0, DateTimeKind.Unspecified), "01-13-23", "11:35:31", new DateTime(2023, 7, 3, 6, 50, 0, 0, DateTimeKind.Unspecified), "frankfurt" },
                    { new Guid("570ed468-e2e6-4434-885b-918b957c1a4e"), new Guid("5d114b2f-3dd0-4798-91b2-480bb3a51ced"), new Guid("00bbcc7d-784a-4be4-8f92-3cbe95875e8c"), "Boston", new DateTime(2023, 2, 19, 18, 20, 0, 0, DateTimeKind.Unspecified), "01-13-23", "11:35:31", new DateTime(2023, 2, 17, 22, 15, 0, 0, DateTimeKind.Unspecified), "Minsk" },
                    { new Guid("809a16aa-d678-4c27-8981-0e525d140ba8"), new Guid("5d114b2f-3dd0-4798-91b2-480bb3a51ced"), new Guid("44033746-83fa-4729-b217-a07cac635358"), "Paris", new DateTime(2023, 5, 13, 18, 55, 0, 0, DateTimeKind.Unspecified), "01-13-23", "11:35:31", new DateTime(2023, 5, 13, 10, 30, 0, 0, DateTimeKind.Unspecified), "Pekin" }
                });

            migrationBuilder.InsertData(
                table: "PersonInformations",
                columns: new[] { "Id", "BirthDate", "Citizenship", "CreatedDate", "CreatedTime", "ExpirePasportDate", "Gender", "IdentificationNumber", "Name", "Surname", "UserId" },
                values: new object[,]
                {
                    { new Guid("b91ac85a-5c34-458c-9629-df48ed67f4a0"), new DateTime(1993, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "America", "01-13-23", "11:35:31", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "М", "2763984J836PB3", "Владислав", "Лазарев", new Guid("997bd07f-65ab-4eca-a95e-3dda96f315de") },
                    { new Guid("feb76b82-21dc-4f8b-9a8d-f230bf702030"), new DateTime(1983, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Belarus", "01-13-23", "11:35:31", new DateTime(2027, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "М", "2763984J836PB3", "Александр", "Иванов", new Guid("0f54863e-733b-47a4-87dd-2fdb494b2cd4") },
                    { new Guid("ff731ca0-da40-4c14-9fdf-1e32f6916fab"), new DateTime(1998, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Poland", "01-13-23", "11:35:31", new DateTime(2025, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ж", "2763984J836PB3", "Мария", "Лебедева", new Guid("40e28ec4-a526-4330-b333-d70627c2d8ef") }
                });

            migrationBuilder.InsertData(
                table: "BoardingPasses",
                columns: new[] { "Id", "BookingExpireDate", "CreatedDate", "CreatedTime", "FlightId", "Prise", "UserId", "isExpired" },
                values: new object[,]
                {
                    { new Guid("2d4d18b0-16c6-4438-852c-28b0082e84a9"), new DateTime(2023, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "03-16-23", "15:22:48", new Guid("1fc32289-4054-4135-891c-38d104c23043"), 1401m, new Guid("40e28ec4-a526-4330-b333-d70627c2d8ef"), false },
                    { new Guid("73826c33-1e84-4f44-937d-e45842b51d3f"), new DateTime(2023, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "07-24-23", "19:36:16", new Guid("05511a7b-3d32-4e15-8479-0d835486fd9e"), 1672m, new Guid("997bd07f-65ab-4eca-a95e-3dda96f315de"), false },
                    { new Guid("73a30765-7117-4ea7-aee1-a64e211d7261"), new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "05-12-23", "12:17:3", new Guid("570ed468-e2e6-4434-885b-918b957c1a4e"), 778m, new Guid("0f54863e-733b-47a4-87dd-2fdb494b2cd4"), false },
                    { new Guid("a071cb0d-0646-4878-82fb-df847c9a24ce"), new DateTime(2023, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "02-06-23", "08:43:17", new Guid("809a16aa-d678-4c27-8981-0e525d140ba8"), 1661m, new Guid("0f54863e-733b-47a4-87dd-2fdb494b2cd4"), false }
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
