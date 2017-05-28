using System;
using System.Net.Http;
using System.Reflection;
using BonVoyage.Models;
using Newtonsoft.Json;

namespace BonVoyage.Infrastructure
{
    public class ConnectedResourceConverter : JsonConverter
    {
        private readonly HttpClient _httpClient;

        public ConnectedResourceConverter(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        
        public override bool CanWrite { get; } = false;

        public override bool CanConvert(Type objectType) => 
            typeof(ConnectedResource).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => 
            throw new NotImplementedException();
        
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var connectedResource = Activator.CreateInstance(objectType, _httpClient);
            serializer.Populate(reader, connectedResource);

            return connectedResource;
        }
    }
}