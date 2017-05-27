using BonVoyage.Models;

namespace BonVoyage.Clients
{
    public class VenuePhoto : FoursquarePhoto
    {
        /// <summary>
        /// A unique string identifier for this photo.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Seconds since epoch when this photo was created.
        /// </summary>
        public long CreatedAt { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        /// <summary>
        /// If present, the name and url for the application that created this photo.
        /// </summary>
        public PhotoSource Source { get; set; }

        /// <summary>
        /// If the checkin is not clear from context, then a compact checkin is present.
        /// </summary>
        public CompactCheckin Checkin { get; set; }

        /// <summary>
        /// If the user is not clear from context, then a compact user is present.
        /// </summary>
        public CompactUser User { get; set; }

        /// <summary>
        /// Can be one of "public" (everybody can see, including on the venue page),
        /// "friends" (only the poster's friends can see), or "private" (only the poster can see)
        /// </summary>
        public PhotoVisibility Visibility { get; set; }

        public string GetUrl()
        {
            return base.GetUrl(Height, Width);
        }
    }
}