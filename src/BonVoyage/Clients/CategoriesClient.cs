using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BonVoyage.Infrastructure;
using BonVoyage.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BonVoyage.Clients
{
    public class CategoriesClient : BaseClient
    {
        public CategoriesClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<IReadOnlyCollection<VenueCategory>> Get()
        {
            using (var response = await HttpClient.GetAsync("v2/venues/categories").ConfigureAwait(false))
            {
                var content = await response.Content.ReadAsStringAsync();

                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    throw new Exception(content, e);
                }

                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new ConnectedResourceConverter(HttpClient));
                
                var resultAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var jObject = DeserializeObject<JObject>(resultAsString);
                var categories = DeserializeObject<IEnumerable<VenueCategory>>(jObject["response"]["categories"].ToString());

                return new ReadOnlyCollection<VenueCategory>(categories.ToList());
            }
        }
    }
}