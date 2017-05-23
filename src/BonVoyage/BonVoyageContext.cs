using BonVoyage.Infrastructure;
using System;
using System.Net.Http;

namespace BonVoyage
{
    public class BonVoyageContext : IDisposable
    {
        private readonly HttpMessageHandler _handler;

        public BonVoyageContext() : this(CreateHandler())
        {
        }

        public BonVoyageContext(HttpMessageHandler handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            _handler = handler;
        }

        public FoursquareContext CreateFoursquareContext(string accessToken)
        {
            if (accessToken == null)
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            return new FoursquareContext(_handler, accessToken);
        }

        public void Dispose()
        {
            _handler?.Dispose();
        }

        private static HttpMessageHandler CreateHandler()
        {
            return HttpClientFactory.CreatePipeline(new HttpClientHandler(), new DelegatingHandler[]
            {
                new OAuthTokenSwitchHandler(),
                new QueryAppenderHandler("v", "20151111")
            });
        }
    }
}
