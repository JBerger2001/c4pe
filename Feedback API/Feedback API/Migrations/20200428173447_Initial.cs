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
                        name: "FK_placeowner_users_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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
                    { 1L, null, "Krems an der Donau", "AT", null, "Peter", false, "Gustav", new byte[] { 43, 123, 95, 19, 133, 41, 199, 80, 73, 107, 152, 124, 163, 45, 103, 243, 175, 99, 143, 188, 123, 27, 32, 168, 147, 80, 159, 141, 43, 185, 152, 60, 16, 116, 118, 158, 33, 222, 221, 85, 91, 112, 233, 171, 247, 91, 67, 220, 222, 129, 52, 28, 3, 174, 139, 203, 176, 244, 128, 131, 137, 79, 158, 97 }, new byte[] { 219, 103, 168, 175, 252, 98, 208, 84, 170, 187, 84, 42, 225, 2, 97, 163, 115, 77, 136, 19, 145, 211, 148, 255, 183, 123, 89, 230, 91, 116, 197, 131, 234, 101, 13, 44, 43, 94, 43, 82, 204, 0, 143, 24, 41, 79, 240, 95, 39, 191, 112, 125, 22, 210, 68, 4, 90, 75, 162, 84, 156, 164, 210, 93, 182, 132, 134, 130, 123, 111, 232, 163, 240, 242, 163, 231, 187, 72, 185, 6, 89, 2, 16, 255, 151, 93, 8, 234, 255, 184, 102, 159, 182, 10, 141, 203, 2, 1, 190, 121, 98, 157, 43, 201, 206, 21, 221, 151, 103, 102, 32, 80, 68, 45, 108, 214, 213, 193, 196, 221, 30, 11, 208, 233, 225, 175, 231, 88 }, "User", "Example Street 1", "pete", "3500" },
                    { 2L, null, "Krems an der Donau", "AT", null, "John", false, "Gustav", new byte[] { 50, 23, 176, 17, 247, 36, 132, 236, 210, 181, 227, 69, 250, 53, 1, 214, 114, 106, 42, 233, 148, 151, 92, 227, 194, 176, 57, 31, 166, 118, 54, 232, 150, 181, 144, 36, 251, 157, 140, 243, 107, 118, 47, 86, 181, 201, 128, 139, 174, 52, 206, 235, 88, 194, 73, 252, 154, 98, 72, 90, 37, 16, 154, 229 }, new byte[] { 35, 221, 121, 115, 79, 136, 91, 84, 51, 216, 55, 7, 52, 50, 74, 168, 149, 56, 194, 238, 106, 250, 142, 209, 201, 251, 13, 200, 61, 162, 8, 108, 184, 249, 103, 226, 51, 107, 130, 207, 80, 202, 146, 44, 197, 236, 216, 46, 55, 184, 89, 23, 106, 5, 178, 5, 116, 50, 153, 7, 250, 162, 175, 29, 59, 19, 4, 233, 90, 247, 239, 153, 33, 75, 29, 232, 217, 247, 159, 56, 230, 82, 224, 69, 68, 183, 226, 213, 0, 159, 218, 138, 4, 13, 48, 75, 89, 192, 99, 183, 236, 102, 249, 148, 88, 120, 175, 246, 103, 137, 0, 187, 174, 113, 45, 57, 183, 234, 185, 173, 164, 119, 161, 219, 2, 30, 142, 65 }, "User", "Example Street 2", "MrJohn", "3500" },
                    { 3L, null, "Krems an der Donau", "AT", null, "Heinz", false, "Gustav", new byte[] { 101, 198, 175, 77, 145, 239, 202, 124, 89, 192, 39, 49, 111, 202, 144, 65, 52, 100, 171, 149, 187, 86, 21, 98, 75, 113, 243, 127, 177, 156, 20, 69, 122, 65, 137, 247, 234, 116, 42, 105, 182, 140, 147, 232, 109, 1, 76, 93, 63, 238, 183, 49, 49, 229, 65, 183, 224, 148, 71, 193, 170, 17, 86, 82 }, new byte[] { 30, 208, 90, 170, 223, 233, 39, 146, 155, 135, 57, 139, 29, 182, 29, 182, 238, 38, 94, 65, 194, 185, 66, 22, 242, 173, 249, 35, 35, 46, 26, 65, 166, 97, 221, 127, 29, 21, 193, 61, 27, 107, 26, 199, 62, 183, 116, 180, 28, 244, 209, 229, 80, 206, 158, 163, 136, 244, 29, 133, 211, 28, 2, 9, 123, 107, 130, 51, 189, 192, 97, 221, 104, 145, 60, 77, 186, 92, 101, 80, 47, 171, 30, 244, 194, 76, 25, 76, 107, 86, 210, 17, 174, 92, 198, 42, 50, 235, 247, 71, 193, 198, 19, 208, 73, 154, 116, 42, 2, 11, 240, 171, 105, 199, 34, 36, 188, 213, 135, 108, 70, 169, 47, 242, 54, 196, 38, 87 }, "User", "Example Street 3", "Ketchup", "3500" },
                    { 4L, null, "Krems an der Donau", "AT", null, "Olaf", false, "Gustav", new byte[] { 242, 147, 181, 158, 42, 185, 244, 64, 134, 89, 247, 131, 141, 118, 172, 167, 131, 24, 130, 246, 174, 159, 16, 156, 118, 190, 238, 152, 184, 225, 199, 202, 243, 98, 41, 2, 13, 230, 182, 247, 199, 183, 108, 249, 94, 63, 37, 229, 164, 225, 133, 156, 0, 84, 222, 161, 99, 158, 231, 210, 238, 93, 89, 241 }, new byte[] { 137, 76, 109, 255, 116, 145, 219, 100, 175, 170, 184, 112, 209, 79, 51, 6, 141, 14, 228, 8, 244, 102, 224, 143, 132, 43, 223, 143, 243, 200, 42, 187, 149, 30, 126, 31, 137, 27, 12, 225, 64, 179, 171, 35, 184, 255, 97, 32, 222, 155, 72, 207, 208, 180, 158, 11, 77, 25, 20, 66, 213, 135, 128, 43, 249, 244, 97, 185, 98, 39, 185, 193, 49, 138, 156, 140, 86, 9, 37, 15, 0, 130, 104, 125, 24, 78, 14, 247, 70, 44, 52, 173, 195, 175, 214, 147, 195, 86, 127, 122, 107, 211, 126, 239, 76, 1, 6, 92, 58, 217, 214, 205, 71, 89, 94, 216, 178, 208, 245, 104, 229, 198, 208, 33, 133, 197, 186, 203 }, "User", "Example Street 4", "Olaf", "3500" },
                    { 5L, null, "Krems an der Donau", "AT", null, "Hans", false, "Gustav", new byte[] { 191, 247, 249, 19, 71, 6, 207, 144, 102, 17, 194, 98, 2, 244, 160, 17, 2, 107, 215, 210, 1, 170, 140, 131, 185, 27, 60, 37, 216, 61, 143, 196, 181, 92, 40, 204, 116, 228, 98, 37, 132, 245, 4, 89, 79, 33, 21, 229, 49, 113, 45, 199, 48, 29, 98, 19, 26, 107, 212, 93, 129, 36, 112, 214 }, new byte[] { 132, 35, 247, 2, 229, 38, 12, 146, 182, 193, 173, 224, 111, 18, 180, 64, 94, 178, 2, 104, 86, 104, 235, 114, 227, 196, 224, 53, 255, 97, 113, 234, 10, 91, 82, 181, 98, 27, 175, 107, 177, 101, 250, 194, 87, 162, 34, 162, 221, 244, 235, 127, 92, 164, 252, 205, 234, 52, 23, 102, 136, 132, 172, 204, 239, 251, 77, 15, 174, 55, 111, 125, 236, 30, 47, 51, 164, 145, 249, 123, 164, 10, 223, 11, 217, 158, 32, 239, 46, 116, 61, 168, 225, 102, 138, 31, 150, 25, 67, 164, 91, 205, 5, 229, 42, 45, 210, 95, 161, 191, 185, 172, 79, 251, 192, 64, 106, 45, 13, 227, 116, 186, 156, 61, 172, 177, 200, 15 }, "User", "Example Street 5", "hansi12", "3500" },
                    { 6L, null, "localhost", "yes", null, "Bobby", false, "Tables", new byte[] { 188, 139, 238, 79, 154, 81, 237, 46, 180, 175, 89, 173, 186, 248, 211, 233, 128, 22, 253, 30, 90, 248, 132, 214, 226, 109, 240, 84, 25, 210, 204, 120, 75, 201, 24, 189, 225, 160, 128, 110, 74, 244, 198, 51, 69, 194, 169, 173, 230, 161, 248, 115, 97, 230, 217, 189, 218, 62, 219, 82, 1, 253, 5, 64 }, new byte[] { 63, 225, 73, 42, 34, 152, 29, 26, 104, 152, 195, 78, 187, 22, 130, 63, 6, 34, 127, 68, 216, 89, 134, 86, 177, 203, 170, 111, 24, 72, 82, 173, 94, 199, 165, 157, 1, 107, 62, 159, 254, 44, 37, 244, 12, 203, 228, 201, 100, 211, 131, 5, 199, 152, 64, 20, 180, 176, 171, 46, 183, 5, 145, 74, 201, 252, 219, 186, 247, 247, 48, 20, 33, 177, 10, 222, 81, 236, 238, 46, 139, 136, 219, 51, 214, 216, 115, 106, 181, 149, 254, 238, 168, 60, 65, 144, 232, 78, 42, 216, 85, 233, 130, 112, 121, 254, 223, 134, 6, 185, 199, 15, 49, 69, 243, 164, 107, 151, 44, 85, 64, 19, 191, 25, 116, 9, 109, 63 }, "Admin", "443", "admin", "1337" }
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
                columns: new[] { "ID", "PlaceID", "Rating", "Text", "Time", "UserID" },
                values: new object[,]
                {
                    { 2L, 1L, 5, "nice", new DateTime(2020, 4, 28, 19, 34, 46, 800, DateTimeKind.Local).AddTicks(1457), 1L },
                    { 3L, 1L, 4, "pretty good", new DateTime(2020, 4, 28, 19, 34, 46, 800, DateTimeKind.Local).AddTicks(1502), 2L },
                    { 1L, 2L, 2, "meh", new DateTime(2020, 4, 28, 19, 34, 46, 797, DateTimeKind.Local).AddTicks(6533), 2L }
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
                name: "IX_placeowner_OwnerID",
                table: "placeowner",
                column: "OwnerID");

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
