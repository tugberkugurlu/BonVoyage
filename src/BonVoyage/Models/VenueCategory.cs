using System.Collections.Generic;
using System.Net.Http;

namespace BonVoyage.Models
{
    public class VenueCategory : ConnectedResource
    {
        public VenueCategory(HttpClient httpClient) : base(httpClient)
        {
        }
        
        public string Id { get; set; }
        public string Name { get; set; }
        public string PluralName { get; set; }
        public string ShortName { get; set; }
        public FoursquareIcon Icon { get; set; }
        public IEnumerable<VenueCategory> Categories { get; set; }
    }
}