using System;
using System.Net.Http;
using System.Threading.Tasks;
using BonVoyage.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BonVoyage.Clients
{
    public class UsersClient
    {
        private readonly HttpClient _httpClient;

        public UsersClient(HttpClient httpClient)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            _httpClient = httpClient;
        }

        /// <remarks>
        /// Implementation of https://developer.foursquare.com/docs/users/users.
        /// </remarks>
        public async Task<FoursquareUser> Get()
        {
            var response = await _httpClient.GetAsync("v2/users/self").ConfigureAwait(false);
            var resultAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var jObject = JsonConvert.DeserializeObject<JObject>(resultAsString);
            var user = JsonConvert.DeserializeObject<FoursquareUser>(jObject["response"]["user"].ToString());

            return user;
        }
    }
}