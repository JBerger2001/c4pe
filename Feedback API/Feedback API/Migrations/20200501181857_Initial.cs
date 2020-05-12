using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Feedback_API.Migrations
{
    public partial class Initial : Migration
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
                    Role = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    IsVerified = table.Column<bool>(nullable: false),
                    AvatarURI = table.Column<string>(nullable: true)
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
                    Street = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
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
                name: "PlaceImages",
                columns: table => new
                {
                    PlaceID = table.Column<long>(nullable: false),
                    ID = table.Column<long>(nullable: false),
                    URI = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceImages", x => new { x.PlaceID, x.ID });
                    table.ForeignKey(
                        name: "FK_PlaceImages_places_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "places",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "placeowner",
                columns: table => new
                {
                    UserID = table.Column<long>(nullable: false),
                    PlaceID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_placeowner", x => new { x.PlaceID, x.UserID });
                    table.ForeignKey(
                        name: "FK_placeowner_places_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "places",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_placeowner_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
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
                    LastEdited = table.Column<DateTime>(nullable: true),
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
                    ReviewID = table.Column<long>(nullable: false),
                    UserID = table.Column<long>(nullable: false),
                    IsHelpful = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reactions", x => new { x.ReviewID, x.UserID });
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
                columns: new[] { "ID", "AvatarURI", "City", "Country", "Description", "FirstName", "IsVerified", "LastName", "PasswordHash", "PasswordSalt", "Role", "Street", "Username", "ZipCode" },
                values: new object[,]
                {
                    { 1L, null, "Krems an der Donau", "AT", null, "Peter", false, "Gustav", new byte[] { 37, 232, 0, 42, 221, 38, 163, 99, 60, 182, 218, 215, 93, 75, 250, 166, 23, 198, 219, 51, 109, 56, 241, 176, 4, 58, 248, 249, 59, 165, 184, 107, 38, 228, 124, 171, 20, 252, 225, 99, 158, 90, 131, 110, 83, 14, 243, 9, 110, 108, 114, 221, 45, 115, 234, 129, 29, 226, 48, 20, 216, 186, 248, 33 }, new byte[] { 223, 203, 243, 107, 252, 115, 69, 41, 82, 196, 209, 242, 28, 172, 199, 84, 251, 239, 148, 147, 7, 160, 142, 241, 51, 147, 177, 217, 154, 186, 132, 141, 145, 62, 85, 95, 19, 80, 2, 40, 156, 164, 144, 55, 190, 196, 79, 124, 38, 218, 124, 221, 42, 93, 129, 40, 42, 27, 31, 50, 171, 146, 244, 188, 42, 1, 80, 207, 78, 61, 226, 3, 215, 249, 8, 59, 55, 244, 30, 75, 207, 79, 59, 141, 199, 197, 224, 99, 229, 175, 134, 239, 243, 118, 139, 16, 3, 238, 140, 186, 170, 157, 164, 59, 116, 117, 202, 151, 60, 26, 238, 153, 161, 223, 34, 179, 216, 86, 199, 111, 171, 93, 163, 175, 172, 135, 32, 43 }, "User", "Example Street 1", "pete", "3500" },
                    { 2L, null, "Krems an der Donau", "AT", null, "John", false, "Gustav", new byte[] { 31, 57, 39, 155, 116, 239, 63, 203, 255, 228, 239, 129, 101, 137, 56, 52, 21, 146, 218, 134, 16, 174, 13, 235, 42, 16, 181, 95, 102, 255, 164, 208, 111, 192, 105, 122, 20, 120, 115, 45, 137, 200, 97, 193, 200, 28, 189, 254, 220, 195, 253, 82, 46, 115, 165, 215, 246, 44, 171, 141, 122, 113, 41, 136 }, new byte[] { 247, 147, 172, 128, 210, 39, 40, 58, 107, 157, 58, 13, 145, 213, 228, 4, 132, 128, 119, 48, 237, 236, 114, 146, 64, 157, 62, 84, 88, 91, 121, 0, 39, 133, 50, 142, 24, 225, 246, 143, 78, 56, 222, 165, 227, 222, 37, 201, 56, 54, 180, 248, 9, 228, 131, 114, 56, 78, 86, 250, 178, 13, 141, 52, 85, 135, 249, 147, 138, 242, 155, 46, 156, 52, 90, 13, 59, 194, 180, 123, 93, 164, 67, 114, 9, 29, 195, 205, 110, 120, 95, 123, 239, 41, 119, 206, 157, 179, 32, 138, 34, 57, 100, 57, 141, 5, 149, 156, 207, 56, 227, 79, 40, 125, 122, 147, 25, 116, 101, 130, 112, 153, 206, 72, 105, 221, 241, 137 }, "User", "Example Street 2", "MrJohn", "3500" },
                    { 3L, null, "Krems an der Donau", "AT", null, "Heinz", false, "Gustav", new byte[] { 176, 163, 59, 5, 234, 200, 81, 166, 76, 164, 223, 131, 24, 160, 191, 104, 223, 2, 211, 150, 249, 221, 27, 113, 76, 215, 202, 180, 72, 199, 195, 112, 201, 10, 45, 175, 22, 102, 134, 235, 76, 233, 30, 220, 253, 239, 179, 150, 15, 105, 129, 99, 158, 249, 115, 142, 55, 158, 129, 93, 104, 136, 77, 132 }, new byte[] { 185, 155, 107, 236, 7, 154, 210, 16, 138, 248, 187, 11, 152, 208, 134, 166, 254, 99, 178, 212, 82, 235, 65, 81, 236, 49, 124, 185, 12, 71, 225, 113, 117, 211, 67, 15, 152, 251, 16, 12, 16, 250, 156, 60, 170, 71, 174, 239, 248, 52, 241, 221, 6, 191, 52, 248, 114, 224, 21, 216, 138, 105, 28, 214, 230, 222, 193, 119, 104, 187, 109, 105, 241, 138, 119, 126, 102, 248, 25, 40, 217, 147, 230, 243, 207, 49, 40, 72, 131, 40, 219, 86, 61, 98, 134, 229, 55, 202, 234, 251, 231, 68, 58, 152, 67, 75, 197, 73, 60, 230, 194, 34, 212, 81, 60, 248, 244, 145, 7, 66, 180, 132, 185, 111, 5, 31, 241, 151 }, "User", "Example Street 3", "Ketchup", "3500" },
                    { 4L, null, "Krems an der Donau", "AT", null, "Olaf", false, "Gustav", new byte[] { 156, 95, 222, 186, 26, 144, 93, 123, 53, 44, 135, 49, 95, 244, 206, 230, 90, 213, 147, 5, 221, 83, 123, 232, 5, 190, 13, 148, 67, 131, 195, 75, 193, 235, 53, 167, 155, 176, 144, 131, 155, 69, 215, 47, 196, 18, 56, 151, 71, 163, 73, 96, 205, 107, 118, 201, 50, 254, 216, 128, 56, 127, 118, 199 }, new byte[] { 117, 151, 66, 97, 132, 208, 47, 223, 232, 59, 204, 178, 125, 110, 123, 144, 0, 158, 146, 176, 150, 190, 102, 216, 179, 78, 13, 53, 244, 204, 118, 67, 6, 39, 120, 189, 18, 23, 180, 6, 86, 222, 74, 207, 31, 217, 196, 132, 232, 135, 254, 167, 235, 175, 104, 164, 251, 96, 5, 30, 57, 54, 215, 238, 198, 85, 65, 181, 126, 149, 187, 119, 203, 203, 183, 155, 56, 150, 76, 59, 22, 205, 134, 131, 162, 203, 68, 80, 83, 203, 168, 47, 196, 220, 160, 229, 239, 121, 123, 70, 198, 6, 178, 53, 4, 218, 0, 199, 71, 241, 20, 246, 236, 126, 114, 18, 100, 44, 249, 17, 245, 103, 7, 197, 94, 90, 246, 175 }, "User", "Example Street 4", "Olaf", "3500" },
                    { 5L, null, "Krems an der Donau", "AT", null, "Hans", false, "Gustav", new byte[] { 49, 193, 115, 142, 186, 139, 252, 101, 234, 40, 198, 123, 72, 69, 227, 242, 204, 116, 199, 226, 110, 172, 113, 49, 98, 216, 102, 254, 93, 216, 239, 132, 34, 28, 238, 85, 127, 108, 221, 110, 120, 148, 80, 81, 139, 149, 209, 4, 248, 42, 232, 3, 241, 136, 132, 228, 182, 55, 90, 51, 92, 175, 251, 208 }, new byte[] { 213, 38, 196, 215, 66, 2, 93, 245, 44, 250, 127, 101, 197, 141, 43, 82, 10, 24, 22, 210, 120, 234, 244, 123, 102, 170, 128, 168, 137, 105, 140, 140, 240, 136, 30, 207, 243, 121, 25, 43, 113, 128, 213, 197, 175, 236, 195, 162, 188, 35, 119, 171, 42, 110, 205, 233, 69, 130, 136, 225, 19, 197, 169, 89, 150, 147, 137, 118, 190, 248, 111, 164, 221, 46, 37, 94, 48, 134, 241, 153, 161, 92, 189, 130, 14, 73, 108, 219, 225, 78, 144, 119, 29, 238, 243, 244, 66, 42, 158, 75, 165, 29, 44, 149, 202, 81, 230, 27, 223, 130, 176, 125, 91, 67, 3, 223, 198, 247, 64, 154, 82, 76, 155, 253, 142, 15, 188, 56 }, "User", "Example Street 5", "hansi12", "3500" },
                    { 6L, null, "localhost", "yes", null, "Bobby", false, "Tables", new byte[] { 48, 63, 14, 170, 255, 122, 46, 124, 202, 69, 153, 145, 205, 109, 162, 71, 52, 245, 117, 166, 211, 222, 19, 95, 200, 146, 195, 243, 150, 186, 82, 240, 186, 84, 104, 247, 162, 218, 72, 230, 95, 140, 11, 195, 247, 49, 112, 119, 133, 223, 52, 185, 236, 96, 245, 16, 224, 31, 17, 223, 161, 203, 143, 50 }, new byte[] { 239, 34, 76, 199, 21, 212, 219, 106, 111, 119, 202, 107, 74, 187, 128, 138, 154, 5, 252, 39, 251, 161, 160, 232, 87, 170, 79, 230, 77, 19, 176, 22, 183, 110, 11, 152, 215, 123, 183, 150, 2, 20, 146, 14, 118, 128, 210, 8, 154, 72, 67, 241, 47, 249, 149, 211, 24, 243, 210, 133, 234, 207, 122, 237, 120, 184, 13, 144, 218, 129, 221, 12, 167, 142, 213, 79, 93, 149, 81, 62, 76, 120, 96, 73, 135, 12, 11, 104, 153, 122, 203, 14, 212, 100, 84, 243, 215, 20, 247, 69, 119, 74, 21, 132, 104, 239, 204, 89, 131, 248, 184, 14, 210, 129, 64, 184, 213, 111, 241, 67, 108, 184, 21, 208, 13, 176, 129, 230 }, "Admin", "443", "admin", "1337" }
                });

            migrationBuilder.InsertData(
                table: "places",
                columns: new[] { "ID", "City", "Country", "IsVerified", "Name", "PlaceTypeID", "Street", "ZipCode" },
                values: new object[] { 1L, "Krems an der Donau", "AT", true, "Coffeehut", 1L, "City Street 1", "3500" });

            migrationBuilder.InsertData(
                table: "places",
                columns: new[] { "ID", "City", "Country", "IsVerified", "Name", "PlaceTypeID", "Street", "ZipCode" },
                values: new object[] { 2L, "Krems an der Donau", "AT", true, "Footly", 2L, "City Street 2", "3500" });

            migrationBuilder.InsertData(
                table: "places",
                columns: new[] { "ID", "City", "Country", "IsVerified", "Name", "PlaceTypeID", "Street", "ZipCode" },
                values: new object[] { 3L, "Krems an der Donau", "AT", true, "Gusto Generic", 3L, "City Street 3", "3500" });

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
                table: "placeowner",
                columns: new[] { "PlaceID", "UserID" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 2L, 2L },
                    { 3L, 3L }
                });

            migrationBuilder.InsertData(
                table: "reviews",
                columns: new[] { "ID", "LastEdited", "PlaceID", "Rating", "Text", "Time", "UserID" },
                values: new object[,]
                {
                    { 2L, null, 1L, 5, "nice", new DateTime(2020, 5, 1, 20, 18, 56, 913, DateTimeKind.Local).AddTicks(3589), 1L },
                    { 3L, null, 1L, 4, "pretty good", new DateTime(2020, 5, 1, 20, 18, 56, 913, DateTimeKind.Local).AddTicks(3642), 2L },
                    { 1L, null, 2L, 2, "meh", new DateTime(2020, 5, 1, 20, 18, 56, 910, DateTimeKind.Local).AddTicks(7658), 2L }
                });

            migrationBuilder.InsertData(
                table: "reactions",
                columns: new[] { "ReviewID", "UserID", "IsHelpful" },
                values: new object[] { 2L, 3L, false });

            migrationBuilder.InsertData(
                table: "reactions",
                columns: new[] { "ReviewID", "UserID", "IsHelpful" },
                values: new object[] { 1L, 2L, true });

            migrationBuilder.CreateIndex(
                name: "IX_openingtimes_PlaceID",
                table: "openingtimes",
                column: "PlaceID");

            migrationBuilder.CreateIndex(
                name: "IX_placeowner_UserID",
                table: "placeowner",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_places_PlaceTypeID",
                table: "places",
                column: "PlaceTypeID");

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
                name: "PlaceImages");

            migrationBuilder.DropTable(
                name: "placeowner");

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
