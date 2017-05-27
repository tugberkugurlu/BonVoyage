namespace BonVoyage.Models
{
    public class CompactCheckin
    {
        /// <summary>
        /// A unique identifier for this checkin.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// One of checkin, shout, or venueless.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The offset in minutes between when this check-in occurred and the same time in UTC.
        /// For example, a check-in that happened at -0500 UTC will have a timeZoneOffset of -300.
        /// </summary>
        public int TimeZoneOffset { get; set; }

        /// <summary>
        /// Seconds since epoch when this checkin was created.
        /// </summary>
        public long CreatedAt { get; set; }
    }
}