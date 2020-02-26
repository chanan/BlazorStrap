using System;

namespace BlazorStrap.Util
{
    public static class NavHelper
    {
        public static bool MatchActiveRoute(this string currentUriAbsolute, string hrefAbsolute)
        {
            hrefAbsolute = hrefAbsolute?.Replace("://", ":///");
            hrefAbsolute = hrefAbsolute?.Replace("//", "/");

            if (hrefAbsolute.ToUpperInvariant() == currentUriAbsolute?.ToUpperInvariant())
            {
                return true;
            }
            if (currentUriAbsolute.Length == hrefAbsolute?.Length - 1)
            {
                if (hrefAbsolute[hrefAbsolute.Length - 1] == '/'
                    && hrefAbsolute.StartsWith(currentUriAbsolute, StringComparison.Ordinal))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool MatchActiveRoute(this Uri currentUriAbsolute, string hrefAbsolute)
        {
            return MatchActiveRoute(currentUriAbsolute?.AbsoluteUri, hrefAbsolute);
        }
    }
}
