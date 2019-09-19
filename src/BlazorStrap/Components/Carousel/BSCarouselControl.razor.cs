using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public abstract class BSCarouselControlBase : ComponentBase
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

        [Parameter] public int NumberOfItems { get; set; }
        [Parameter] public CarouselDirection CarouselDirection { get; set; } = CarouselDirection.Previous;
        [Parameter] public string Class { get; set; }
        [CascadingParameter] internal BSCarousel Parent { get; set; }

        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }

        protected async Task OnClick(MouseEventArgs e)
        {
            if (Parent.AnimationRunning) return;
            if (CarouselDirection == CarouselDirection.Previous)
            {
                Parent.Direction = 1;
                Parent.ResetTimer();
                if (Parent.ActiveIndex == 0) { Parent.ActiveIndex = NumberOfItems - 1; }
                else { Parent.ActiveIndex = Parent.ActiveIndex - 1; }
            }
            else
            {
                Parent.Direction = 0;
                Parent.ResetTimer();
                if (Parent.ActiveIndex == NumberOfItems - 1) { Parent.ActiveIndex = 0; }
                else { Parent.ActiveIndex = Parent.ActiveIndex + 1; }

            }
            await ActiveIndexChanged.InvokeAsync(Parent.ActiveIndex);
        }
    }
}
