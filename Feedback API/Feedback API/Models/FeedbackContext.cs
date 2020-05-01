using Feedback_API.Controllers;
using Feedback_API.Models.Domain;
using Feedback_API.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Models
{
    public class FeedbackContext : DbContext
    {
        public FeedbackContext(DbContextOptions<FeedbackContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<PlaceType> PlaceTypes { get; set; }
        public DbSet<PlaceOwner> PlaceOwners { get; set; }
        public DbSet<PlaceImage> PlaceImages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<OpeningTime> OpeningTimes { get; set; }
        public DbSet<Reaction> Reactions { get; set; }

        private void SetPassword(User user, string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlaceOwner>()
                .HasKey(po => new { po.PlaceID, po.OwnerID });

            modelBuilder.Entity<Reaction>()
                .HasKey(r => new { r.ReviewID, r.UserID });

            modelBuilder.Entity<PlaceImage>()
                .HasKey(pi => new { pi.PlaceID, pi.ID });

            modelBuilder.Entity<Review>().Property("LastEdited").IsRequired(false);

            // default users
            List<User> users = new List<User>
            {
                new User { ID = 1, Role = Role.User, ZipCode = "3500", City = "Krems an der Donau", Street = "Example Street 1", Country = "AT", FirstName = "Peter", LastName = "Gustav", Username = "pete" },
                new User { ID = 2, Role = Role.User, ZipCode = "3500", City = "Krems an der Donau", Street = "Example Street 2", Country = "AT", FirstName = "John", LastName = "Gustav", Username = "MrJohn" },
                new User { ID = 3, Role = Role.User, ZipCode = "3500", City = "Krems an der Donau", Street = "Example Street 3", Country = "AT", FirstName = "Heinz", LastName = "Gustav", Username = "Ketchup" },
                new User { ID = 4, Role = Role.User, ZipCode = "3500", City = "Krems an der Donau", Street = "Example Street 4", Country = "AT", FirstName = "Olaf", LastName = "Gustav", Username = "Olaf" },
                new User { ID = 5, Role = Role.User, ZipCode = "3500", City = "Krems an der Donau", Street = "Example Street 5", Country = "AT", FirstName = "Hans", LastName = "Gustav", Username = "hansi12" },
                new User { ID = 6, Role = Role.Admin, ZipCode = "1337", City = "localhost", Street = "443", Country = "yes", FirstName = "Bobby", LastName = "Tables", Username = "admin" }
            };
            users.ForEach(u =>
            {
                var password = (u.Role == Role.User) ? "user" : "admin";
                SetPassword(u, password);
            });
            modelBuilder.Entity<User>().HasData(users);


            modelBuilder.Entity<PlaceType>().HasData(new PlaceType[]
            {
                new PlaceType { ID = 1, Name = "Café" },
                new PlaceType { ID = 2, Name = "Shoe Store" },
                new PlaceType { ID = 3, Name = "Fast Food Restaurant" },
            });

            modelBuilder.Entity<Place>().HasData(new Place[]
            {
                new Place { ID = 1, Name = "Coffeehut", ZipCode = "3500", City = "Krems an der Donau", Street = "City Street 1", Country = "AT", PlaceTypeID = 1, IsVerified = true },
                new Place { ID = 2, Name = "Footly", ZipCode = "3500", City = "Krems an der Donau", Street = "City Street 2", Country = "AT", PlaceTypeID = 2, IsVerified = true },
                new Place { ID = 3, Name = "Gusto Generic", ZipCode = "3500", City = "Krems an der Donau", Street = "City Street 3", Country = "AT", PlaceTypeID = 3, IsVerified = true },
            });

            modelBuilder.Entity<PlaceOwner>().HasData(new PlaceOwner[]
            {
                new PlaceOwner { OwnerID = 1, PlaceID = 1 },
                new PlaceOwner { OwnerID = 2, PlaceID = 2 },
                new PlaceOwner { OwnerID = 3, PlaceID = 3 },
            });

            modelBuilder.Entity<OpeningTime>().HasData(new OpeningTime[]
            {
                new OpeningTime { ID = 1, PlaceID = 2, Day = 0, Open = new TimeSpan(8, 0, 0), Close = new TimeSpan(20, 0, 0) },
                new OpeningTime { ID = 2, PlaceID = 3, Day = 0, Open = new TimeSpan(9, 0, 0), Close = new TimeSpan(19, 0, 0) },
                new OpeningTime { ID = 3, PlaceID = 1, Day = 0, Open = new TimeSpan(10, 0, 0), Close = new TimeSpan(22, 0, 0) },
            });

            modelBuilder.Entity<Review>().HasData(new Review[]
            {
                new Review { ID = 1, PlaceID = 2, UserID = 2, Rating = 2, Time = DateTime.Now, Text = "meh" },
                new Review { ID = 2, PlaceID = 1, UserID = 1, Rating = 5, Time = DateTime.Now, Text = "nice" },
                new Review { ID = 3, PlaceID = 1, UserID = 2, Rating = 4, Time = DateTime.Now, Text = "pretty good" },
            });

            modelBuilder.Entity<Reaction>().HasData(new Reaction[]
            {
                new Reaction { ReviewID = 2, UserID = 3, IsHelpful = false },
                new Reaction { ReviewID = 1, UserID = 2, IsHelpful = true },
            });
        }
    }
}
