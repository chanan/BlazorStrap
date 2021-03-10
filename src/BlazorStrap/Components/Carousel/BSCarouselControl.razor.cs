using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public partial class BSCarouselControl : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }

        protected string Classname =>
         new CssBuilder()
             .AddClass("carousel-control-prev", CarouselDirection == CarouselDirection.Previous)
             .AddClass("carousel-control-next", CarouselDirection == CarouselDirection.Next)
             .AddClass(Class)
         .Build();

        protected string IconClassname =>
            new CssBuilder()
                .AddClass("carousel-control-prev-icon", CarouselDirection == CarouselDirection.Previous)
                .AddClass("carousel-control-next-icon", CarouselDirection == CarouselDirection.Next)
            .Build();

        protected string DirectionName => CarouselDirection == CarouselDirection.Previous ? "Previous" : "Next";

        [Parameter] public CarouselDirection CarouselDirection { get; set; } = CarouselDirection.Previous;
        [Parameter] public string Class { get; set; }
        [CascadingParameter] internal BSCarousel Parent { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }

        protected async Task OnClick()
        {
            if (Parent.AnimationRunning) return;
            if (CarouselDirection == CarouselDirection.Previous)
            {
                await Parent.GoToPrevItem().ConfigureAwait(true);
            }
            else
            {
                await Parent.GoToNextItem().ConfigureAwait(true);
            }
            await ActiveIndexChanged.InvokeAsync(Parent.ActiveIndex).ConfigureAwait(true);
        }
    }
}
