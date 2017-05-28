using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BonVoyage.Infrastructure;
using BonVoyage.Models;
using Newtonsoft.Json.Linq;

namespace BonVoyage.Clients
{
    public class PhotosClient : BaseClient
    {
        public PhotosClient(HttpClient httpClient) : base(httpClient)
        {
        }
        
        /// <summary>
        /// Returns photos for a venue.
        /// </summary>
        /// <param name="venueId">The id of the venue you want photos for.</param>
        /// <param name="limit">Number of results to return, up to 200.</param>
        /// <param name="offset">Used to page through results.</param>
        /// <returns>A list of venue photos. <seealso href="https://developer.foursquare.com/docs/responses/photo" /></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public async Task<IReadOnlyCollection<VenuePhoto>> GetVenuePhotos(string venueId, int limit, int offset)
        {
            if (venueId == null) throw new ArgumentNullException(nameof(venueId));
            if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset), offset, "Cannot be lower than 0");
            if (limit < 1) throw new ArgumentOutOfRangeException(nameof(limit), limit, "Cannot be lower than 1");
            if (limit > 200) throw new ArgumentOutOfRangeException(nameof(limit), limit, "Cannot be greater than 200");

            var url = $"v2/venues/{venueId}/photos?limit={limit.ToStringInvariant()}&offset={offset.ToStringInvariant()}";
            using (var response = await HttpClient.GetAsync(url).ConfigureAwait(false))
            {
                var resultAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var jObject = DeserializeObject<JObject>(resultAsString);
                var categories = DeserializeObject<IEnumerable<VenuePhoto>>(jObject["response"]["photos"]["items"].ToString());

                return new ReadOnlyCollection<VenuePhoto>(categories.ToList());
            }
        }
    }
}