using System;
using System.Net.Http;
using BonVoyage.Infrastructure;
using Newtonsoft.Json;

namespace BonVoyage.Clients
{
    public abstract class BaseClient
    {
        protected BaseClient(HttpClient httpClient)
        {   
            HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            SerializerSettings = new JsonSerializerSettings();
            SerializerSettings.Converters.Add(new ConnectedResourceConverter(httpClient));
        }
     
        public JsonSerializerSettings SerializerSettings { get; }
        protected HttpClient HttpClient { get; }
        
        protected TObject DeserializeObject<TObject>(string value) => 
            JsonConvert.DeserializeObject<TObject>(value, SerializerSettings);
    }
}