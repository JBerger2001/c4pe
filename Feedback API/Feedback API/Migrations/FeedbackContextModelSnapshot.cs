﻿// <auto-generated />
using System;
using Feedback_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Feedback_API.Migrations
{
    [DbContext(typeof(FeedbackContext))]
    partial class FeedbackContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("Feedback_API.Models.Domain.PlaceOwner", b =>
                {
                    b.Property<long>("PlaceID")
                        .HasColumnType("bigint");

                    b.Property<long>("OwnerID")
                        .HasColumnType("bigint");

                    b.HasKey("PlaceID", "OwnerID");

                    b.ToTable("placeowner");

                    b.HasData(
                        new
                        {
                            PlaceID = 1L,
                            OwnerID = 1L
                        },
                        new
                        {
                            PlaceID = 2L,
                            OwnerID = 2L
                        },
                        new
                        {
                            PlaceID = 3L,
                            OwnerID = 3L
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
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool>("IsHelpful")
                        .HasColumnType("tinyint(1)");

                    b.Property<long>("ReviewID")
                        .HasColumnType("bigint");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("ReviewID");

                    b.HasIndex("UserID");

                    b.ToTable("reactions");

                    b.HasData(
                        new
                        {
                            ID = 1L,
                            IsHelpful = false,
                            ReviewID = 2L,
                            UserID = 3L
                        },
                        new
                        {
                            ID = 2L,
                            IsHelpful = true,
                            ReviewID = 1L,
                            UserID = 2L
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
                            Time = new DateTime(2020, 5, 1, 11, 18, 32, 330, DateTimeKind.Local).AddTicks(6829),
                            UserID = 2L
                        },
                        new
                        {
                            ID = 2L,
                            PlaceID = 1L,
                            Rating = 5,
                            Text = "nice",
                            Time = new DateTime(2020, 5, 1, 11, 18, 32, 333, DateTimeKind.Local).AddTicks(3104),
                            UserID = 1L
                        },
                        new
                        {
                            ID = 3L,
                            PlaceID = 1L,
                            Rating = 4,
                            Text = "pretty good",
                            Time = new DateTime(2020, 5, 1, 11, 18, 32, 333, DateTimeKind.Local).AddTicks(3161),
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
                            PasswordHash = new byte[] { 179, 117, 237, 206, 121, 47, 67, 146, 178, 172, 202, 53, 4, 101, 39, 117, 233, 24, 36, 93, 40, 18, 141, 220, 113, 253, 106, 228, 104, 54, 225, 204, 203, 123, 163, 191, 85, 141, 6, 181, 217, 20, 68, 229, 136, 83, 121, 253, 83, 6, 70, 27, 149, 82, 23, 206, 166, 117, 42, 138, 49, 25, 81, 170 },
                            PasswordSalt = new byte[] { 100, 184, 3, 190, 76, 203, 60, 16, 53, 189, 252, 96, 164, 252, 63, 16, 37, 125, 96, 140, 86, 34, 79, 16, 152, 226, 209, 127, 228, 205, 158, 157, 205, 75, 91, 171, 55, 37, 138, 225, 151, 189, 131, 4, 242, 55, 233, 165, 80, 231, 232, 7, 99, 219, 227, 125, 100, 102, 170, 90, 106, 198, 182, 228, 182, 131, 231, 135, 178, 127, 250, 17, 190, 16, 190, 28, 114, 47, 222, 121, 228, 77, 249, 225, 9, 148, 45, 46, 13, 212, 207, 93, 167, 8, 183, 94, 193, 1, 50, 120, 43, 249, 240, 207, 37, 26, 238, 240, 14, 101, 53, 216, 189, 160, 235, 59, 42, 21, 87, 74, 181, 243, 137, 89, 16, 108, 183, 144 },
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
                            PasswordHash = new byte[] { 176, 162, 63, 74, 213, 67, 103, 136, 89, 136, 150, 187, 65, 209, 119, 201, 79, 49, 58, 33, 114, 79, 206, 143, 155, 89, 100, 153, 64, 81, 105, 51, 140, 218, 134, 81, 129, 69, 153, 228, 240, 109, 157, 99, 126, 146, 250, 212, 119, 115, 13, 154, 215, 79, 23, 14, 183, 225, 164, 32, 217, 156, 63, 148 },
                            PasswordSalt = new byte[] { 238, 173, 92, 99, 37, 141, 108, 167, 138, 86, 190, 183, 174, 23, 253, 28, 23, 139, 87, 173, 173, 168, 161, 64, 112, 22, 6, 195, 218, 225, 48, 6, 169, 65, 255, 169, 107, 179, 221, 23, 72, 90, 44, 208, 177, 193, 71, 52, 77, 78, 89, 82, 126, 218, 167, 235, 96, 67, 245, 0, 189, 217, 215, 88, 91, 55, 225, 106, 77, 195, 200, 160, 61, 28, 154, 162, 38, 25, 3, 214, 59, 19, 128, 35, 249, 154, 135, 235, 157, 134, 10, 77, 236, 51, 85, 108, 21, 143, 7, 168, 166, 6, 223, 238, 164, 174, 217, 54, 166, 158, 177, 9, 254, 135, 102, 44, 82, 59, 96, 8, 75, 184, 50, 126, 5, 197, 170, 52 },
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
                            PasswordHash = new byte[] { 174, 11, 144, 27, 245, 89, 77, 247, 226, 81, 5, 48, 230, 183, 193, 197, 246, 2, 235, 63, 217, 84, 177, 121, 246, 114, 207, 215, 184, 127, 104, 4, 210, 41, 78, 133, 57, 95, 83, 103, 219, 92, 6, 98, 203, 21, 165, 75, 92, 212, 240, 64, 105, 158, 223, 200, 228, 58, 57, 235, 100, 155, 28, 184 },
                            PasswordSalt = new byte[] { 110, 79, 195, 55, 51, 52, 253, 32, 59, 233, 0, 198, 41, 137, 20, 229, 187, 123, 244, 210, 178, 24, 218, 68, 177, 226, 73, 212, 173, 100, 127, 1, 151, 140, 114, 123, 236, 135, 182, 188, 188, 192, 130, 16, 97, 50, 197, 214, 192, 127, 48, 226, 237, 165, 53, 112, 159, 221, 166, 46, 161, 223, 154, 197, 147, 122, 241, 173, 48, 234, 144, 155, 142, 240, 239, 143, 178, 129, 221, 63, 201, 134, 226, 123, 215, 183, 240, 104, 219, 132, 152, 17, 116, 187, 208, 137, 78, 114, 197, 223, 41, 236, 10, 145, 48, 2, 1, 251, 129, 231, 47, 247, 35, 200, 175, 4, 110, 0, 136, 69, 61, 248, 74, 143, 45, 34, 8, 227 },
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
                            PasswordHash = new byte[] { 175, 165, 204, 40, 104, 50, 38, 142, 196, 55, 106, 177, 101, 228, 86, 118, 17, 116, 248, 178, 34, 90, 217, 167, 129, 208, 199, 194, 228, 125, 136, 93, 240, 49, 27, 57, 136, 111, 248, 238, 229, 98, 255, 119, 37, 1, 34, 191, 145, 101, 220, 135, 110, 55, 99, 31, 32, 225, 235, 44, 134, 133, 208, 231 },
                            PasswordSalt = new byte[] { 29, 243, 176, 78, 45, 81, 157, 57, 185, 31, 62, 197, 107, 165, 11, 77, 224, 186, 173, 2, 85, 176, 181, 124, 243, 97, 236, 138, 178, 237, 247, 40, 6, 180, 6, 150, 96, 102, 141, 112, 100, 72, 10, 12, 214, 166, 169, 190, 197, 81, 113, 141, 81, 141, 165, 119, 183, 226, 163, 102, 251, 222, 1, 9, 92, 3, 8, 215, 199, 9, 21, 87, 25, 57, 134, 8, 216, 197, 204, 85, 91, 44, 49, 218, 124, 57, 241, 198, 181, 228, 191, 149, 31, 242, 7, 207, 113, 160, 104, 204, 13, 183, 194, 168, 52, 248, 90, 58, 113, 66, 116, 1, 250, 224, 41, 222, 211, 85, 31, 53, 231, 183, 244, 163, 97, 76, 175, 227 },
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
                            PasswordHash = new byte[] { 196, 86, 175, 227, 33, 123, 169, 81, 100, 27, 166, 55, 102, 25, 38, 70, 70, 245, 138, 156, 2, 212, 164, 230, 237, 123, 99, 220, 105, 68, 43, 0, 156, 57, 35, 186, 12, 216, 17, 222, 182, 106, 116, 224, 251, 11, 237, 150, 72, 254, 58, 28, 12, 5, 203, 62, 8, 88, 15, 148, 37, 105, 141, 53 },
                            PasswordSalt = new byte[] { 252, 120, 116, 0, 79, 213, 218, 210, 163, 230, 86, 167, 221, 178, 146, 194, 151, 98, 94, 81, 68, 38, 210, 42, 16, 17, 5, 182, 179, 18, 44, 40, 98, 19, 189, 126, 71, 195, 148, 241, 135, 237, 110, 149, 187, 204, 163, 69, 39, 251, 196, 137, 167, 230, 160, 123, 47, 148, 169, 58, 85, 235, 67, 79, 47, 225, 218, 179, 145, 212, 111, 51, 33, 196, 237, 80, 96, 223, 36, 134, 14, 81, 76, 158, 182, 77, 182, 115, 96, 12, 118, 218, 214, 104, 118, 128, 71, 245, 203, 142, 156, 164, 178, 7, 230, 150, 133, 98, 224, 91, 239, 71, 184, 103, 232, 33, 61, 25, 0, 101, 110, 117, 103, 33, 85, 103, 29, 170 },
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
                            PasswordHash = new byte[] { 100, 183, 124, 188, 110, 126, 253, 58, 66, 0, 83, 202, 230, 247, 155, 137, 141, 21, 225, 86, 135, 17, 240, 198, 21, 183, 129, 230, 71, 230, 84, 84, 127, 235, 189, 120, 57, 194, 128, 20, 200, 70, 13, 125, 236, 13, 59, 190, 108, 159, 242, 177, 229, 68, 214, 252, 47, 10, 222, 200, 148, 230, 70, 161 },
                            PasswordSalt = new byte[] { 50, 179, 74, 11, 192, 106, 53, 38, 71, 62, 163, 115, 117, 27, 12, 217, 135, 108, 120, 21, 147, 142, 117, 187, 235, 14, 211, 75, 67, 10, 229, 47, 150, 16, 127, 224, 141, 131, 102, 61, 200, 24, 178, 152, 106, 61, 94, 169, 22, 148, 72, 45, 73, 253, 2, 44, 71, 227, 131, 123, 58, 88, 40, 165, 210, 203, 45, 161, 58, 14, 223, 212, 84, 226, 251, 242, 193, 68, 70, 100, 208, 167, 182, 232, 149, 145, 98, 191, 235, 70, 186, 191, 150, 178, 195, 77, 17, 77, 221, 10, 85, 20, 126, 148, 92, 156, 210, 209, 65, 212, 5, 123, 116, 100, 215, 133, 135, 225, 207, 63, 205, 98, 183, 39, 150, 215, 164, 37 },
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

            modelBuilder.Entity("Feedback_API.Models.Domain.PlaceOwner", b =>
                {
                    b.HasOne("Feedback_API.Models.Domain.Place", "Place")
                        .WithMany("PlaceOwners")
                        .HasForeignKey("PlaceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Feedback_API.Models.Domain.Reaction", b =>
                {
                    b.HasOne("Feedback_API.Models.Domain.Review", "Review")
                        .WithMany()
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
