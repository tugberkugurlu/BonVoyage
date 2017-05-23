using System;

namespace BonVoyage.Models
{
    public class FoursquareIcon
    {
        public string Prefix { get; set; }
        public string Suffix { get; set; }

        public string GetUrl(FoursquareIconSize size)
        {
            if (size == default(FoursquareIconSize))
            {
                throw new ArgumentException("Size should be specified", nameof(size));
            }

            return $"{Prefix.TrimEnd('/')}{(int)size}{Suffix}";
        }
    }
}