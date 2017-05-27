using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

            using (var bonVoyageContext = new BonVoyageContext())
            using (var foursquareContext = bonVoyageContext.CreateUserlessFoursquareContext(new UserlessAccessSettings(clientId, clientSecret)))
            {
                var categories = foursquareContext.Categories.Get().Result;
                PrintCategory(categories, 0);

                var categoryId = categories.First().Id;
                var venues = foursquareContext.Venues.Search("San Fransisco, CA", categoryId, 1).Result;

                var venueId = venues.First().Id;
                var photos = foursquareContext.Photos.GetVenuePhotos(venueId, 50, 0).Result;
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
