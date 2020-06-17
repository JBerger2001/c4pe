﻿// <auto-generated />
using System;
using Feedback_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Feedback_API.Migrations
{
    [DbContext(typeof(FeedbackContext))]
    [Migration("20200501181857_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Feedback_API.Models.Domain.OpeningTime", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<TimeSpan>("Close")
                        .HasColumnType("time(6)");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Open")
                        .HasColumnType("time(6)");

                    b.Property<long>("PlaceID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("PlaceID");

                    b.ToTable("openingtimes");

                    b.HasData(
                        new
                        {
                            ID = 1L,
                            Close = new TimeSpan(0, 20, 0, 0, 0),
                            Day = 0,
                            Open = new TimeSpan(0, 8, 0, 0, 0),
                            PlaceID = 2L
                        },
                        new
                        {
                            ID = 2L,
                            Close = new TimeSpan(0, 19, 0, 0, 0),
                            Day = 0,
                            Open = new TimeSpan(0, 9, 0, 0, 0),
                            PlaceID = 3L
                        },
                        new
                        {
                            ID = 3L,
                            Close = new TimeSpan(0, 22, 0, 0, 0),
                            Day = 0,
                            Open = new TimeSpan(0, 10, 0, 0, 0),
                            PlaceID = 1L
                        });
                });

            modelBuilder.Entity("Feedback_API.Models.Domain.Place", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("City")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Country")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("PlaceTypeID")
                        .HasColumnType("bigint");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ZipCode")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.HasIndex("PlaceTypeID");

                    b.ToTable("places");

                    b.HasData(
                        new
                        {
                            ID = 1L,
                            City = "Krems an der Donau",
                            Country = "AT",
                            IsVerified = true,
                            Name = "Coffeehut",
                            PlaceTypeID = 1L,
                            Street = "City Street 1",
                            ZipCode = "3500"
                        },
                        new
                        {
                            ID = 2L,
                            City = "Krems an der Donau",
                            Country = "AT",
                            IsVerified = true,
                            Name = "Footly",
                            PlaceTypeID = 2L,
                            Street = "City Street 2",
                            ZipCode = "3500"
                        },
                        new
                        {
                            ID = 3L,
                            City = "Krems an der Donau",
                            Country = "AT",
                            IsVerified = true,
                            Name = "Gusto Generic",
                            PlaceTypeID = 3L,
                            Street = "City Street 3",
                            ZipCode = "3500"
                        });
                });

            modelBuilder.Entity("Feedback_API.Models.Domain.PlaceImage", b =>
                {
                    b.Property<long>("PlaceID")
                        .HasColumnType("bigint");

                    b.Property<long>("ID")
                        .HasColumnType("bigint");

                    b.Property<string>("URI")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("PlaceID", "ID");

                    b.ToTable("PlaceImages");
                });

            modelBuilder.Entity("Feedback_API.Models.Domain.PlaceOwner", b =>
                {
                    b.Property<long>("PlaceID")
                        .HasColumnType("bigint");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.HasKey("PlaceID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("placeowner");

                    b.HasData(
                        new
                        {
                            PlaceID = 1L,
                            UserID = 1L
                        },
                        new
                        {
                            PlaceID = 2L,
                            UserID = 2L
                        },
                        new
                        {
                            PlaceID = 3L,
                            UserID = 3L
                        });
                });

            modelBuilder.Entity("Feedback_API.Models.Domain.PlaceType", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.ToTable("placetypes");

                    b.HasData(
                        new
                        {
                            ID = 1L,
                            Name = "Café"
                        },
                        new
                        {
                            ID = 2L,
                            Name = "Shoe Store"
                        },
                        new
                        {
                            ID = 3L,
                            Name = "Fast Food Restaurant"
                        });
                });

            modelBuilder.Entity("Feedback_API.Models.Domain.Reaction", b =>
                {
                    b.Property<long>("ReviewID")
                        .HasColumnType("bigint");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsHelpful")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("ReviewID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("reactions");

                    b.HasData(
                        new
                        {
                            ReviewID = 2L,
                            UserID = 3L,
                            IsHelpful = false
                        },
                        new
                        {
                            ReviewID = 1L,
                            UserID = 2L,
                            IsHelpful = true
                        });
                });

            modelBuilder.Entity("Feedback_API.Models.Domain.Review", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("LastEdited")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("PlaceID")
                        .HasColumnType("bigint");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("PlaceID");

                    b.HasIndex("UserID");

                    b.ToTable("reviews");

                    b.HasData(
                        new
                        {
                            ID = 1L,
                            PlaceID = 2L,
                            Rating = 2,
                            Text = "meh",
                            Time = new DateTime(2020, 5, 1, 20, 18, 56, 910, DateTimeKind.Local).AddTicks(7658),
                            UserID = 2L
                        },
                        new
                        {
                            ID = 2L,
                            PlaceID = 1L,
                            Rating = 5,
                            Text = "nice",
                            Time = new DateTime(2020, 5, 1, 20, 18, 56, 913, DateTimeKind.Local).AddTicks(3589),
                            UserID = 1L
                        },
                        new
                        {
                            ID = 3L,
                            PlaceID = 1L,
                            Rating = 4,
                            Text = "pretty good",
                            Time = new DateTime(2020, 5, 1, 20, 18, 56, 913, DateTimeKind.Local).AddTicks(3642),
                            UserID = 2L
                        });
                });

            modelBuilder.Entity("Feedback_API.Models.Domain.User", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("AvatarURI")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("City")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Country")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("longblob");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("longblob");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Street")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ZipCode")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            ID = 1L,
                            City = "Krems an der Donau",
                            Country = "AT",
                            FirstName = "Peter",
                            IsVerified = false,
                            LastName = "Gustav",
                            PasswordHash = new byte[] { 37, 232, 0, 42, 221, 38, 163, 99, 60, 182, 218, 215, 93, 75, 250, 166, 23, 198, 219, 51, 109, 56, 241, 176, 4, 58, 248, 249, 59, 165, 184, 107, 38, 228, 124, 171, 20, 252, 225, 99, 158, 90, 131, 110, 83, 14, 243, 9, 110, 108, 114, 221, 45, 115, 234, 129, 29, 226, 48, 20, 216, 186, 248, 33 },
                            PasswordSalt = new byte[] { 223, 203, 243, 107, 252, 115, 69, 41, 82, 196, 209, 242, 28, 172, 199, 84, 251, 239, 148, 147, 7, 160, 142, 241, 51, 147, 177, 217, 154, 186, 132, 141, 145, 62, 85, 95, 19, 80, 2, 40, 156, 164, 144, 55, 190, 196, 79, 124, 38, 218, 124, 221, 42, 93, 129, 40, 42, 27, 31, 50, 171, 146, 244, 188, 42, 1, 80, 207, 78, 61, 226, 3, 215, 249, 8, 59, 55, 244, 30, 75, 207, 79, 59, 141, 199, 197, 224, 99, 229, 175, 134, 239, 243, 118, 139, 16, 3, 238, 140, 186, 170, 157, 164, 59, 116, 117, 202, 151, 60, 26, 238, 153, 161, 223, 34, 179, 216, 86, 199, 111, 171, 93, 163, 175, 172, 135, 32, 43 },
                            Role = "User",
                            Street = "Example Street 1",
                            Username = "pete",
                            ZipCode = "3500"
                        },
                        new
                        {
                            ID = 2L,
                            City = "Krems an der Donau",
                            Country = "AT",
                            FirstName = "John",
                            IsVerified = false,
                            LastName = "Gustav",
                            PasswordHash = new byte[] { 31, 57, 39, 155, 116, 239, 63, 203, 255, 228, 239, 129, 101, 137, 56, 52, 21, 146, 218, 134, 16, 174, 13, 235, 42, 16, 181, 95, 102, 255, 164, 208, 111, 192, 105, 122, 20, 120, 115, 45, 137, 200, 97, 193, 200, 28, 189, 254, 220, 195, 253, 82, 46, 115, 165, 215, 246, 44, 171, 141, 122, 113, 41, 136 },
                            PasswordSalt = new byte[] { 247, 147, 172, 128, 210, 39, 40, 58, 107, 157, 58, 13, 145, 213, 228, 4, 132, 128, 119, 48, 237, 236, 114, 146, 64, 157, 62, 84, 88, 91, 121, 0, 39, 133, 50, 142, 24, 225, 246, 143, 78, 56, 222, 165, 227, 222, 37, 201, 56, 54, 180, 248, 9, 228, 131, 114, 56, 78, 86, 250, 178, 13, 141, 52, 85, 135, 249, 147, 138, 242, 155, 46, 156, 52, 90, 13, 59, 194, 180, 123, 93, 164, 67, 114, 9, 29, 195, 205, 110, 120, 95, 123, 239, 41, 119, 206, 157, 179, 32, 138, 34, 57, 100, 57, 141, 5, 149, 156, 207, 56, 227, 79, 40, 125, 122, 147, 25, 116, 101, 130, 112, 153, 206, 72, 105, 221, 241, 137 },
                            Role = "User",
                            Street = "Example Street 2",
                            Username = "MrJohn",
                            ZipCode = "3500"
                        },
                        new
                        {
                            ID = 3L,
                            City = "Krems an der Donau",
                            Country = "AT",
                            FirstName = "Heinz",
                            IsVerified = false,
                            LastName = "Gustav",
                            PasswordHash = new byte[] { 176, 163, 59, 5, 234, 200, 81, 166, 76, 164, 223, 131, 24, 160, 191, 104, 223, 2, 211, 150, 249, 221, 27, 113, 76, 215, 202, 180, 72, 199, 195, 112, 201, 10, 45, 175, 22, 102, 134, 235, 76, 233, 30, 220, 253, 239, 179, 150, 15, 105, 129, 99, 158, 249, 115, 142, 55, 158, 129, 93, 104, 136, 77, 132 },
                            PasswordSalt = new byte[] { 185, 155, 107, 236, 7, 154, 210, 16, 138, 248, 187, 11, 152, 208, 134, 166, 254, 99, 178, 212, 82, 235, 65, 81, 236, 49, 124, 185, 12, 71, 225, 113, 117, 211, 67, 15, 152, 251, 16, 12, 16, 250, 156, 60, 170, 71, 174, 239, 248, 52, 241, 221, 6, 191, 52, 248, 114, 224, 21, 216, 138, 105, 28, 214, 230, 222, 193, 119, 104, 187, 109, 105, 241, 138, 119, 126, 102, 248, 25, 40, 217, 147, 230, 243, 207, 49, 40, 72, 131, 40, 219, 86, 61, 98, 134, 229, 55, 202, 234, 251, 231, 68, 58, 152, 67, 75, 197, 73, 60, 230, 194, 34, 212, 81, 60, 248, 244, 145, 7, 66, 180, 132, 185, 111, 5, 31, 241, 151 },
                            Role = "User",
                            Street = "Example Street 3",
                            Username = "Ketchup",
                            ZipCode = "3500"
                        },
                        new
                        {
                            ID = 4L,
                            City = "Krems an der Donau",
                            Country = "AT",
                            FirstName = "Olaf",
                            IsVerified = false,
                            LastName = "Gustav",
                            PasswordHash = new byte[] { 156, 95, 222, 186, 26, 144, 93, 123, 53, 44, 135, 49, 95, 244, 206, 230, 90, 213, 147, 5, 221, 83, 123, 232, 5, 190, 13, 148, 67, 131, 195, 75, 193, 235, 53, 167, 155, 176, 144, 131, 155, 69, 215, 47, 196, 18, 56, 151, 71, 163, 73, 96, 205, 107, 118, 201, 50, 254, 216, 128, 56, 127, 118, 199 },
                            PasswordSalt = new byte[] { 117, 151, 66, 97, 132, 208, 47, 223, 232, 59, 204, 178, 125, 110, 123, 144, 0, 158, 146, 176, 150, 190, 102, 216, 179, 78, 13, 53, 244, 204, 118, 67, 6, 39, 120, 189, 18, 23, 180, 6, 86, 222, 74, 207, 31, 217, 196, 132, 232, 135, 254, 167, 235, 175, 104, 164, 251, 96, 5, 30, 57, 54, 215, 238, 198, 85, 65, 181, 126, 149, 187, 119, 203, 203, 183, 155, 56, 150, 76, 59, 22, 205, 134, 131, 162, 203, 68, 80, 83, 203, 168, 47, 196, 220, 160, 229, 239, 121, 123, 70, 198, 6, 178, 53, 4, 218, 0, 199, 71, 241, 20, 246, 236, 126, 114, 18, 100, 44, 249, 17, 245, 103, 7, 197, 94, 90, 246, 175 },
                            Role = "User",
                            Street = "Example Street 4",
                            Username = "Olaf",
                            ZipCode = "3500"
                        },
                        new
                        {
                            ID = 5L,
                            City = "Krems an der Donau",
                            Country = "AT",
                            FirstName = "Hans",
                            IsVerified = false,
                            LastName = "Gustav",
                            PasswordHash = new byte[] { 49, 193, 115, 142, 186, 139, 252, 101, 234, 40, 198, 123, 72, 69, 227, 242, 204, 116, 199, 226, 110, 172, 113, 49, 98, 216, 102, 254, 93, 216, 239, 132, 34, 28, 238, 85, 127, 108, 221, 110, 120, 148, 80, 81, 139, 149, 209, 4, 248, 42, 232, 3, 241, 136, 132, 228, 182, 55, 90, 51, 92, 175, 251, 208 },
                            PasswordSalt = new byte[] { 213, 38, 196, 215, 66, 2, 93, 245, 44, 250, 127, 101, 197, 141, 43, 82, 10, 24, 22, 210, 120, 234, 244, 123, 102, 170, 128, 168, 137, 105, 140, 140, 240, 136, 30, 207, 243, 121, 25, 43, 113, 128, 213, 197, 175, 236, 195, 162, 188, 35, 119, 171, 42, 110, 205, 233, 69, 130, 136, 225, 19, 197, 169, 89, 150, 147, 137, 118, 190, 248, 111, 164, 221, 46, 37, 94, 48, 134, 241, 153, 161, 92, 189, 130, 14, 73, 108, 219, 225, 78, 144, 119, 29, 238, 243, 244, 66, 42, 158, 75, 165, 29, 44, 149, 202, 81, 230, 27, 223, 130, 176, 125, 91, 67, 3, 223, 198, 247, 64, 154, 82, 76, 155, 253, 142, 15, 188, 56 },
                            Role = "User",
                            Street = "Example Street 5",
                            Username = "hansi12",
                            ZipCode = "3500"
                        },
                        new
                        {
                            ID = 6L,
                            City = "localhost",
                            Country = "yes",
                            FirstName = "Bobby",
                            IsVerified = false,
                            LastName = "Tables",
                            PasswordHash = new byte[] { 48, 63, 14, 170, 255, 122, 46, 124, 202, 69, 153, 145, 205, 109, 162, 71, 52, 245, 117, 166, 211, 222, 19, 95, 200, 146, 195, 243, 150, 186, 82, 240, 186, 84, 104, 247, 162, 218, 72, 230, 95, 140, 11, 195, 247, 49, 112, 119, 133, 223, 52, 185, 236, 96, 245, 16, 224, 31, 17, 223, 161, 203, 143, 50 },
                            PasswordSalt = new byte[] { 239, 34, 76, 199, 21, 212, 219, 106, 111, 119, 202, 107, 74, 187, 128, 138, 154, 5, 252, 39, 251, 161, 160, 232, 87, 170, 79, 230, 77, 19, 176, 22, 183, 110, 11, 152, 215, 123, 183, 150, 2, 20, 146, 14, 118, 128, 210, 8, 154, 72, 67, 241, 47, 249, 149, 211, 24, 243, 210, 133, 234, 207, 122, 237, 120, 184, 13, 144, 218, 129, 221, 12, 167, 142, 213, 79, 93, 149, 81, 62, 76, 120, 96, 73, 135, 12, 11, 104, 153, 122, 203, 14, 212, 100, 84, 243, 215, 20, 247, 69, 119, 74, 21, 132, 104, 239, 204, 89, 131, 248, 184, 14, 210, 129, 64, 184, 213, 111, 241, 67, 108, 184, 21, 208, 13, 176, 129, 230 },
                            Role = "Admin",
                            Street = "443",
                            Username = "admin",
                            ZipCode = "1337"
                        });
                });

            modelBuilder.Entity("Feedback_API.Models.Domain.OpeningTime", b =>
                {
                    b.HasOne("Feedback_API.Models.Domain.Place", "Place")
                        .WithMany("OpeningTimes")
                        .HasForeignKey("PlaceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Feedback_API.Models.Domain.Place", b =>
                {
                    b.HasOne("Feedback_API.Models.Domain.PlaceType", "PlaceType")
                        .WithMany("Places")
                        .HasForeignKey("PlaceTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Feedback_API.Models.Domain.PlaceImage", b =>
                {
                    b.HasOne("Feedback_API.Models.Domain.Place", "Place")
                        .WithMany("Images")
                        .HasForeignKey("PlaceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Feedback_API.Models.Domain.PlaceOwner", b =>
                {
                    b.HasOne("Feedback_API.Models.Domain.Place", "Place")
                        .WithMany("Owners")
                        .HasForeignKey("PlaceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Feedback_API.Models.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Feedback_API.Models.Domain.Reaction", b =>
                {
                    b.HasOne("Feedback_API.Models.Domain.Review", "Review")
                        .WithMany("Reactions")
                        .HasForeignKey("ReviewID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Feedback_API.Models.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Feedback_API.Models.Domain.Review", b =>
                {
                    b.HasOne("Feedback_API.Models.Domain.Place", "Place")
                        .WithMany("Reviews")
                        .HasForeignKey("PlaceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Feedback_API.Models.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
