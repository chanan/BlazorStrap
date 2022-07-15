using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap.V5_1
{
    public class BSCard : BSCardBase
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder()
            .AddClass(GetClass())
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Attributes.TryGetValue("src", out var value) && (value.ToString()?.Contains("placeholder:") ?? false) && CardType == CardType.Image)
            {
                builder.OpenComponent(0, typeof(BSImage));
                builder.AddAttribute(1, "Class", ClassBuilder);
                builder.AddAttribute(2, "IsPlaceholder", true);
                builder.AddAttribute(3, "Source", value.ToString()?.Replace("placeholder:", ""));
                builder.AddMultipleAttributes(4, Attributes);
                builder.CloseComponent();
            }
            else
            {
                builder.OpenElement(0, Tag);
                builder.AddAttribute(1, "class", ClassBuilder);
                builder.AddMultipleAttributes(2, Attributes);
                builder.AddContent(3, ChildContent);
                builder.CloseElement();
            }

        }

    }
}
