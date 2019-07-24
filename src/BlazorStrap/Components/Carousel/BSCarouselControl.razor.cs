using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public class CodeBSCarouselControl : BootstrapComponentBase
    {
        protected string classname =>
         new CssBuilder()
             .AddClass("carousel-control-prev", CarouselDirection == CarouselDirection.Previous)
             .AddClass("carousel-control-next", CarouselDirection == CarouselDirection.Next)
             .AddClass(Class)
         .Build();

        protected string iconClassname =>
            new CssBuilder()
                .AddClass("carousel-control-prev-icon", CarouselDirection == CarouselDirection.Previous)
                .AddClass("carousel-control-next-icon", CarouselDirection == CarouselDirection.Next)
            .Build();

        protected string directionName => CarouselDirection == CarouselDirection.Previous ? "Previous" : "Next";

        [Parameter] protected int ActiveIndex { get; set; }
        [Parameter] protected int NumberOfItems { get; set; }
        [Parameter] protected CarouselDirection CarouselDirection { get; set; } = CarouselDirection.Previous;
        [Parameter] protected string Class { get; set; }

        [Parameter] protected EventCallback<int> ActiveIndexChanged { get; set; }

        protected async Task _onclick(UIMouseEventArgs e)
        {
            if (CarouselDirection == CarouselDirection.Previous)
            {
                if (ActiveIndex == 0) { ActiveIndex = NumberOfItems - 1; }
                else { ActiveIndex = ActiveIndex - 1; }
            }
            else
            {
                if (ActiveIndex == NumberOfItems - 1) { ActiveIndex = 0; }
                else { ActiveIndex = ActiveIndex + 1; }

            }
            await ActiveIndexChanged.InvokeAsync(ActiveIndex);
        }
    }
}
