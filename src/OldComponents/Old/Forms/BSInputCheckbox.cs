using System.Diagnostics.CodeAnalysis;
using BlazorComponentUtilities;
using BlazorStrap.Bootstrap.V5_1.Enums;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{

    public class BSInputCheckbox<T> : BSInputBase<T>
    {
        /// <summary>
        /// Sets checkbox color.
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        /// <summary>
        /// Adds the <c>btn-outline</c> class.
        /// </summary>
        [Parameter] public bool IsOutlined { get; set; }

        /// <summary>
        /// Value of <typeparamref name="T"/> when input is checked.
        /// </summary>
        [Parameter] public virtual T? CheckedValue { get; set; }

        /// <summary>
        /// Value of <typeparamref name="T"/> when input is unchecked.
        /// </summary>
        [Parameter] public virtual T? UnCheckedValue { get; set; }

        protected bool IsRadio { get; set; }

        /// <summary>
        /// Display as toggle buttons.
        /// </summary>
        [Parameter] public bool IsToggle { get; set; }

        [DisallowNull] private ElementReference? Element { get; set; }
        private string InputType => IsRadio ? "radio" : "checkbox";

        /// <summary>
        /// Size of input.
        /// </summary>
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

        [SuppressMessage("Style", "IDE0045:Convert to conditional expression", Justification = "<Pending>")]
        private void RadioOnClickEvent(MouseEventArgs e)
        {
            if (Value == null)
            {
                if (CheckedValue == null)
                    Value = default(T);
            }
            else if (IsToggle && Value.Equals(CheckedValue) && !IsRadio)
            {
                Value = UnCheckedValue;
            }
            else if (Value.Equals(CheckedValue) && !IsRadio)
            {
                Value = UnCheckedValue;
            }
            else
                Value = CheckedValue;
            ValueChanged.InvokeAsync(Value);
        }
        private bool Checked()
        {
            if (CheckedValue == null)
            {
                if (Value == null)
                    return true;
            }
            else if (CheckedValue.Equals(Value))
                return true;
            return false;
        }
        protected override bool TryParseValueFromString(string? value, out T result, [NotNullWhen(false)] out string? validationErrorMessage)
        {
            return TryParseString<T>.ToValue(value, out result, out validationErrorMessage);
        }
    }
}