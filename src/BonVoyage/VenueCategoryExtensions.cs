using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BonVoyage.Clients;
using BonVoyage.Models;

namespace BonVoyage
{
    public static class VenueCategoryExtensions
    {
        public static Task<IReadOnlyCollection<CompactVenue>> SearchVenues(this VenueCategory category, 
            string placeName)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));
            
            var client = new VenuesClient(category.HttpClient);
            return client.Search(placeName, category.Id);
        }

        public static Task<IReadOnlyCollection<CompactVenue>> SearchVenues(this VenueCategory category,
            string placeName, int limit)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));

            var client = new VenuesClient(category.HttpClient);
            return client.Search(placeName, category.Id, limit);
        }
    }
}