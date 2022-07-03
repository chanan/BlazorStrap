using BlazorComponentUtilities;
using BlazorStrap.Bootstrap.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap.Bootstrap.V4
{
    internal class CloseButton : CloseButtonBase
    {
        public string? ClassBuilder() => new CssBuilder("close")
           .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
           .AddClass(Class, !string.IsNullOrEmpty(Class))
           .Build().ToNullString();

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var s = 0;
            builder.OpenElement(s, "button");
            builder.AddAttribute(s++, "class", ClassBuilder());
            builder.AddAttribute(s++, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, ClickEventAsync));
            builder.AddAttribute(s++, "disabled", IsDisabled);
            builder.AddMultipleAttributes(s++, Attributes);
            builder.OpenElement(s, "span");
            builder.AddAttribute(s, "aria-hidden", "true");
            builder.AddContent(s, new MarkupString("&times;"));
            builder.CloseElement();
            builder.CloseElement();
        }
    }
}