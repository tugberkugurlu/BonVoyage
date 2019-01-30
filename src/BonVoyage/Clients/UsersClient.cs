using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BonVoyage.Models;
using Newtonsoft.Json.Linq;

namespace BonVoyage.Clients
{
    public class UsersClient : BaseClient
    {
        public UsersClient(HttpClient httpClient) : base(httpClient)
        {
        }

        /// <remarks>
        /// Implementation of https://developer.foursquare.com/docs/users/users.
        /// </remarks>
        public async Task<CompactUser> Get()
        {
            using (var response = await HttpClient.GetAsync("v2/users/self").ConfigureAwait(false))
            {
                var resultAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var jObject = DeserializeObject<JObject>(resultAsString);
                var user = DeserializeObject<CompactUser>(jObject["response"]["user"].ToString());

                return user;
            }
        }

        /// <remarks>
        /// Returns a list of all venues visited by the specified user, along with how many visits and when they were last there.
        /// </remarks>
        /// <seealso href="https://developer.foursquare.com/docs/api/users/venuehistory" />
        public async Task<IReadOnlyCollection<CompactVenue>> GetVisitedVenues() 
        {
            using(var response = await HttpClient.GetAsync("v2/users/self/venuehistory")) 
            {
                var resultAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false); 
                var jObject = DeserializeObject<JObject>(resultAsString);
                var venueItems = DeserializeObject<IEnumerable<JObject>>(jObject["response"]["venues"]["items"].ToString());
                var venues = new List<CompactVenue>();
                foreach (var item in venueItems)
                {
                    venues.Add(DeserializeObject<CompactVenue>(item["venue"].ToString()));
                }

                return venues.AsReadOnly();
            }
        }
    }
}