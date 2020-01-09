using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Feedback_API.Migrations
{
    public partial class InitialEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlaceTypes",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    IsVerified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IsVerified = table.Column<bool>(nullable: false),
                    PlaceTypeID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Places_PlaceTypes_PlaceTypeID",
                        column: x => x.PlaceTypeID,
                        principalTable: "PlaceTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpeningTimes",
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
                    table.PrimaryKey("PK_OpeningTimes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OpeningTimes_Places_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "Places",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
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
                    table.PrimaryKey("PK_Reviews", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reviews_Places_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "Places",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reactions",
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
                    table.PrimaryKey("PK_Reactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reactions_Reviews_ReviewID",
                        column: x => x.ReviewID,
                        principalTable: "Reviews",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reactions_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PlaceTypes",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1L, "Café" },
                    { 2L, "Shoe Store" },
                    { 3L, "Fast Food Restaurant" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Address", "Description", "FirstName", "IsVerified", "LastName", "Username" },
                values: new object[,]
                {
                    { 1L, "3500 Krems an der Donau", null, "Peter", false, "Gustav", "pete" },
                    { 2L, "3500 Krems an der Donau", null, "John", false, "Gustav", "MrJohn" },
                    { 3L, "3500 Krems an der Donau", null, "Heinz", false, "Gustav", "Ketchup" },
                    { 4L, "3500 Krems an der Donau", null, "Olaf", false, "Gustav", "Olaf" },
                    { 5L, "3500 Krems an der Donau", null, "Hans", false, "Gustav", "hansi12" }
                });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "ID", "Address", "IsVerified", "Name", "PlaceTypeID" },
                values: new object[] { 1L, "3500 Krems an der Donau", true, "Coffeehut", 1L });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "ID", "Address", "IsVerified", "Name", "PlaceTypeID" },
                values: new object[] { 2L, "3500 Krems an der Donau", true, "Footly", 2L });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "ID", "Address", "IsVerified", "Name", "PlaceTypeID" },
                values: new object[] { 3L, "3500 Krems an der Donau", true, "Gusto Generic", 3L });

            migrationBuilder.InsertData(
                table: "OpeningTimes",
                columns: new[] { "ID", "Close", "Day", "Open", "PlaceID" },
                values: new object[,]
                {
                    { 3L, new TimeSpan(0, 22, 0, 0, 0), 0, new TimeSpan(0, 10, 0, 0, 0), 1L },
                    { 1L, new TimeSpan(0, 20, 0, 0, 0), 0, new TimeSpan(0, 8, 0, 0, 0), 2L },
                    { 2L, new TimeSpan(0, 19, 0, 0, 0), 0, new TimeSpan(0, 9, 0, 0, 0), 3L }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ID", "PlaceID", "Rating", "Text", "Time", "UserID" },
                values: new object[,]
                {
                    { 2L, 1L, 5, "nice", new DateTime(2020, 1, 9, 10, 55, 21, 836, DateTimeKind.Local).AddTicks(1651), 1L },
                    { 1L, 2L, 2, "meh", new DateTime(2020, 1, 9, 10, 55, 21, 832, DateTimeKind.Local).AddTicks(8812), 2L }
                });

            migrationBuilder.InsertData(
                table: "Reactions",
                columns: new[] { "ID", "IsHelpful", "ReviewID", "UserID" },
                values: new object[] { 1L, false, 2L, 3L });

            migrationBuilder.InsertData(
                table: "Reactions",
                columns: new[] { "ID", "IsHelpful", "ReviewID", "UserID" },
                values: new object[] { 2L, true, 1L, 2L });

            migrationBuilder.CreateIndex(
                name: "IX_OpeningTimes_PlaceID",
                table: "OpeningTimes",
                column: "PlaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Places_PlaceTypeID",
                table: "Places",
                column: "PlaceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_ReviewID",
                table: "Reactions",
                column: "ReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_UserID",
                table: "Reactions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PlaceID",
                table: "Reviews",
                column: "PlaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserID",
                table: "Reviews",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpeningTimes");

            migrationBuilder.DropTable(
                name: "Reactions");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PlaceTypes");
        }
    }
}
