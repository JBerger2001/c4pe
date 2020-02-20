using Feedback_API.Models.Domain;
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
        public DbSet<Review> Reviews { get; set; }
        public DbSet<OpeningTime> OpeningTimes { get; set; }
        public DbSet<Reaction> Reactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(new User[]
            {
                new User { ID = 1, Address = "3500 Krems an der Donau", FirstName = "Peter", LastName = "Gustav", Username = "pete", PasswordHash = new byte[]{0}, PasswordSalt = new byte[]{0}},
                new User { ID = 2, Address = "3500 Krems an der Donau", FirstName = "John", LastName = "Gustav", Username = "MrJohn", PasswordHash = new byte[]{0}, PasswordSalt = new byte[]{0}},
                new User { ID = 3, Address = "3500 Krems an der Donau", FirstName = "Heinz", LastName = "Gustav", Username = "Ketchup", PasswordHash = new byte[]{0}, PasswordSalt = new byte[]{0}},
                new User { ID = 4, Address = "3500 Krems an der Donau", FirstName = "Olaf", LastName = "Gustav", Username = "Olaf", PasswordHash = new byte[]{0}, PasswordSalt = new byte[]{0}},
                new User { ID = 5, Address = "3500 Krems an der Donau", FirstName = "Hans", LastName = "Gustav", Username = "hansi12", PasswordHash = new byte[]{0}, PasswordSalt = new byte[]{0}}
            });

            modelBuilder.Entity<PlaceType>().HasData(new PlaceType[]
            {
                new PlaceType { ID = 1, Name = "Café" },
                new PlaceType { ID = 2, Name = "Shoe Store" },
                new PlaceType { ID = 3, Name = "Fast Food Restaurant" },
            });

            modelBuilder.Entity<Place>().HasData(new Place[]
            {
                new Place { ID = 1, Name = "Coffeehut", Address = "3500 Krems an der Donau", PlaceTypeID = 1, IsVerified = true },
                new Place { ID = 2, Name = "Footly", Address = "3500 Krems an der Donau", PlaceTypeID = 2, IsVerified = true },
                new Place { ID = 3, Name = "Gusto Generic", Address = "3500 Krems an der Donau", PlaceTypeID = 3, IsVerified = true },
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
            });

            modelBuilder.Entity<Reaction>().HasData(new Reaction[]
            {
                new Reaction { ID = 1, ReviewID = 2, UserID = 3, IsHelpful = false },
                new Reaction { ID = 2, ReviewID = 1, UserID = 2, IsHelpful = true },
            });
        }
    }
}
