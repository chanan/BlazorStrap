using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap.V5
{
    public class BSInputCheckbox<TValue> : BSInputCheckboxBase<TValue, Size>
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder()
                .AddClass("form-check-input", !IsToggle)
                .AddClass("btn-check", IsToggle)
                .AddClass(ValidClass, IsValid)
                .AddClass(InvalidClass, IsInvalid)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();

        protected override string? ToggleClassBuilder => new CssBuilder("btn")
                .AddClass($"btn-outline-{Color.NameToLower()}", IsOutlined)
                .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
                .AddClass($"btn-{Color.NameToLower()}", Color != BSColor.Default && !IsOutlined)
                .Build().ToNullString();

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var id = Guid.NewGuid();
            builder.OpenElement(0, "input");
            builder.AddAttribute(1, "type", InputType);
            builder.AddAttribute(2, "class", ClassBuilder);
            builder.AddAttribute(3, "value", BindConverter.FormatValue(CurrentValue));
            builder.AddAttribute(4, "onclick", RadioOnClickEvent);
            builder.AddAttribute(7, "onblur", OnBlurEvent);
            builder.AddAttribute(8, "onfocus", OnFocusEvent);
            builder.AddAttribute(8, "checked", Checked());
            builder.AddAttribute(8, "disabled", IsDisabled);
            if (IsToggle)
                builder.AddAttribute(8, "id", id);
            builder.AddMultipleAttributes(10, AdditionalAttributes);
            builder.AddElementReferenceCapture(11, elReference => Element = elReference);
            builder.CloseElement();
            if (IsToggle)
            {
                builder.OpenElement(12, "label");
                builder.AddAttribute(13, "for", id);
                builder.AddAttribute(14, "class", ToggleClassBuilder);
                builder.AddContent(14, @ChildContent);
                builder.CloseElement();
            }
        }
    }
}
