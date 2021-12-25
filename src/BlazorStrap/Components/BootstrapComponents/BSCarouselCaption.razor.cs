using BlazorComponentUtilities;

namespace BlazorStrap
{
    public partial class BSCarouselCaption : BlazorStrapBase
    {
        private string? ClassBuilder => new CssBuilder("carousel-caption")
           
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}