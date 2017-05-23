namespace BonVoyage.Models
{
    public class Checkin
    {
        public string Id { get; set; }
        public string CreatedAt { get; set; }
        public Venue Venue { get; set; }
    }
}