namespace BonVoyage.Models
{
    public class FoursquarePhoto
    {
        public string Prefix { get; set; }
        public string Suffix { get; set; }

        public string GetUrl(int height, int width)
        {
            return $"{Prefix.TrimEnd('/')}/{width}x{height}/{Suffix.TrimStart('/')}";
        }
    }
}