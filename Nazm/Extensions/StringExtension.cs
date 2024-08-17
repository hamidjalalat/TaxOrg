using System.Text.RegularExpressions;

namespace Nazm.Extensions
{
    public static class StringExtension
    {
        public static int ToInt(this string val)
        {
            if (string.IsNullOrEmpty(val))
                return 0;
            int res = 0;
            if (int.TryParse(val, out res))
                return res;
            return 0;
        }
        public static string ToPersianST(this string value)
        {
            return value.Replace("آ", "ا").Replace("ئ", "ي").Replace("ء", "");
        }
        public static string StripHtmlTags(this string source)
        {
            return Regex.Replace(source, "<.*?>|&.*?;", string.Empty);
        }

        public static string NOETSubstring(this string source, int startIndex, int length)
        {
            if (source.Length < length)
                length = source.Length;
            return source.Substring(startIndex, length);
        }


    }
}
