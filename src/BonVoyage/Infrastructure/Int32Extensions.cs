using System.Globalization;

namespace BonVoyage.Infrastructure
{
    internal static class Int32Extensions
    {
        public static string ToStringInvariant(this int value) => value.ToString(CultureInfo.InvariantCulture);
    }
}