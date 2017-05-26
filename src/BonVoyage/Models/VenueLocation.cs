using System.Collections.Generic;

namespace BonVoyage.Models
{
    public class VenueLocation
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Cc { get; set; }
        public IEnumerable<string> FormattedAddress { get; set; }
    }
}