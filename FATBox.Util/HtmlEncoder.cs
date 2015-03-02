namespace FATBox.Util
{
    public static class HtmlEncoder
    {
        public static string Encode(string text)
        {
            return System.Web.HttpUtility.HtmlEncode(text ?? "")
                .Replace("\r\n", "<BR>");
        }
    }
}