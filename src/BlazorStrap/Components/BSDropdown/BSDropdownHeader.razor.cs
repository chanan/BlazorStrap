using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSDropdownHeader : BootstrapComponentBase
    {
        protected string classname =>
         new CssBuilder("dropdown-header")
        .AddClass(Class)
        .Build();

        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
