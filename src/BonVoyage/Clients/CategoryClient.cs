using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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

            using (var response = await HttpClient.GetAsync($"v2/venues/search?near={placeName}&categoryId={categoryId}").ConfigureAwait(false))
            {
                var resultAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var jObject = JsonConvert.DeserializeObject<JObject>(resultAsString);
                var categories = JsonConvert.DeserializeObject<IEnumerable<CompactVenue>>(jObject["response"]["venues"].ToString());

                return new ReadOnlyCollection<CompactVenue>(categories.ToList());
            }
        }
    }

    public class VenueStats
    {
        public ulong CheckinsCount { get; set; }
        public ulong UsersCount { get; set; }
        public ulong TipCount { get; set; }
    }

    public class VenueLocation
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Cc { get; set; }
        public IEnumerable<string> FormattedAddress { get; set; }
    }

    /// <seealso href="https://developer.foursquare.com/docs/responses/venue" />
    public class CompactVenue
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Verified { get; set; }
        public VenueLocation Location { get; set; }
        public VenueStats Stats { get; set; }

        public Uri Url { get; set; }
        public IEnumerable<VenueCategory> Categories { get; set; }
    }

    public class CategoryClient : BaseClient
    {
        public CategoryClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<IReadOnlyCollection<VenueCategory>> Get()
        {
            using (var response = await HttpClient.GetAsync("v2/venues/categories").ConfigureAwait(false))
            {
                var resultAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var jObject = JsonConvert.DeserializeObject<JObject>(resultAsString);
                var categories = JsonConvert.DeserializeObject<IEnumerable<VenueCategory>>(jObject["response"]["categories"].ToString());

                return new ReadOnlyCollection<VenueCategory>(categories.ToList());
            }
        }
    }
}