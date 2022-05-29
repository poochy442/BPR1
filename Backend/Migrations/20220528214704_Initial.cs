using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostalCode = table.Column<string>(type: "varchar(50)", nullable: true),
                    City = table.Column<string>(type: "varchar(50)", nullable: true),
                    Street = table.Column<string>(type: "varchar(50)", nullable: true),
                    StreetNo = table.Column<string>(type: "varchar(50)", nullable: true),
                    Longtitude = table.Column<decimal>(type: "decimal(12,9)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(12,9)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Restrictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<bool>(type: "bit", nullable: false),
                    Handicap = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restrictions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", nullable: true),
                    Claims = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "varchar(50)", nullable: true),
                    PhoneNo = table.Column<string>(type: "varchar(50)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", nullable: true),
                    FoodType = table.Column<string>(type: "varchar(50)", nullable: true),
                    StudentDiscount = table.Column<int>(type: "int", nullable: true),
                    WorkingHours = table.Column<string>(type: "varchar(400)", nullable: true),
                    TotalScore = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restaurants_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Restaurants_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "varchar(200)", nullable: true),
                    RestaurantId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ratings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableNo = table.Column<int>(type: "int", nullable: false),
                    Seats = table.Column<int>(type: "int", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "varchar(50)", nullable: true),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BookingTimes = table.Column<string>(type: "varchar(400)", nullable: true),
                    RestaurantId = table.Column<int>(type: "int", nullable: true),
                    RestrictionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tables_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tables_Restrictions_RestrictionId",
                        column: x => x.RestrictionId,
                        principalTable: "Restrictions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GuestNo = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "varchar(50)", nullable: true),
                    RestaurantId = table.Column<int>(type: "int", nullable: true),
                    TableId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bookings_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Latitude", "Longtitude", "PostalCode", "Street", "StreetNo" },
                values: new object[,]
                {
                    { 1, "Horsens", 55.86155m, 9.85451m, "8700", "Borgergade", "15" },
                    { 2, "Horsens", 55.87022m, 9.8643m, "8700", "Flintebakken", "1" },
                    { 3, "Horsens", 55.86142m, 9.85619m, "8700", "Borgergade", "25" }
                });

            migrationBuilder.InsertData(
                table: "Restrictions",
                columns: new[] { "Id", "Age", "Handicap" },
                values: new object[,]
                {
                    { 1, true, false },
                    { 2, false, true },
                    { 3, true, true }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Claims", "Name" },
                values: new object[,]
                {
                    { 1, "Customer", "CustomerRole" },
                    { 2, "RestaurantManager", "RestaurantManagerRole" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "PhoneNo", "RoleId" },
                values: new object[] { 1, "user@email.com", "Steve G", "$2a$11$YPB10zErqK1sD2h61D0xkupRJMObqFlaGoXcZ5TcVuIij.oWZlZGy", "+4596254585", 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "PhoneNo", "RoleId" },
                values: new object[] { 2, "manager@email.com", "Clive L", "$2a$11$Y7zhcGWJn.Ym8gp6XmvOe.m09CcAnynFZtV6eL7Hc9Tk9ffMCYWVK", "+4532124565", 2 });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "AddressId", "FoodType", "Name", "StudentDiscount", "TotalScore", "UserId", "WorkingHours" },
                values: new object[] { 1, 1, "Pizza", "Pizza King", 5, 4m, 2, "[\r\n{\r\n\"Day\" : 0,\r\n\"From\": \"09:00\",\r\n\"Till\": \"20:00\"\r\n},\r\n{\r\n\"Day\" : 1,\r\n\"From\": \"09:00\",\r\n\"Till\": \"20:00\"\r\n},\r\n{\r\n\"Day\" : 2,\r\n\"From\": \"09:00\",\r\n\"Till\": \"20:00\"\r\n},\r\n{\r\n\"Day\" : 3,\r\n\"From\": \"09:00\",\r\n\"Till\": \"20:00\"\r\n},\r\n{\r\n\"Day\" : 4,\r\n\"From\": \"09:00\",\r\n\"Till\": \"20:00\"\r\n},\r\n{\r\n\"Day\" : 5,\r\n\"From\": \"10:00\",\r\n\"Till\": \"22:00\"\r\n},\r\n{\r\n\"Day\" : 6,\r\n\"From\": \"10:00\",\r\n\"Till\": \"22:00\"\r\n}\r\n]" });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "AddressId", "FoodType", "Name", "StudentDiscount", "TotalScore", "UserId", "WorkingHours" },
                values: new object[] { 2, 2, "Fast Food", "Mc Donald's", 7, 4.4m, 2, "[\r\n{\r\n\"Day\" : 0,\r\n\"From\": \"07:00\",\r\n\"Till\": \"13:00\"\r\n},\r\n{\r\n\"Day\" : 1,\r\n\"From\": \"07:00\",\r\n\"Till\": \"13:00\"\r\n},\r\n{\r\n\"Day\" : 2,\r\n\"From\": \"07:00\",\r\n\"Till\": \"13:00\"\r\n},\r\n{\r\n\"Day\" : 3,\r\n\"From\": \"07:00\",\r\n\"Till\": \"13:00\"\r\n},\r\n{\r\n\"Day\" : 4,\r\n\"From\": \"07:00\",\r\n\"Till\": \"15:00\"\r\n},\r\n{\r\n\"Day\" : 5,\r\n\"From\": \"07:00\",\r\n\"Till\": \"15:00\"\r\n},\r\n{\r\n\"Day\" : 6,\r\n\"From\": \"07:00\",\r\n\"Till\": \"13:00\"\r\n}\r\n]" });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "AddressId", "FoodType", "Name", "StudentDiscount", "TotalScore", "UserId", "WorkingHours" },
                values: new object[] { 3, 3, "Italian", "Venezia", 5, 4.2m, 2, "[\r\n{\r\n\"Day\" : 0,\r\n\"From\": \"00:00\",\r\n\"Till\": \"00:00\"\r\n},\r\n{\r\n\"Day\" : 1,\r\n\"From\": \"16:30\",\r\n\"Till\": \"21:00\"\r\n},\r\n{\r\n\"Day\" : 2,\r\n\"From\": \"16:30\",\r\n\"Till\": \"21:00\"\r\n},\r\n{\r\n\"Day\" : 3,\r\n\"From\": \"16:30\",\r\n\"Till\": \"21:00\"\r\n},\r\n{\r\n\"Day\" : 4,\r\n\"From\": \"16:30\",\r\n\"Till\": \"22:00\"\r\n},\r\n{\r\n\"Day\" : 5,\r\n\"From\": \"16:30\",\r\n\"Till\": \"22:00\"\r\n},\r\n{\r\n\"Day\" : 6,\r\n\"From\": \"16:30\",\r\n\"Till\": \"21:00\"\r\n}\r\n]" });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "Available", "BookingTimes", "Deadline", "Notes", "RestaurantId", "RestrictionId", "Seats", "TableNo" },
                values: new object[,]
                {
                    { 1, true, null, new DateTime(1970, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 1, 2, 1 },
                    { 2, true, null, new DateTime(1970, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, 2, 2 },
                    { 3, true, null, new DateTime(1970, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 2, 4, 3 },
                    { 4, true, null, new DateTime(1970, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, 4, 4 },
                    { 5, true, null, new DateTime(1970, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, 6, 5 },
                    { 6, true, null, new DateTime(1970, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, 6, 6 }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "Date", "EndDate", "GuestNo", "Note", "RestaurantId", "StartDate", "TableId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 24, 10, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 1, new DateTime(2022, 6, 24, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 24, 15, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 1, new DateTime(2022, 6, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 3, new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 24, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 1, new DateTime(2022, 6, 24, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 4, new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 1, new DateTime(2022, 6, 24, 10, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 },
                    { 5, new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 24, 17, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 1, new DateTime(2022, 6, 24, 15, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RestaurantId",
                table: "Bookings",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TableId",
                table: "Bookings",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RestaurantId",
                table: "Ratings",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_AddressId",
                table: "Restaurants",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_UserId",
                table: "Restaurants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_RestaurantId",
                table: "Tables",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_RestrictionId",
                table: "Tables",
                column: "RestrictionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropTable(
                name: "Restrictions");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
