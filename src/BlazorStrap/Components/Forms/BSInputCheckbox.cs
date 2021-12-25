using System.Diagnostics.CodeAnalysis;
using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{

    public class BSInputCheckbox<T> : BSInputBase<T>
    {
        [Parameter] public virtual T CheckedValue { get; set; }
        protected bool IsRadio { get; set; }
        [DisallowNull] public ElementReference? Element { get; protected set; }

        [Parameter] public RenderFragment? Label { get; set; }
        private string InputType => IsRadio ? "radio" : "checkbox";
        [Parameter] public Size Size { get; set; }

        private string? ClassBuilder => new CssBuilder("form-check-input")
            .AddClass(ValidClass, IsValid)
            .AddClass(InvalidClass, IsInvalid)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "input");
            builder.AddAttribute(1, "type", InputType);
            builder.AddAttribute(2, "class", ClassBuilder);
            builder.AddAttribute(3, "value", BindConverter.FormatValue(CurrentValue));
            builder.AddAttribute(4, "onclick", EventCallback.Factory.Create(this, RadioOnClickEvent));
            builder.AddAttribute(7, "onblur", OnBlurEvent);
            builder.AddAttribute(8, "onfocus", OnFocusEvent);
            builder.AddAttribute(8, "checked", Checked());
            builder.AddMultipleAttributes(10, AdditionalAttributes);
            builder.AddElementReferenceCapture(11, elReference => Element = elReference);
            builder.CloseElement();
        }

        private void RadioOnClickEvent(MouseEventArgs e)
        {
            Value = CheckedValue;
            ValueChanged.InvokeAsync(Value);
        }
        private bool Checked()
        {
            if (CheckedValue.Equals(Value))
                return true;
            return false;
        }
        protected override bool TryParseValueFromString(string? value, out T result, [NotNullWhen(false)] out string? validationErrorMessage)
        {
            return TryParseString<T>.ToValue(value, out result, out validationErrorMessage);
        }
    }
}