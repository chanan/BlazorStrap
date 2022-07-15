using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Diagnostics.CodeAnalysis;

namespace BlazorStrap.Shared.Components.Forms
{
    public abstract class BSInputCheckboxBase<T, TSize> : BSInputBase<T, TSize> where TSize : Enum
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
        [Parameter] public TSize Size { get; set; }
        protected abstract string? ToggleClassBuilder { get; }


        protected void RadioOnClickEvent(MouseEventArgs e)
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
        protected bool Checked()
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
