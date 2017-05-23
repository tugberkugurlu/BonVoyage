using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BonVoyage.Infrastructure
{
    internal class QueryAppenderHandler : DelegatingHandler
    {
        private readonly string _queryStringValue;

        public QueryAppenderHandler(string parameter, string value)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _queryStringValue = $"{parameter}={value}";
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.RequestUri = string.IsNullOrWhiteSpace(request.RequestUri.Query)
                ? new Uri(string.Concat(request.RequestUri.ToString(), "?", _queryStringValue))
                : new Uri(string.Concat(request.RequestUri.ToString(), "&", _queryStringValue));

            return base.SendAsync(request, cancellationToken);
        }
    }
}
