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
                    Rating = table.Column<double>(type: "float", nullable: false)
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
                    MaximumWeight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airplanes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    ArrivingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Prise = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    isExpired = table.Column<bool>(type: "bit", nullable: false),
                    BookingExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                columns: new[] { "Id", "AirlineName", "Rating" },
                values: new object[,]
                {
                    { new Guid("098acb74-29c7-46fc-8d62-839a51842205"), "American Airlines", 4.7000000000000002 },
                    { new Guid("2fbfb19d-a94a-41f4-ba03-4641a079f658"), "Belavia", 4.2999999999999998 },
                    { new Guid("b169abc0-4a87-4152-9187-8dc97d01c68f"), "Airnorth", 3.8999999999999999 }
                });

            migrationBuilder.InsertData(
                table: "Airplanes",
                columns: new[] { "Id", "MaximumSeats", "MaximumWeight", "ModelName" },
                values: new object[,]
                {
                    { new Guid("03c9e780-31c2-4599-92fa-ff580c13c646"), 525, 46750, "ATR EVO" },
                    { new Guid("143fea35-ded7-4e4b-8456-070275ebbec9"), 150, 22600, "Comac C919" },
                    { new Guid("576fbfa1-c520-4080-80a8-6d6e4db8092a"), 125, 35600, "Airbus A220" },
                    { new Guid("5f62643f-c965-4b42-be98-6a9d44a4f736"), 320, 32400, "CRIAC CR929" },
                    { new Guid("6b5c2f29-8cad-4555-ae40-afa7f38730e7"), 180, 22600, "Comac C919" },
                    { new Guid("ac0d2a55-4a27-419e-a7d4-4878109f3ec1"), 300, 31000, "Airbus A350" },
                    { new Guid("f4d8814e-bc80-4662-a5ba-c0fbdf717933"), 525, 46750, "ATR EVO" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                column: "Id",
                values: new object[]
                {
                    new Guid("0bc9828f-fdbe-4160-ad74-d9260678460c"),
                    new Guid("4106bf6d-2306-43a3-8614-e7818fd98f31"),
                    new Guid("efc4584c-2f31-411a-93c8-eff6ec3bb14f")
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "AirlineId", "AirplaneId", "Arrival", "ArrivingDate", "DepartureDate", "Departurer" },
                values: new object[,]
                {
                    { new Guid("229b7943-0ff5-4a26-8664-ed32b8a6569f"), new Guid("098acb74-29c7-46fc-8d62-839a51842205"), new Guid("6b5c2f29-8cad-4555-ae40-afa7f38730e7"), "Mykonos", new DateTime(2023, 3, 23, 11, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 20, 20, 55, 0, 0, DateTimeKind.Unspecified), "Singapore" },
                    { new Guid("25d1806a-d7e3-4deb-a449-41e9aa520314"), new Guid("b169abc0-4a87-4152-9187-8dc97d01c68f"), new Guid("576fbfa1-c520-4080-80a8-6d6e4db8092a"), "yaunde", new DateTime(2023, 7, 6, 3, 50, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 3, 6, 50, 0, 0, DateTimeKind.Unspecified), "frankfurt" },
                    { new Guid("908db434-d6f4-43f2-ac9e-fc8bfcf04455"), new Guid("2fbfb19d-a94a-41f4-ba03-4641a079f658"), new Guid("f4d8814e-bc80-4662-a5ba-c0fbdf717933"), "Boston", new DateTime(2023, 2, 19, 18, 20, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 17, 22, 15, 0, 0, DateTimeKind.Unspecified), "Minsk" },
                    { new Guid("c29c4308-90eb-4467-a43e-f6177038591d"), new Guid("2fbfb19d-a94a-41f4-ba03-4641a079f658"), new Guid("5f62643f-c965-4b42-be98-6a9d44a4f736"), "Paris", new DateTime(2023, 5, 13, 18, 55, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 13, 10, 30, 0, 0, DateTimeKind.Unspecified), "Pekin" }
                });

            migrationBuilder.InsertData(
                table: "PersonInformations",
                columns: new[] { "Id", "BirthDate", "Citizenship", "ExpirePasportDate", "Gender", "IdentificationNumber", "Name", "Surname", "UserId" },
                values: new object[,]
                {
                    { new Guid("2168ac13-d46d-4261-93cd-c88ce375c4d8"), new DateTime(1983, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Belarus", new DateTime(2027, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "М", "2763984J836PB3", "Александр", "Иванов", new Guid("4106bf6d-2306-43a3-8614-e7818fd98f31") },
                    { new Guid("259c9342-3fa8-476c-b3db-cb7a650d805a"), new DateTime(1993, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "America", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "М", "2763984J836PB3", "Владислав", "Лазарев", new Guid("0bc9828f-fdbe-4160-ad74-d9260678460c") },
                    { new Guid("3a0d00f9-188b-4b68-9f3f-daf543d30c92"), new DateTime(1998, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Poland", new DateTime(2025, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ж", "2763984J836PB3", "Мария", "Лебедева", new Guid("efc4584c-2f31-411a-93c8-eff6ec3bb14f") }
                });

            migrationBuilder.InsertData(
                table: "BoardingPasses",
                columns: new[] { "Id", "BookingExpireDate", "CreatedDate", "FlightId", "Prise", "UserId", "isExpired" },
                values: new object[,]
                {
                    { new Guid("5c7f70c2-ec74-4d52-9a44-9576322ad787"), new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("908db434-d6f4-43f2-ac9e-fc8bfcf04455"), 778m, new Guid("4106bf6d-2306-43a3-8614-e7818fd98f31"), false },
                    { new Guid("790bd9bb-c652-472d-91bd-2cbe1fe922ad"), new DateTime(2023, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c29c4308-90eb-4467-a43e-f6177038591d"), 1661m, new Guid("4106bf6d-2306-43a3-8614-e7818fd98f31"), false },
                    { new Guid("a128118e-98d7-45c8-8abb-45715e198636"), new DateTime(2023, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("229b7943-0ff5-4a26-8664-ed32b8a6569f"), 1672m, new Guid("0bc9828f-fdbe-4160-ad74-d9260678460c"), false },
                    { new Guid("c0dd45ed-aa2a-4d3a-8b8a-6ffba34cc3e6"), new DateTime(2023, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("25d1806a-d7e3-4deb-a449-41e9aa520314"), 1401m, new Guid("efc4584c-2f31-411a-93c8-eff6ec3bb14f"), false }
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
