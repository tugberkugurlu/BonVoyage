using System.Collections.Generic;

namespace BonVoyage.Models
{
    public class VenueCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PluralName { get; set; }
        public string ShortName { get; set; }
        public FoursquareIcon Icon { get; set; }
        public IEnumerable<VenueCategory> Categories { get; set; }
    }
}