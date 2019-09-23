using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public abstract class BSCarouselIndicatorItemBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }

        protected string Classname =>
        new CssBuilder()
        .AddClass("active", Parent.ActiveIndex < Parent.CarouselIndicatorItems.Count ? (Parent.CarouselIndicatorItems[Parent.ActiveIndex] == this) : false)
        .Build();

        [Parameter] public bool IsActive { get; set; }
        [Parameter] public int Index { get; set; }
        [CascadingParameter] internal BSCarousel Parent { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChangedEvent { get; set; }

        protected override void OnInitialized()
        {
            Parent.CarouselIndicatorItems.Add(this);
        }

        protected async Task OnClick()
        {
            await ActiveIndexChangedEvent.InvokeAsync(Index).ConfigureAwait(false);
        }
    }
}
