using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V4
{
    public partial class BSCollapse : BSCollapseBase
    {
        [CascadingParameter] public BSNavbar? NavbarParent { get; set; }
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("collapse")
                .AddClass("show", Shown)
                .AddClass("navbar-collapse", IsInNavbar)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();

        protected override async Task OnResize(int width)
        {
            if (!IsInNavbar) return;
            if (width > 576 && NavbarParent?.Expand == Size.ExtraSmall ||
                width > 576 && NavbarParent?.Expand == Size.Small ||
                width > 768 && NavbarParent?.Expand == Size.Medium ||
                width > 992 && NavbarParent?.Expand == Size.Large ||
                width > 1200 && NavbarParent?.Expand == Size.ExtraLarge )
            {
                Shown = false;
                if (OnHidden.HasDelegate && !Shown)
                    _ = OnHidden.InvokeAsync();
                await InvokeAsync(StateHasChanged);
            }
        }
    }
}