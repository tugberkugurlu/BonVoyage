﻿using BonVoyage.Infrastructure;
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
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public FoursquareContext CreateFoursquareContext(string accessToken)
        {
            if (accessToken == null)
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            return new FoursquareContext(_handler, accessToken);
        }

        public FoursquareUserlessContext CreateUserlessFoursquareContext(UserlessAccessSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return new FoursquareUserlessContext(_handler, settings);
        }

        public void Dispose()
        {
            _handler?.Dispose();
        }

        /// <seealso href="https://developer.foursquare.com/overview/versioning" />
        private static HttpMessageHandler CreateHandler()
        {
            return HttpClientFactory.CreatePipeline(new HttpClientHandler(), new DelegatingHandler[]
            {
                new OAuthTokenSwitchHandler(),
                new QueryAppenderHandler("v", "20170523")
            });
        }
    }
}
