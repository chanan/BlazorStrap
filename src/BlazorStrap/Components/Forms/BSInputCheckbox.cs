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
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;
        [Parameter] public bool IsOutlined { get; set; }
        [Parameter] public virtual T CheckedValue { get; set; }
        [Parameter] public virtual T UnCheckedValue { get; set; }
        protected bool IsRadio { get; set; }
        [Parameter] public bool IsToggle { get; set; }
        [DisallowNull] public ElementReference? Element { get; protected set; }

        [Parameter] public RenderFragment? Label { get; set; }
        private string InputType => IsRadio ? "radio" : "checkbox";
        [Parameter] public Size Size { get; set; }

        private string? ClassBuilder => new CssBuilder()
            .AddClass("form-check-input", !IsToggle)
            .AddClass("btn-check", IsToggle)
            .AddClass(ValidClass, IsValid)
            .AddClass(InvalidClass, IsInvalid)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private string? ToggleClassBuilder => new CssBuilder("btn")
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
            builder.AddAttribute(4, "onclick", EventCallback.Factory.Create(this, RadioOnClickEvent));
            builder.AddAttribute(7, "onblur", OnBlurEvent);
            builder.AddAttribute(8, "onfocus", OnFocusEvent);
            builder.AddAttribute(8, "checked", Checked());
            builder.AddAttribute(8, "disabled", IsDisabled);
            if(IsToggle)
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

        private void RadioOnClickEvent(MouseEventArgs e)
        {
            if (IsToggle && Value.Equals(CheckedValue) && !IsRadio)
                Value = UnCheckedValue;
            else
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