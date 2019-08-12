using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorStrap.Util
{
    public static class NavHelper
    {
        //private static string _hrefAbsolute;                                                                                   


        public static bool MatchActiveRoute(this string currentUriAbsolute, string _hrefAbsolute)
        {
            _hrefAbsolute = _hrefAbsolute.Replace("://", ":///");
            _hrefAbsolute = _hrefAbsolute.Replace("//", "/");

            if(_hrefAbsolute.ToLower() == currentUriAbsolute.ToLower())
            {
                return true;
            }
            if (currentUriAbsolute.Length == _hrefAbsolute?.Length - 1)
            {
                if (_hrefAbsolute[_hrefAbsolute.Length - 1] == '/'
                    && _hrefAbsolute.StartsWith(currentUriAbsolute, StringComparison.Ordinal))
                {
                    return true;
                }
            }
            return false;
        }
    }
}