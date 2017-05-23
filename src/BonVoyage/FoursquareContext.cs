using System;
using System.Net.Http;
using System.Net.Http.Headers;
using BonVoyage.Clients;

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
                BaseAddress = new Uri(Constants.FoursquareApiBaseUrl)
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
}
