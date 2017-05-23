using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BonVoyage.Infrastructure
{
    internal class OAuthTokenSwitchHandler : DelegatingHandler
    {
        private const string OAuthQueryStringParameter = "oauth_token";

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = request.Headers.Authorization.Parameter;
            var queryStringValue = $"{OAuthQueryStringParameter}={accessToken}";

            request.RequestUri = string.IsNullOrWhiteSpace(request.RequestUri.Query)
                ? new Uri(string.Concat(request.RequestUri.ToString(), "?", queryStringValue))
                : new Uri(string.Concat(request.RequestUri.ToString(), "&", queryStringValue));

            request.Headers.Authorization = null;

            return base.SendAsync(request, cancellationToken);
        }
    }
}
