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
                columns: new[] { "ID", "AvatarURI", "City", "Country", "Description", "FirstName", "IsVerified", "LastName", "PasswordHash", "PasswordSalt", "Role", "Street", "Username", "ZipCode" },
                values: new object[,]
                {
                    { 1L, null, "Krems an der Donau", "AT", null, "Peter", false, "Gustav", new byte[] { 179, 117, 237, 206, 121, 47, 67, 146, 178, 172, 202, 53, 4, 101, 39, 117, 233, 24, 36, 93, 40, 18, 141, 220, 113, 253, 106, 228, 104, 54, 225, 204, 203, 123, 163, 191, 85, 141, 6, 181, 217, 20, 68, 229, 136, 83, 121, 253, 83, 6, 70, 27, 149, 82, 23, 206, 166, 117, 42, 138, 49, 25, 81, 170 }, new byte[] { 100, 184, 3, 190, 76, 203, 60, 16, 53, 189, 252, 96, 164, 252, 63, 16, 37, 125, 96, 140, 86, 34, 79, 16, 152, 226, 209, 127, 228, 205, 158, 157, 205, 75, 91, 171, 55, 37, 138, 225, 151, 189, 131, 4, 242, 55, 233, 165, 80, 231, 232, 7, 99, 219, 227, 125, 100, 102, 170, 90, 106, 198, 182, 228, 182, 131, 231, 135, 178, 127, 250, 17, 190, 16, 190, 28, 114, 47, 222, 121, 228, 77, 249, 225, 9, 148, 45, 46, 13, 212, 207, 93, 167, 8, 183, 94, 193, 1, 50, 120, 43, 249, 240, 207, 37, 26, 238, 240, 14, 101, 53, 216, 189, 160, 235, 59, 42, 21, 87, 74, 181, 243, 137, 89, 16, 108, 183, 144 }, "User", "Example Street 1", "pete", "3500" },
                    { 2L, null, "Krems an der Donau", "AT", null, "John", false, "Gustav", new byte[] { 176, 162, 63, 74, 213, 67, 103, 136, 89, 136, 150, 187, 65, 209, 119, 201, 79, 49, 58, 33, 114, 79, 206, 143, 155, 89, 100, 153, 64, 81, 105, 51, 140, 218, 134, 81, 129, 69, 153, 228, 240, 109, 157, 99, 126, 146, 250, 212, 119, 115, 13, 154, 215, 79, 23, 14, 183, 225, 164, 32, 217, 156, 63, 148 }, new byte[] { 238, 173, 92, 99, 37, 141, 108, 167, 138, 86, 190, 183, 174, 23, 253, 28, 23, 139, 87, 173, 173, 168, 161, 64, 112, 22, 6, 195, 218, 225, 48, 6, 169, 65, 255, 169, 107, 179, 221, 23, 72, 90, 44, 208, 177, 193, 71, 52, 77, 78, 89, 82, 126, 218, 167, 235, 96, 67, 245, 0, 189, 217, 215, 88, 91, 55, 225, 106, 77, 195, 200, 160, 61, 28, 154, 162, 38, 25, 3, 214, 59, 19, 128, 35, 249, 154, 135, 235, 157, 134, 10, 77, 236, 51, 85, 108, 21, 143, 7, 168, 166, 6, 223, 238, 164, 174, 217, 54, 166, 158, 177, 9, 254, 135, 102, 44, 82, 59, 96, 8, 75, 184, 50, 126, 5, 197, 170, 52 }, "User", "Example Street 2", "MrJohn", "3500" },
                    { 3L, null, "Krems an der Donau", "AT", null, "Heinz", false, "Gustav", new byte[] { 174, 11, 144, 27, 245, 89, 77, 247, 226, 81, 5, 48, 230, 183, 193, 197, 246, 2, 235, 63, 217, 84, 177, 121, 246, 114, 207, 215, 184, 127, 104, 4, 210, 41, 78, 133, 57, 95, 83, 103, 219, 92, 6, 98, 203, 21, 165, 75, 92, 212, 240, 64, 105, 158, 223, 200, 228, 58, 57, 235, 100, 155, 28, 184 }, new byte[] { 110, 79, 195, 55, 51, 52, 253, 32, 59, 233, 0, 198, 41, 137, 20, 229, 187, 123, 244, 210, 178, 24, 218, 68, 177, 226, 73, 212, 173, 100, 127, 1, 151, 140, 114, 123, 236, 135, 182, 188, 188, 192, 130, 16, 97, 50, 197, 214, 192, 127, 48, 226, 237, 165, 53, 112, 159, 221, 166, 46, 161, 223, 154, 197, 147, 122, 241, 173, 48, 234, 144, 155, 142, 240, 239, 143, 178, 129, 221, 63, 201, 134, 226, 123, 215, 183, 240, 104, 219, 132, 152, 17, 116, 187, 208, 137, 78, 114, 197, 223, 41, 236, 10, 145, 48, 2, 1, 251, 129, 231, 47, 247, 35, 200, 175, 4, 110, 0, 136, 69, 61, 248, 74, 143, 45, 34, 8, 227 }, "User", "Example Street 3", "Ketchup", "3500" },
                    { 4L, null, "Krems an der Donau", "AT", null, "Olaf", false, "Gustav", new byte[] { 175, 165, 204, 40, 104, 50, 38, 142, 196, 55, 106, 177, 101, 228, 86, 118, 17, 116, 248, 178, 34, 90, 217, 167, 129, 208, 199, 194, 228, 125, 136, 93, 240, 49, 27, 57, 136, 111, 248, 238, 229, 98, 255, 119, 37, 1, 34, 191, 145, 101, 220, 135, 110, 55, 99, 31, 32, 225, 235, 44, 134, 133, 208, 231 }, new byte[] { 29, 243, 176, 78, 45, 81, 157, 57, 185, 31, 62, 197, 107, 165, 11, 77, 224, 186, 173, 2, 85, 176, 181, 124, 243, 97, 236, 138, 178, 237, 247, 40, 6, 180, 6, 150, 96, 102, 141, 112, 100, 72, 10, 12, 214, 166, 169, 190, 197, 81, 113, 141, 81, 141, 165, 119, 183, 226, 163, 102, 251, 222, 1, 9, 92, 3, 8, 215, 199, 9, 21, 87, 25, 57, 134, 8, 216, 197, 204, 85, 91, 44, 49, 218, 124, 57, 241, 198, 181, 228, 191, 149, 31, 242, 7, 207, 113, 160, 104, 204, 13, 183, 194, 168, 52, 248, 90, 58, 113, 66, 116, 1, 250, 224, 41, 222, 211, 85, 31, 53, 231, 183, 244, 163, 97, 76, 175, 227 }, "User", "Example Street 4", "Olaf", "3500" },
                    { 5L, null, "Krems an der Donau", "AT", null, "Hans", false, "Gustav", new byte[] { 196, 86, 175, 227, 33, 123, 169, 81, 100, 27, 166, 55, 102, 25, 38, 70, 70, 245, 138, 156, 2, 212, 164, 230, 237, 123, 99, 220, 105, 68, 43, 0, 156, 57, 35, 186, 12, 216, 17, 222, 182, 106, 116, 224, 251, 11, 237, 150, 72, 254, 58, 28, 12, 5, 203, 62, 8, 88, 15, 148, 37, 105, 141, 53 }, new byte[] { 252, 120, 116, 0, 79, 213, 218, 210, 163, 230, 86, 167, 221, 178, 146, 194, 151, 98, 94, 81, 68, 38, 210, 42, 16, 17, 5, 182, 179, 18, 44, 40, 98, 19, 189, 126, 71, 195, 148, 241, 135, 237, 110, 149, 187, 204, 163, 69, 39, 251, 196, 137, 167, 230, 160, 123, 47, 148, 169, 58, 85, 235, 67, 79, 47, 225, 218, 179, 145, 212, 111, 51, 33, 196, 237, 80, 96, 223, 36, 134, 14, 81, 76, 158, 182, 77, 182, 115, 96, 12, 118, 218, 214, 104, 118, 128, 71, 245, 203, 142, 156, 164, 178, 7, 230, 150, 133, 98, 224, 91, 239, 71, 184, 103, 232, 33, 61, 25, 0, 101, 110, 117, 103, 33, 85, 103, 29, 170 }, "User", "Example Street 5", "hansi12", "3500" },
                    { 6L, null, "localhost", "yes", null, "Bobby", false, "Tables", new byte[] { 100, 183, 124, 188, 110, 126, 253, 58, 66, 0, 83, 202, 230, 247, 155, 137, 141, 21, 225, 86, 135, 17, 240, 198, 21, 183, 129, 230, 71, 230, 84, 84, 127, 235, 189, 120, 57, 194, 128, 20, 200, 70, 13, 125, 236, 13, 59, 190, 108, 159, 242, 177, 229, 68, 214, 252, 47, 10, 222, 200, 148, 230, 70, 161 }, new byte[] { 50, 179, 74, 11, 192, 106, 53, 38, 71, 62, 163, 115, 117, 27, 12, 217, 135, 108, 120, 21, 147, 142, 117, 187, 235, 14, 211, 75, 67, 10, 229, 47, 150, 16, 127, 224, 141, 131, 102, 61, 200, 24, 178, 152, 106, 61, 94, 169, 22, 148, 72, 45, 73, 253, 2, 44, 71, 227, 131, 123, 58, 88, 40, 165, 210, 203, 45, 161, 58, 14, 223, 212, 84, 226, 251, 242, 193, 68, 70, 100, 208, 167, 182, 232, 149, 145, 98, 191, 235, 70, 186, 191, 150, 178, 195, 77, 17, 77, 221, 10, 85, 20, 126, 148, 92, 156, 210, 209, 65, 212, 5, 123, 116, 100, 215, 133, 135, 225, 207, 63, 205, 98, 183, 39, 150, 215, 164, 37 }, "Admin", "443", "admin", "1337" }
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
                    { 2L, null, 1L, 5, "nice", new DateTime(2020, 5, 1, 11, 18, 32, 333, DateTimeKind.Local).AddTicks(3104), 1L },
                    { 3L, null, 1L, 4, "pretty good", new DateTime(2020, 5, 1, 11, 18, 32, 333, DateTimeKind.Local).AddTicks(3161), 2L },
                    { 1L, null, 2L, 2, "meh", new DateTime(2020, 5, 1, 11, 18, 32, 330, DateTimeKind.Local).AddTicks(6829), 2L }
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
