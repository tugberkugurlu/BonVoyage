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
    public class CheckinClient
    {
        private readonly HttpClient _httpClient;

        public CheckinClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IReadOnlyCollection<Checkin>> Get()
        {
            using (var response = await _httpClient.GetAsync("v2/users/self/checkins").ConfigureAwait(false))
            {
                var resultAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var jObject = JsonConvert.DeserializeObject<JObject>(resultAsString);
                var checkins = JsonConvert.DeserializeObject<IEnumerable<Checkin>>(jObject["response"]["checkins"]["items"].ToString());

                return new ReadOnlyCollection<Checkin>(checkins.ToList());
            }
        }
    }
}