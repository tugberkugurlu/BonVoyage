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
    }
}