using System.Linq;

namespace FATBox.Core
{
    public class Localizer
    {
        public static string Localize(string str)
        {
            if (str == null) return null;
            return str.Split('>').Last().Trim();
        }
    }
}