using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using BonVoyage.Models;

namespace BonVoyage.Infrastructure
{
    internal static class ConnectedResourceExtensions
    {
        public static IEnumerable<TConnectedResource> SelectWithHttpClient<TConnectedResource>(
            this IEnumerable<TConnectedResource> source, HttpClient httpClient) where TConnectedResource : ConnectedResource
        {
            return source.Select(resource =>
            {
                resource.HttpClient = httpClient;
                return resource;
            });
        }
    }
}