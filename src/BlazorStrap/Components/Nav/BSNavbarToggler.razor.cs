using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public abstract class BSNavbarTogglerBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string Classname =>
          new CssBuilder("navbar-toggler")
              .AddClass(Class)
          .Build();
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public string Class { get; set; }

        protected async Task Clicked(MouseEventArgs e)
        {
            await OnClick.InvokeAsync(e).ConfigureAwait(false);
        }
    }
}
