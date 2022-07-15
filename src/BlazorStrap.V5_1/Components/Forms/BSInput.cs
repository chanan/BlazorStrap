using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap.V5_1
{
    public class BSInput<TValue> : BSInputBase<TValue, Size>
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder()
                .AddClass($"form-control-{InputSize.ToDescriptionString()}", InputSize != Size.None && InputType != InputType.Select)
                .AddClass($"form-select-{InputSize.ToDescriptionString()}", InputSize != Size.None && InputType == InputType.Select)
                .AddClass("form-control", InputType != InputType.Select && InputType != InputType.Range)
                .AddClass("form-range", InputType == InputType.Range)
                .AddClass(BS.Form_Control_Plaintext, IsPlainText)
                .AddClass(ValidClass, IsValid)
                .AddClass("form-select", InputType == InputType.Select)
                .AddClass(InvalidClass, IsInvalid)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, Tag);
            if (Tag == "input")
                builder.AddAttribute(1, "type", InputType.ToDescriptionString());
            builder.AddAttribute(2, "class", ClassBuilder);
            if (IsMultipleSelect) 
            {
                builder.AddAttribute(4, "value", BindConverter.FormatValue(CurrentValue)?.ToString());
                builder.AddAttribute(5, "onchange", EventCallback.Factory.CreateBinder<string?[]?>(this, SetCurrentValueAsStringArray, default));
            }
            else
            {
                builder.AddAttribute(3, "value", BindConverter.FormatValue(CurrentValueAsString));
                builder.AddAttribute(4, "onchange", EventCallback.Factory.CreateBinder<string?>(this, OnChangeEvent, CurrentValueAsString));
                builder.AddAttribute(5, "oninput", EventCallback.Factory.CreateBinder<string?>(this, OnInputEvent, CurrentValueAsString));
            }
            builder.AddAttribute(6, "disabled", IsDisabled);
            builder.AddAttribute(7, "readonly", IsReadonly);
            builder.AddAttribute(8, "onblur", OnBlurEvent);
            builder.AddAttribute(9, "onfocus", OnFocusEvent);
            builder.AddMultipleAttributes(8, AdditionalAttributes);
            builder.AddAttribute(10, "multiple", IsMultipleSelect);
            builder.AddElementReferenceCapture(11, elReference => Element = elReference);
            if (Tag != "input")
                builder.AddContent(12, ChildContent);
            builder.CloseElement();
        }
    }
}
