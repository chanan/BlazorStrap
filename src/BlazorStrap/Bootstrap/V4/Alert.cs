using BlazorComponentUtilities;
using BlazorStrap.Interfaces;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap.Bootstrap.V4
{
    internal class Alert : IAlert
    {
        public void SetParameters(BSColor color, RenderFragment? content, EventCallback dismissed, bool hasIcon, RenderFragment? header, int heading, bool isDismissible, IDictionary<string, object> attributes, RenderFragment? childContent, string dataId, string? @class, string? layoutClass)
        {
            Color = color;
            Content = content;
            Dismissed = dismissed;
            HasIcon = hasIcon;
            Header = header;
            Heading = heading;
            IsDismissible = isDismissible;
            Attributes = attributes;
            ChildContent = childContent;
            DataId = dataId;
            Class = @class;
            LayoutClass = layoutClass;
        }

        public BSColor Color { get; set; }
        public RenderFragment? Content { get; set; }
        public EventCallback Dismissed { get; set; }
        public bool HasIcon { get; set; }
        public RenderFragment? Header { get; set; }
        public int Heading { get; set; }
        public bool IsDismissible { get; set; }
        public IDictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();
        public RenderFragment? ChildContent { get; set; }
        public string DataId { get; set; } = Guid.NewGuid().ToString();
        public string? Class { get; set; }
        public string? LayoutClass { get; set; }

        public string? ClassBuilder() => new CssBuilder("alert")
            .AddClass($"alert-{Color.NameToLower()}", Color != BSColor.Default)
            .AddClass("d-flex align-items-center", HasIcon)
            .AddClass("alert-dismissible", IsDismissible)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        public RenderFragment Output()
        {
            var secquence = 0;
            var result = new RenderFragment(builder =>
            {
                builder.OpenElement(secquence, "div");
                builder.AddAttribute(secquence++, "class", ClassBuilder());
                builder.AddAttribute(secquence++, "role", "alert");
                builder.AddMultipleAttributes(secquence++, Attributes);
                if (Header != null)
                {
                    builder.OpenElement(secquence++, $"h{Heading}");
                    builder.AddAttribute(secquence++, "class", "alert-heading");
                    builder.AddContent(secquence++, Header);
                    builder.CloseElement();
                    builder.AddContent(secquence++, Content);
                }
                else if (HasIcon)
                {
                    builder.AddContent(secquence++, (MarkupString)(Icons.GetAlertIcon(Color.NameToLower()) ?? ""));
                    builder.AddContent(secquence++, ChildContent);
                }
                else
                {
                    builder.AddContent(secquence++, ChildContent);
                }
                if (IsDismissible)
                {
                    builder.OpenComponent(secquence++, typeof(BSCloseButton));
                    builder.AddAttribute(0, "OnClick", EventCallback.Factory.Create<MouseEventArgs>(this, Dismissed));
                    builder.CloseComponent();
                }
                builder.CloseElement();
            });
            return result;
        }

    }
}