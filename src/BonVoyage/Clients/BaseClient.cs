using System;
using System.Net.Http;

namespace BonVoyage.Clients
{
    public abstract class BaseClient
    {
        protected BaseClient(HttpClient httpClient)
        {
            HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        
        protected HttpClient HttpClient { get; }
    }
}