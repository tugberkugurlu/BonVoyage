using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BonVoyage
{
    public class FoursquareContext : IDisposable
    {
        private readonly HttpClient _httpClient;

        public FoursquareContext(HttpMessageHandler handler, string accessToken)
        {
            if(handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (accessToken == null)
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            _httpClient = new HttpClient(handler, false)
            {
                BaseAddress = new Uri("https://api.foursquare.com")
            };

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            Checkins = new CheckinClient(_httpClient);
            Users = new UsersClient(_httpClient);
        }

        public CheckinClient Checkins { get; }

        public UsersClient Users { get; }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }

    public class CheckinClient
    {
        private readonly HttpClient _httpClient;

        public CheckinClient(HttpClient httpClient)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<Checkin>> Get()
        {
            var response = await _httpClient.GetAsync("v2/users/self/checkins").ConfigureAwait(false);
            var resultAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var jObject = JsonConvert.DeserializeObject<JObject>(resultAsString);
            var checkins = JsonConvert.DeserializeObject<IEnumerable<Checkin>>(jObject["response"]["checkins"]["items"].ToString());

            return new ReadOnlyCollection<Checkin>(checkins.ToList());
        }
    }

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

    public class FoursquareUser
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public FoursquarePhoto Photo { get; set; }
    }

    public class FoursquarePhoto
    {
        public string Prefix { get; set; }
        public string Suffix { get; set; }

        public string GetUrl(int height, int width)
        {
            return $"{Prefix.TrimEnd('/')}/{width}x{height}/{Suffix.TrimStart('/')}";
        }
    }

    public class Checkin
    {
        public string Id { get; set; }
        public string CreatedAt { get; set; }
        public Venue Venue { get; set; }
    }

    public class Venue
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
    }

    public class Location
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
