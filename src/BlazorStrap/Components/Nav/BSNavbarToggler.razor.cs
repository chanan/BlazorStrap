using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSNavbarToggler : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
          new CssBuilder("navbar-toggler")
              .AddClass(Class)
          .Build();
        [Parameter] public EventCallback<UIMouseEventArgs> OnClick { get; set; }
        [Parameter] public string Class { get; set; }

        protected async Task Clicked(UIMouseEventArgs e)
        {
            await OnClick.InvokeAsync(e);
        }
    }
}
