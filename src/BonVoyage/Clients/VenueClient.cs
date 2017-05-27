using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BonVoyage.Infrastructure;
using BonVoyage.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BonVoyage.Clients
{
    public class VenueClient : BaseClient
    {
        public VenueClient(HttpClient httpClient) : base(httpClient)
        {
        }

        /// <param name="placeName">
        /// The name of a place in the world (i.e. San Fransisco, CA). Used to pass as the value for 'near' parameter.
        /// </param>
        /// <param name="categoryId">
        /// The id of the category to limit results to. If specifying a top-level category, all sub-categories will also match the query.
        /// </param>
        /// <seealso href="https://developer.foursquare.com/docs/venues/search" />
        public Task<IEnumerable<CompactVenue>> Search(string placeName, string categoryId)
        {
            return Search(placeName, categoryId, 50);
        }

        /// <param name="placeName">
        /// The name of a place in the world (i.e. San Fransisco, CA). Used to pass as the value for 'near' parameter.
        /// </param>
        /// <param name="categoryId">
        /// The id of the category to limit results to. If specifying a top-level category, all sub-categories will also match the query.
        /// </param>
        /// <param name="limit">The number of search results. Min: 1, Max: 50</param>
        /// <seealso href="https://developer.foursquare.com/docs/venues/search" />
        public async Task<IEnumerable<CompactVenue>> Search(string placeName, string categoryId, int limit)
        {
            if (placeName == null) throw new ArgumentNullException(nameof(placeName));
            if (categoryId == null) throw new ArgumentNullException(nameof(categoryId));
            if (limit < 1) throw new ArgumentOutOfRangeException(nameof(limit), limit, "Cannot be lower than 1");
            if (limit > 50) throw new ArgumentOutOfRangeException(nameof(limit), limit, "Cannot be greater than 50");

            using (var response = await HttpClient.GetAsync($"v2/venues/search?near={placeName}&categoryId={categoryId}&limit={limit.ToString(CultureInfo.InvariantCulture)}").ConfigureAwait(false))
            {
                var resultAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var jObject = JsonConvert.DeserializeObject<JObject>(resultAsString);
                var categories = JsonConvert.DeserializeObject<IEnumerable<CompactVenue>>(jObject["response"]["venues"].ToString());

                return new ReadOnlyCollection<CompactVenue>(categories.ToList());
            }
        }

        /// <summary>
        /// Returns photos for a venue.
        /// </summary>
        /// <param name="venueId">The venue you want photos for.</param>
        /// <param name="limit">Number of results to return, up to 200.</param>
        /// <param name="offset">Used to page through results.</param>
        /// <returns>A list of venue photos. <seealso href="https://developer.foursquare.com/docs/responses/photo" /></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public async Task<IEnumerable<VenuePhoto>> GetPhotos(string venueId, int limit, int offset)
        {
            if (venueId == null) throw new ArgumentNullException(nameof(venueId));
            if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset), offset, "Cannot be lower than 0");
            if (limit < 1) throw new ArgumentOutOfRangeException(nameof(limit), limit, "Cannot be lower than 1");
            if (limit > 200) throw new ArgumentOutOfRangeException(nameof(limit), limit, "Cannot be greater than 200");

            var url = $"v2/venues/{venueId}/photos?limit={limit.ToStringInvariant()}&offset={offset.ToStringInvariant()}";
            using (var response = await HttpClient.GetAsync(url).ConfigureAwait(false))
            {
                var resultAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var jObject = JsonConvert.DeserializeObject<JObject>(resultAsString);
                var categories = JsonConvert.DeserializeObject<IEnumerable<VenuePhoto>>(jObject["response"]["photos"]["items"].ToString());

                return new ReadOnlyCollection<VenuePhoto>(categories.ToList());
            }
        }
    }
}