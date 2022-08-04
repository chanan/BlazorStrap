using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Diagnostics.CodeAnalysis;

namespace BlazorStrap.Shared.Components.Forms
{
    public abstract class BSInputCheckboxBase<T> : BSInputBase<T> 
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

        protected bool _isToggle;
        [DisallowNull] private ElementReference? Element { get; set; }
        protected string InputType => IsRadio ? "radio" : "checkbox";

     
        protected abstract string? ToggleClassBuilder { get; }

        protected void RadioOnClickEvent()
        {
            T tempValue;
            if (IsRadio)
            {
                tempValue = CheckedValue;
            }
            else
            {
                if (!object.Equals(CheckedValue, Value) && !IsRadio)
                {
                    tempValue = CheckedValue;
                }
                else
                {
                    tempValue = UnCheckedValue;
                }
            }
            if ((ValidateOnChange || ValidateOnInput) && EditContext != null)
            {
                CurrentValue = tempValue;
            }
            else
            {
                Value = tempValue; 
            }
            ValueChanged.InvokeAsync(Value);
        }
        protected bool Checked()
        {
            return object.Equals(CheckedValue, Value);  
        }
        protected override bool TryParseValueFromString(string? value, out T result, [NotNullWhen(false)] out string? validationErrorMessage)
        {
            return TryParseString<T>.ToValue(value, out result, out validationErrorMessage);
        }
    }
}
