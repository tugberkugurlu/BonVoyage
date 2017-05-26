using System;
using System.Collections.Generic;

namespace BonVoyage.Models
{
    /// <seealso href="https://developer.foursquare.com/docs/responses/venue" />
    public class CompactVenue
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Verified { get; set; }
        public VenueLocation Location { get; set; }
        public VenueStats Stats { get; set; }

        public Uri Url { get; set; }
        public IEnumerable<VenueCategory> Categories { get; set; }
    }
}