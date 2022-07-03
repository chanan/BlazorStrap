using BlazorComponentUtilities;
using BlazorStrap.Bootstrap.Base;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap.Bootstrap.V4
{
    internal class Alert : AlertBase
    {
        public string? ClassBuilder() => new CssBuilder("alert")
            .AddClass($"alert-{Color.NameToLower()}", Color != BSColor.Default)
            .AddClass("d-flex align-items-center", HasIcon)
            .AddClass("alert-dismissible", IsDismissible)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (IsDismissed) return;
            var s = 0;
            builder.OpenElement(s, "div");
            builder.AddAttribute(s++, "class", ClassBuilder());
            builder.AddAttribute(s++, "role", "alert");
            builder.AddMultipleAttributes(s++, Attributes);
            if (Header != null)
            {
                builder.OpenElement(s++, $"h{Heading}");
                builder.AddAttribute(s++, "class", "alert-heading");
                builder.AddContent(s++, Header);
                builder.CloseElement();
                builder.AddContent(s++, Content);
            }
            else if (HasIcon)
            {
                builder.AddContent(s++, (MarkupString)(Icons.GetAlertIcon(Color.NameToLower()) ?? ""));
                builder.OpenElement(s++, "div");
                builder.AddContent(s++, ChildContent);
                builder.CloseElement();
            }
            else
            {
                builder.AddContent(s++, ChildContent);
            }
            if (IsDismissible)
            {
                builder.OpenComponent(s++, typeof(BSCloseButton));
                builder.AddAttribute(s++, "OnClick", EventCallback.Factory.Create<MouseEventArgs>(this, CloseEventAsync));
                builder.CloseComponent();
            }
            builder.CloseElement();
        }

    }
}