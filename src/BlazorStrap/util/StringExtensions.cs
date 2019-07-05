using System.Text;

namespace BlazorStrap.util
{
    public static class StringExtensions
    {
        public static string RemoveUnneededSpaces(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }

            bool bFound = false;
            StringBuilder sbBuffer = new StringBuilder(source.Length);

            foreach (char chr in source.Trim())
            {
                if (chr == ' ')
                {
                    if (bFound)
                    {
                        continue;
                    }

                    bFound = true;
                }
                else
                {
                    bFound = false;
                }

                sbBuffer.Append(chr);
            }

            string strResult = sbBuffer.ToString().Trim();
            if (string.IsNullOrWhiteSpace(strResult))
            {
                return null;
            }

            return strResult;
        }
    }
}