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
            Console.WriteLine("ClientId:");
            var clientId = Console.ReadLine();

            Console.WriteLine("ClientSecret:");
            var clientSecret = Console.ReadLine();

            Run(clientId, clientSecret).Wait();
        }

        private static async Task Run(string clientId, string clientSecret)
        {
            using (var bonVoyageContext = new BonVoyageContext())
            using (var foursquareContext = bonVoyageContext.CreateUserlessFoursquareContext(new UserlessAccessSettings(clientId, clientSecret)))
            {
                var categories = await foursquareContext.Categories.Get();
                PrintCategory(categories, 0);

                var category = categories.First().Categories.First();
                var venues = await category.SearchVenues("San Fransisco, CA", 1);
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
