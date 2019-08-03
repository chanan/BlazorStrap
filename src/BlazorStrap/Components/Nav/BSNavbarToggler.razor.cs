using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public class CodeBSNavbarToggler : BootstrapComponentBase
    {
        protected string classname =>
          new CssBuilder("navbar-toggler")
              .AddClass(Class)
          .Build();
        [Parameter] protected EventCallback<UIMouseEventArgs> OnClick { get; set; }
        [Parameter] protected string Class { get; set; }

        protected async Task Clicked(UIMouseEventArgs e)
        {
            await OnClick.InvokeAsync(e);
        }
    }
}
