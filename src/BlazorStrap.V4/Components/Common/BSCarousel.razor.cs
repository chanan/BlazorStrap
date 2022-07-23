using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;

namespace BlazorStrap.V4
{
    public partial class BSCarousel : BSCarouselBase
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("carousel")
                .AddClass("slide", IsSlide)
                .AddClass("carousel-fade", IsFade)
                .AddClass("carousel-dark", IsDark)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();

        protected override async Task DoAnimations(bool back)
        {
            await DoAnimationsV4(back);
        }
    }
}