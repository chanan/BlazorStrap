using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap.V4
{
    public partial class BSCloseButton : BSCloseButtonBase
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("close")
               .AddClass("bs-close")
               .AddClass("text-white", IsWhite & !IsDisabled)
               .AddClass("text-secondary", IsWhite & IsDisabled)
               .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
               .AddClass(Class, !string.IsNullOrEmpty(Class))
               .Build().ToNullString();

        protected override async Task ClickEventAsync(MouseEventArgs e)
        {
            if (OnClick.HasDelegate)
            {
                await EventUtil.AsNonRenderingEventHandler<MouseEventArgs>(OnClick.InvokeAsync).Invoke(e);
            }
        }
    }
}