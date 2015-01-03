using System.Text.RegularExpressions;

namespace FATBox.Util.Extensions
{
    public static class StringEx
    {
        public static bool FaultTolerantContains(this string str, string kw)
        {
            if (str == null) return false;
            if (kw == null) return false;
            return Regex.Matches(str, kw, RegexOptions.IgnoreCase).Count > 0;
        }
    }
}