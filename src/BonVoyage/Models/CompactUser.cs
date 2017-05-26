namespace BonVoyage.Models
{
    public class CompactUser
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public FoursquarePhoto Photo { get; set; }
    }
}