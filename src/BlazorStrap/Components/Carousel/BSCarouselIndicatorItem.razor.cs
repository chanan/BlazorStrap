using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSCarouselIndicatorItem : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder()
        .AddClass("active", IsActive)
        .Build();

        [Parameter] public bool IsActive { get; set; }
        [Parameter] public int Index { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChangedEvent { get; set; }

        protected async Task onclick()
        {
            await ActiveIndexChangedEvent.InvokeAsync(Index);
        }
    }
}
