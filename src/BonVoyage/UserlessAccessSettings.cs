using System;

namespace BonVoyage
{
    public class UserlessAccessSettings
    {
        public UserlessAccessSettings(string clientId, string clientSecret)
        {
            ClientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
            ClientSecret = clientSecret ?? throw new ArgumentNullException(nameof(clientSecret));
        }

        public string ClientId { get; }
        public string ClientSecret { get; }
    }
}