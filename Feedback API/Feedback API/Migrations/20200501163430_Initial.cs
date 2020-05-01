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
                    OwnerID = table.Column<long>(nullable: false),
                    PlaceID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_placeowner", x => new { x.PlaceID, x.OwnerID });
                    table.ForeignKey(
                        name: "FK_placeowner_places_PlaceID",
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
                    { 1L, null, "Krems an der Donau", "AT", null, "Peter", false, "Gustav", new byte[] { 112, 208, 7, 234, 45, 84, 135, 62, 206, 208, 196, 41, 205, 86, 201, 2, 150, 39, 254, 198, 149, 231, 165, 118, 25, 98, 52, 184, 174, 161, 59, 98, 120, 135, 100, 68, 139, 239, 167, 249, 114, 101, 194, 47, 152, 70, 212, 132, 212, 131, 184, 157, 36, 96, 153, 246, 205, 7, 98, 129, 29, 255, 51, 200 }, new byte[] { 223, 144, 34, 59, 104, 179, 34, 218, 71, 99, 55, 191, 106, 137, 58, 208, 159, 35, 211, 221, 110, 36, 231, 49, 98, 1, 179, 53, 153, 43, 77, 195, 146, 253, 26, 193, 146, 21, 49, 218, 193, 8, 195, 81, 64, 145, 44, 250, 3, 168, 26, 65, 200, 37, 191, 131, 29, 113, 29, 230, 2, 218, 160, 171, 197, 116, 107, 117, 224, 197, 95, 46, 137, 152, 250, 5, 156, 132, 51, 245, 66, 130, 173, 59, 60, 109, 56, 94, 72, 210, 22, 176, 49, 177, 131, 238, 93, 198, 168, 84, 10, 232, 111, 98, 83, 169, 90, 90, 25, 174, 119, 236, 38, 55, 121, 43, 253, 165, 90, 6, 194, 153, 228, 234, 162, 99, 118, 211 }, "User", "Example Street 1", "pete", "3500" },
                    { 2L, null, "Krems an der Donau", "AT", null, "John", false, "Gustav", new byte[] { 95, 53, 140, 58, 67, 233, 173, 54, 129, 157, 63, 141, 147, 75, 69, 180, 22, 32, 251, 32, 53, 28, 57, 209, 31, 65, 109, 197, 222, 94, 89, 16, 184, 3, 17, 1, 202, 105, 18, 135, 198, 97, 193, 162, 123, 1, 32, 31, 108, 197, 74, 180, 197, 193, 152, 229, 63, 45, 92, 175, 245, 38, 234, 196 }, new byte[] { 50, 14, 87, 72, 22, 52, 1, 67, 114, 89, 28, 240, 107, 204, 68, 213, 169, 205, 36, 100, 161, 243, 181, 51, 48, 84, 228, 161, 207, 2, 112, 180, 203, 255, 163, 137, 160, 28, 45, 27, 249, 60, 212, 97, 55, 154, 175, 239, 225, 163, 234, 98, 208, 212, 228, 193, 254, 172, 184, 26, 68, 169, 62, 83, 58, 93, 160, 207, 91, 157, 6, 149, 248, 15, 71, 102, 252, 123, 171, 4, 175, 52, 37, 182, 215, 79, 144, 17, 102, 53, 83, 235, 78, 111, 153, 200, 25, 193, 208, 146, 98, 205, 127, 234, 172, 216, 47, 39, 32, 160, 243, 224, 133, 104, 205, 23, 171, 248, 175, 186, 199, 186, 69, 247, 166, 239, 73, 1 }, "User", "Example Street 2", "MrJohn", "3500" },
                    { 3L, null, "Krems an der Donau", "AT", null, "Heinz", false, "Gustav", new byte[] { 159, 216, 253, 149, 132, 80, 80, 0, 169, 3, 4, 158, 251, 4, 127, 5, 232, 116, 122, 183, 20, 166, 45, 199, 44, 168, 75, 158, 135, 247, 246, 16, 189, 39, 140, 243, 205, 57, 20, 48, 66, 175, 46, 154, 41, 215, 95, 194, 15, 111, 226, 148, 96, 59, 220, 204, 122, 22, 204, 143, 183, 190, 116, 248 }, new byte[] { 115, 148, 241, 157, 56, 22, 224, 68, 28, 173, 124, 250, 2, 79, 146, 231, 13, 20, 171, 10, 243, 200, 190, 170, 10, 105, 212, 70, 145, 55, 153, 219, 191, 143, 195, 192, 169, 49, 21, 234, 244, 219, 81, 13, 1, 188, 17, 58, 232, 54, 28, 10, 125, 85, 20, 138, 116, 224, 247, 226, 62, 91, 133, 90, 50, 41, 223, 226, 108, 177, 138, 24, 172, 85, 40, 126, 155, 73, 242, 110, 231, 122, 167, 153, 48, 238, 14, 221, 251, 227, 229, 13, 133, 119, 128, 56, 112, 150, 61, 129, 204, 161, 33, 222, 48, 41, 98, 93, 106, 99, 45, 202, 221, 223, 57, 95, 78, 66, 134, 149, 190, 227, 148, 255, 185, 240, 31, 17 }, "User", "Example Street 3", "Ketchup", "3500" },
                    { 4L, null, "Krems an der Donau", "AT", null, "Olaf", false, "Gustav", new byte[] { 58, 177, 128, 191, 78, 141, 222, 165, 128, 160, 208, 172, 46, 43, 93, 176, 94, 117, 104, 177, 170, 55, 249, 102, 89, 192, 117, 214, 70, 245, 26, 45, 61, 157, 159, 36, 82, 62, 112, 211, 200, 252, 4, 25, 103, 126, 5, 169, 148, 202, 9, 215, 247, 224, 248, 1, 255, 114, 16, 214, 95, 216, 143, 239 }, new byte[] { 2, 247, 189, 255, 52, 19, 7, 181, 207, 108, 167, 186, 248, 198, 212, 173, 185, 91, 123, 234, 39, 205, 193, 194, 139, 30, 30, 123, 30, 62, 243, 195, 109, 29, 70, 139, 170, 22, 241, 32, 208, 230, 14, 241, 23, 165, 77, 130, 5, 58, 112, 64, 216, 133, 95, 252, 21, 113, 59, 171, 194, 227, 66, 53, 166, 163, 107, 45, 232, 78, 204, 189, 196, 100, 192, 12, 247, 17, 74, 23, 91, 149, 236, 158, 193, 175, 205, 87, 137, 62, 218, 22, 130, 91, 223, 167, 54, 186, 179, 158, 43, 130, 72, 16, 39, 108, 45, 19, 210, 30, 6, 30, 113, 194, 147, 8, 47, 56, 207, 253, 119, 164, 56, 34, 22, 177, 191, 172 }, "User", "Example Street 4", "Olaf", "3500" },
                    { 5L, null, "Krems an der Donau", "AT", null, "Hans", false, "Gustav", new byte[] { 18, 214, 200, 155, 234, 204, 230, 254, 245, 9, 182, 48, 161, 36, 236, 160, 91, 150, 50, 125, 241, 92, 14, 5, 207, 87, 86, 251, 33, 221, 71, 38, 250, 87, 66, 121, 143, 219, 169, 213, 224, 122, 105, 25, 47, 245, 107, 36, 8, 197, 131, 186, 185, 114, 254, 153, 125, 165, 14, 181, 149, 115, 38, 238 }, new byte[] { 61, 230, 178, 108, 25, 97, 189, 30, 79, 26, 74, 81, 83, 93, 111, 136, 15, 137, 140, 254, 103, 107, 40, 244, 29, 100, 175, 178, 57, 111, 6, 91, 203, 200, 26, 236, 160, 25, 88, 24, 44, 70, 242, 111, 88, 114, 10, 176, 230, 133, 86, 103, 216, 126, 123, 15, 207, 34, 9, 162, 22, 196, 254, 8, 42, 29, 64, 3, 77, 79, 203, 225, 25, 13, 58, 214, 200, 144, 76, 138, 108, 210, 59, 85, 29, 61, 54, 29, 89, 142, 113, 31, 63, 141, 183, 1, 197, 202, 1, 123, 139, 21, 34, 18, 73, 111, 48, 178, 45, 66, 74, 253, 139, 36, 26, 124, 249, 72, 172, 77, 158, 139, 234, 112, 68, 228, 17, 105 }, "User", "Example Street 5", "hansi12", "3500" },
                    { 6L, null, "localhost", "yes", null, "Bobby", false, "Tables", new byte[] { 118, 99, 98, 182, 222, 183, 195, 234, 172, 163, 240, 224, 176, 21, 209, 197, 72, 136, 166, 213, 230, 157, 137, 165, 127, 138, 43, 104, 228, 105, 31, 127, 43, 228, 60, 11, 29, 25, 139, 27, 159, 231, 85, 164, 201, 21, 126, 181, 231, 176, 28, 24, 224, 73, 38, 181, 24, 76, 242, 102, 248, 248, 66, 186 }, new byte[] { 67, 170, 178, 176, 218, 188, 169, 213, 47, 203, 17, 86, 215, 62, 108, 152, 195, 54, 158, 139, 243, 110, 114, 217, 239, 60, 148, 205, 207, 32, 200, 33, 205, 21, 218, 86, 129, 66, 132, 194, 218, 184, 88, 126, 253, 58, 213, 6, 222, 32, 95, 84, 227, 56, 97, 158, 37, 149, 41, 216, 83, 25, 127, 78, 144, 97, 30, 152, 168, 82, 187, 122, 158, 12, 190, 197, 162, 227, 239, 78, 147, 77, 137, 65, 243, 168, 37, 230, 233, 120, 187, 139, 255, 56, 160, 178, 233, 224, 112, 101, 89, 11, 124, 107, 211, 97, 76, 9, 132, 51, 35, 64, 72, 182, 132, 62, 103, 1, 112, 17, 124, 20, 59, 139, 130, 183, 126, 119 }, "Admin", "443", "admin", "1337" }
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
                columns: new[] { "PlaceID", "OwnerID" },
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
                    { 2L, null, 1L, 5, "nice", new DateTime(2020, 5, 1, 18, 34, 29, 719, DateTimeKind.Local).AddTicks(3812), 1L },
                    { 3L, null, 1L, 4, "pretty good", new DateTime(2020, 5, 1, 18, 34, 29, 719, DateTimeKind.Local).AddTicks(3861), 2L },
                    { 1L, null, 2L, 2, "meh", new DateTime(2020, 5, 1, 18, 34, 29, 716, DateTimeKind.Local).AddTicks(9993), 2L }
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
