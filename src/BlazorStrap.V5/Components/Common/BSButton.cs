using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap.V5
{
    public class BSButton : BSButtonBase<Size>
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder()
                .AddClass("btn", !IsLinkType || HasButtonClass)
                .AddClass($"btn-outline-{Color.NameToLower()}", IsOutlined && (!IsLinkType || HasButtonClass))
                .AddClass($"btn-{Color.NameToLower()}", Color != BSColor.Default && !IsOutlined && (!IsLinkType || HasButtonClass))
                .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None && (!IsLinkType || HasButtonClass))
                .AddClass("btn-link", HasLinkClass)
                .AddClass("active", IsActive ?? false)
                .AddClass("disabled", IsDisabled)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, GetTag);
            if (!IsLinkType)
                builder.AddAttribute(1, "type", ButtonType());
            builder.AddAttribute(2, "class", ClassBuilder);
            builder.AddAttribute(3, "data-blazorstrap", DataId);
            if (!string.IsNullOrEmpty(Target))
            {
                builder.AddAttribute(4, "data-blazorstrap-target", Target);
                if (IsLinkType)
                    builder.AddAttribute(5, "href", "javascript:void(0);");
            }
            else
            {
                if (IsLinkType)
                    builder.AddAttribute(6, "href", UrlBase);
            }

            builder.AddAttribute(7, "onclick", ClickEvent);
            builder.AddMultipleAttributes(8, Attributes);
            builder.AddContent(9, @ChildContent);
            builder.AddElementReferenceCapture(10, elReference => Element = elReference);
            builder.CloseElement();
        }
    }
}
