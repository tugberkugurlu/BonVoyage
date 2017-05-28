using System;
using System.Net.Http;

namespace BonVoyage.Models
{
    public abstract class ConnectedResource
    {
        protected ConnectedResource(HttpClient httpClient)
        {
            HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        
        internal HttpClient HttpClient { get; }
    }
}