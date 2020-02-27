using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Feedback_API.Migrations
{
    public partial class inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "placetypes",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_placetypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    IsVerified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "places",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    IsVerified = table.Column<bool>(nullable: false),
                    PlaceTypeID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_places", x => x.ID);
                    table.ForeignKey(
                        name: "FK_places_placetypes_PlaceTypeID",
                        column: x => x.PlaceTypeID,
                        principalTable: "placetypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "openingtimes",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PlaceID = table.Column<long>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    Open = table.Column<TimeSpan>(nullable: false),
                    Close = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_openingtimes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_openingtimes_places_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "places",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<long>(nullable: false),
                    PlaceID = table.Column<long>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.ID);
                    table.ForeignKey(
                        name: "FK_reviews_places_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "places",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reviews_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reactions",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<long>(nullable: false),
                    ReviewID = table.Column<long>(nullable: false),
                    IsHelpful = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_reactions_reviews_ReviewID",
                        column: x => x.ReviewID,
                        principalTable: "reviews",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reactions_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "placetypes",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1L, "Café" },
                    { 2L, "Shoe Store" },
                    { 3L, "Fast Food Restaurant" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "ID", "City", "Country", "Description", "FirstName", "IsVerified", "LastName", "PasswordHash", "PasswordSalt", "Street", "Username", "ZipCode" },
                values: new object[,]
                {
                    { 1L, "Krems an der Donau", "AT", null, "Peter", false, "Gustav", new byte[] { 0 }, new byte[] { 0 }, "Example Street 1", "pete", "3500" },
                    { 2L, "Krems an der Donau", "AT", null, "John", false, "Gustav", new byte[] { 0 }, new byte[] { 0 }, "Example Street 2", "MrJohn", "3500" },
                    { 3L, "Krems an der Donau", "AT", null, "Heinz", false, "Gustav", new byte[] { 0 }, new byte[] { 0 }, "Example Street 3", "Ketchup", "3500" },
                    { 4L, "Krems an der Donau", "AT", null, "Olaf", false, "Gustav", new byte[] { 0 }, new byte[] { 0 }, "Example Street 4", "Olaf", "3500" },
                    { 5L, "Krems an der Donau", "AT", null, "Hans", false, "Gustav", new byte[] { 0 }, new byte[] { 0 }, "Example Street 5", "hansi12", "3500" }
                });

            migrationBuilder.InsertData(
                table: "places",
                columns: new[] { "ID", "Address", "IsVerified", "Name", "PlaceTypeID" },
                values: new object[] { 1L, "3500 Krems an der Donau", true, "Coffeehut", 1L });

            migrationBuilder.InsertData(
                table: "places",
                columns: new[] { "ID", "Address", "IsVerified", "Name", "PlaceTypeID" },
                values: new object[] { 2L, "3500 Krems an der Donau", true, "Footly", 2L });

            migrationBuilder.InsertData(
                table: "places",
                columns: new[] { "ID", "Address", "IsVerified", "Name", "PlaceTypeID" },
                values: new object[] { 3L, "3500 Krems an der Donau", true, "Gusto Generic", 3L });

            migrationBuilder.InsertData(
                table: "openingtimes",
                columns: new[] { "ID", "Close", "Day", "Open", "PlaceID" },
                values: new object[,]
                {
                    { 3L, new TimeSpan(0, 22, 0, 0, 0), 0, new TimeSpan(0, 10, 0, 0, 0), 1L },
                    { 1L, new TimeSpan(0, 20, 0, 0, 0), 0, new TimeSpan(0, 8, 0, 0, 0), 2L },
                    { 2L, new TimeSpan(0, 19, 0, 0, 0), 0, new TimeSpan(0, 9, 0, 0, 0), 3L }
                });

            migrationBuilder.InsertData(
                table: "reviews",
                columns: new[] { "ID", "PlaceID", "Rating", "Text", "Time", "UserID" },
                values: new object[,]
                {
                    { 2L, 1L, 5, "nice", new DateTime(2020, 2, 27, 14, 29, 39, 845, DateTimeKind.Local).AddTicks(5482), 1L },
                    { 1L, 2L, 2, "meh", new DateTime(2020, 2, 27, 14, 29, 39, 842, DateTimeKind.Local).AddTicks(5742), 2L }
                });

            migrationBuilder.InsertData(
                table: "reactions",
                columns: new[] { "ID", "IsHelpful", "ReviewID", "UserID" },
                values: new object[] { 1L, false, 2L, 3L });

            migrationBuilder.InsertData(
                table: "reactions",
                columns: new[] { "ID", "IsHelpful", "ReviewID", "UserID" },
                values: new object[] { 2L, true, 1L, 2L });

            migrationBuilder.CreateIndex(
                name: "IX_openingtimes_PlaceID",
                table: "openingtimes",
                column: "PlaceID");

            migrationBuilder.CreateIndex(
                name: "IX_places_PlaceTypeID",
                table: "places",
                column: "PlaceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_reactions_ReviewID",
                table: "reactions",
                column: "ReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_reactions_UserID",
                table: "reactions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_PlaceID",
                table: "reviews",
                column: "PlaceID");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_UserID",
                table: "reviews",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "openingtimes");

            migrationBuilder.DropTable(
                name: "reactions");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "places");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "placetypes");
        }
    }
}
