using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BonVoyage.Clients;
using BonVoyage.Models;

namespace BonVoyage
{
    public static class CompactVenueExtensions
    {
        /// <summary>
        /// Returns first 50 photos for a venue.
        /// </summary>
        /// <param name="venue">The venue you want photos for.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static Task<IReadOnlyCollection<VenuePhoto>> GetPhotos(this CompactVenue venue)
        {
            if (venue == null) throw new ArgumentNullException(nameof(venue));
            
            var client = new PhotosClient(venue.HttpClient);
            return client.GetVenuePhotos(venue.Id, 50, 0);
        }
    }
}