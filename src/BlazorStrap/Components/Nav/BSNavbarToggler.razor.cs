using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSNavbarToggler : BootstrapComponentBase
    {
        protected string classname =>
          new CssBuilder("navbar-toggler")
              .AddClass(Class)
          .Build();
        [Parameter] protected EventCallback<UIMouseEventArgs> onclick { get; set; }
        [Parameter] protected string Class { get; set; }
    }
}
