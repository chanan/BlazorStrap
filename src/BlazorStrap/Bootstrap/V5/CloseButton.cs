using BlazorComponentUtilities;
using BlazorStrap.Bootstrap.Base;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap.Bootstrap.V5
{
    internal class CloseButton : CloseButtonBase
    {
        public string? ClassBuilder() => new CssBuilder("btn-close")
           .AddClass("btn-close-white", IsWhite)
           .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
           .AddClass(Class, !string.IsNullOrEmpty(Class))
           .Build().ToNullString();

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var s = 0;
            builder.OpenElement(s, "button");
            builder.AddAttribute(s++, "class", ClassBuilder());
            builder.AddAttribute(s++, "onclick", EventUtil.AsNonRenderingEventHandler<MouseEventArgs>(ClickEventAsync));
            builder.AddAttribute(s++, "disabled", IsDisabled);
            builder.AddMultipleAttributes(s++, Attributes);
            builder.CloseElement();
        }
        
    }
}