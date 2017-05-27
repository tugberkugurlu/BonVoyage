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
    public class CheckinsClient : BaseClient
    {
        public CheckinsClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<IReadOnlyCollection<Checkin>> Get()
        {
            using (var response = await HttpClient.GetAsync("v2/users/self/checkins").ConfigureAwait(false))
            {
                var resultAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var jObject = JsonConvert.DeserializeObject<JObject>(resultAsString);
                var checkins = JsonConvert.DeserializeObject<IEnumerable<Checkin>>(jObject["response"]["checkins"]["items"].ToString());

                return new ReadOnlyCollection<Checkin>(checkins.ToList());
            }
        }
    }
}