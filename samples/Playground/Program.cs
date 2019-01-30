using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BonVoyage;
using BonVoyage.Models;

namespace Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Userless?");
            var userless = bool.Parse(Console.ReadLine());

            if (userless) 
            {
                Console.WriteLine("ClientId:");
                var clientId = Console.ReadLine();

                Console.WriteLine("ClientSecret:");
                var clientSecret = Console.ReadLine();

                Run(clientId, clientSecret).Wait();
            }
            else 
            {
                Console.WriteLine("Access Token:");
                var accessToken = Console.ReadLine();

                Run(accessToken).Wait();
            }
        }

        private static async Task Run(string accessToken)
        {
            using (var bonVoyageContext = new BonVoyageContext())
            using (var foursquareContext = bonVoyageContext.CreateFoursquareContext(accessToken))
            {
                var visitedVenues = await foursquareContext.Users.GetVisitedVenues();
                foreach (var visitedVenue in visitedVenues)
                {
                    Console.WriteLine("{0}: ({1}, {2})", visitedVenue.Name, 
                        visitedVenue.Location.City, 
                        visitedVenue.Location.Country);
                }
            }
        }

        private static async Task Run(string clientId, string clientSecret)
        {
            using (var bonVoyageContext = new BonVoyageContext())
            using (var foursquareContext = bonVoyageContext.CreateUserlessFoursquareContext(new UserlessAccessSettings(clientId, clientSecret)))
            {
                var categories = await foursquareContext.Categories.Get();
                PrintCategory(categories, 0);

                var category = categories.First().Categories.First();
                //var venues = await category.SearchVenues("San Fransisco, CA", 1);
                var venues = await category.SearchVenues(new Location() { Lat = 53.551085, Lng = 9.993682 }, 50);
                var venue = venues.First();
                Console.WriteLine("{0}: {1} (for {2})", venue.Id, venue.Name, category.Name);

                var photos = await venue.GetPhotos();
                foreach (var photo in photos)
                {
                    Console.WriteLine("{0}: {1}", photo.GetUrl(), photo.Visibility);
                }
            }
        }

        private static void PrintCategory(IReadOnlyCollection<VenueCategory> categories, int indentCount)
        {
            if (categories.Any())
            {
                foreach (var venueCategory in categories)
                {
                    for (int i = 0; i < indentCount; i++)
                    {
                        Console.Write("\t");
                    }

                    Console.Write("{0}: {1}", venueCategory.Id, venueCategory.Name);
                    Console.Write(Environment.NewLine);

                    PrintCategory(new ReadOnlyCollection<VenueCategory>(venueCategory.Categories.ToList()), indentCount + 1);
                }
            }
        }
    }
}
