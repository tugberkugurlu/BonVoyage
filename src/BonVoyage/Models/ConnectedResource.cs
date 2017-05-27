using System.Net.Http;

namespace BonVoyage.Models
{
    public abstract class ConnectedResource
    {
        internal HttpClient HttpClient { get; set; }
    }
}