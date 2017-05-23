using System;
using BonVoyage;

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

                foreach (var venueCategory in categories)
                {
                    Console.WriteLine("{0}: {1}", venueCategory.Id, venueCategory.Name);
                }
            }
        }
    }
}
