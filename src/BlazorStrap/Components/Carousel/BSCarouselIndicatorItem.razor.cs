using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public class CodeBSCarouselIndicatorItem : BootstrapComponentBase
    {
        protected string classname =>
        new CssBuilder()
        .AddClass("active", IsActive)
        .Build();

        [Parameter] protected bool IsActive { get; set; }
        [Parameter] protected int Index { get; set; }
        [Parameter] protected EventCallback<int> ActiveIndexChangedEvent { get; set; }

        protected async Task onclick()
        {
            await ActiveIndexChangedEvent.InvokeAsync(Index);
        }
    }
}
