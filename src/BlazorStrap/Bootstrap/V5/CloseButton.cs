using BlazorComponentUtilities;
using BlazorStrap.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap.Bootstrap.V5
{
    internal class CloseButton : ICloseButton
    {
        public IDictionary<string, object> Attributes { get; set; }
        public RenderFragment? ChildContent { get; set; }
        public string DataId { get; set; }
        public string? Class { get; set; }
        public string? LayoutClass { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsWhite { get; set; }
        public EventCallback<MouseEventArgs> OnClick { get; set; }
        public void SetParameters(bool isDisabled, bool isWhite, EventCallback<MouseEventArgs> onClick, IDictionary<string, object> attributes, RenderFragment? childContent, string dataId, string? @class, string? layoutClass)
        {
            Attributes = attributes;
            ChildContent = childContent;
            DataId = dataId;
            Class = @class;
            LayoutClass = layoutClass;
            IsDisabled = isDisabled;
            IsWhite = isWhite;
            OnClick = onClick;
        }

        public string? ClassBuilder() => new CssBuilder("btn-close")
           .AddClass("btn-close-white", IsWhite)
           .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
           .AddClass(Class, !string.IsNullOrEmpty(Class))
           .Build().ToNullString();
        
        public RenderFragment Output()
        {
            var secquence = 0;
            var result = new RenderFragment(builder =>
            {
                builder.OpenElement(secquence, "button");
                builder.AddAttribute(secquence++, "class", ClassBuilder());
                builder.AddAttribute(secquence++, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnClick));
                builder.AddAttribute(secquence++, "disabled", IsDisabled);
                builder.AddMultipleAttributes(secquence++, Attributes);
                builder.CloseElement();
            });
            return result;
        }
    }
}