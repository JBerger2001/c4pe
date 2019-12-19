using Feedback_API.Models;
using Feedback_API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(FeedbackContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any() || context.Places.Any())
            {
                return;
            }

            var users = new User[]
            {
                new User { ID = 0, Address = "3500 Krems an der Donau", FirstName = "Hans", LastName = "Gustav", Username = "hansi12", Password = "..."},
                new User { ID = 1, Address = "3500 Krems an der Donau", FirstName = "Peter", LastName = "Gustav", Username = "pete", Password = "..."},
                new User { ID = 2, Address = "3500 Krems an der Donau", FirstName = "John", LastName = "Gustav", Username = "MrJohn", Password = "..."},
                new User { ID = 3, Address = "3500 Krems an der Donau", FirstName = "Heinz", LastName = "Gustav", Username = "Ketchup", Password = "..."},
                new User { ID = 4, Address = "3500 Krems an der Donau", FirstName = "Olaf", LastName = "Gustav", Username = "Olaf", Password = "..."}
            };
            context.Users.AddRange(users);

            var placeTypes = new PlaceType[]
            {
                new PlaceType { ID = 0, Name = "Fast Food Restaurant" },
                new PlaceType { ID = 1, Name = "Café" },
                new PlaceType { ID = 2, Name = "Shoe Store" },
            };
            context.PlaceTypes.AddRange(placeTypes);

            var places = new Place[]
            {
                new Place { ID = 0, Name = "Gusto Generic", Address = "3500 Krems an der Donau", PlaceTypeID = 1, IsVerified = true },
                new Place { ID = 1, Name = "Coffeehut", Address = "3500 Krems an der Donau", PlaceTypeID = 2, IsVerified = true },
                new Place { ID = 2, Name = "Footly", Address = "3500 Krems an der Donau", PlaceTypeID = 3, IsVerified = true },
            };
            context.Places.AddRange(places);

            var openingTimes = new OpeningTime[]
            {
                new OpeningTime { ID = 0, PlaceID = 1, Day = 0, Open = new TimeSpan(10, 0, 0), Close = new TimeSpan(22, 0, 0) },
                new OpeningTime { ID = 1, PlaceID = 2, Day = 0, Open = new TimeSpan(8, 0, 0), Close = new TimeSpan(20, 0, 0) },
                new OpeningTime { ID = 2, PlaceID = 3, Day = 0, Open = new TimeSpan(9, 0, 0), Close = new TimeSpan(19, 0, 0) },
            };
            context.OpeningTimes.AddRange(openingTimes);

            var reviews = new Review[]
            {
                new Review { ID = 0, PlaceID = 1, UserID = 1, Rating = 5, Time = DateTime.Now, Text = "nice" },
                new Review { ID = 1, PlaceID = 2, UserID = 2, Rating = 2, Time = DateTime.Now, Text = "meh" }
            };
            context.Reviews.AddRange(reviews);

            var reactions = new Reaction[]
            {
                new Reaction { ID = 0, ReviewID = 1, UserID = 2, IsHelpful = true },
                new Reaction { ID = 1, ReviewID = 2, UserID = 3, IsHelpful = false },
            };
            context.Reactions.AddRange(reactions);

            context.SaveChanges();
        }
    }
}
