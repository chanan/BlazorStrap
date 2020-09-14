using BlazorComponentUtilities;
using BlazorStrap.Util;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlazorStrap
{
   
    public class BSBasicInput<T> : ComponentBase
    {
        // Constants
        private const string _dateFormat = "yyyy-MM-dd";

        // Protected variables
        protected string Classname =>
        new CssBuilder()
           .AddClass($"form-control-{Size.ToDescriptionString()}", Size != Size.None)
           .AddClass("is-valid", IsValid)
           .AddClass("is-invalid", IsInvalid)
           .AddClass(GetClass())
           .AddClass(Class)
         .Build();

        protected string Tag => InputType switch
        {
            InputType.Select => "select",
            InputType.TextArea => "textarea",
            _ => "input"
        };

        protected ElementReference ElementReference;
        protected string Type => InputType.ToDescriptionString();
        private FieldIdentifier _fieldIdentifier { get; set; }

        // Dependency Injection
        [Inject] protected BlazorStrapInterop BlazorStrapInterop { get; set; }

        // Parameters
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Parameter] public Expression<Func<object>> For { get; set; }
        [Parameter] public InputType InputType { get; set; } = InputType.Text;
        [Parameter] public Size Size { get; set; } = Size.None;
        [Parameter] public string MaxDate { get; set; } = "9999-12-31";
        [Parameter] public virtual T Value { get; set; }
        [Parameter] public virtual T RadioValue { get; set; }
        [Parameter] public virtual T CheckValue { get; set; }
        [Parameter] public virtual EventCallback<T> ValueChanged { get; set; }
        [Parameter] public EventCallback<string> ConversionError { get; set; }
        [Parameter] public bool IsReadonly { get; set; }
        [Parameter] public bool IsPlaintext { get; set; }
        [Parameter] public bool IsDisabled { get; set; }
        [Parameter] public bool IsChecked { get; set; }
        [Parameter] public bool IsValid { get; set; }
        [Parameter] public bool IsInvalid { get; set; }
        [Parameter] public bool IsMultipleSelect { get; set; }
        [Parameter] public int? SelectSize { get; set; }
        [Parameter] public int? SelectedIndex { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public bool ValidateOnInput { get; set; } = false;
        [Parameter] public int DebounceInterval { get; set; } = 500;
        [Parameter] public RenderFragment ChildContent { get; set; }

        // Cascading Parameters
        [CascadingParameter] protected EditContext MyEditContext { get; set; }
        [CascadingParameter] public BSLabel BSLabel { get; set; }

        // Overrides
        protected override void OnParametersSet()
        {
            if (For != null)
            {
                _fieldIdentifier = FieldIdentifier.Create(For);
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var index = -1;

            builder?.OpenElement(index++, Tag);
            builder.AddMultipleAttributes(index++, UnknownParameters);
            builder.AddAttribute(index++, "class", Classname);
            builder.AddAttribute(index++, "type", Type);
            builder.AddAttribute(index++, "readonly", IsReadonly);
            builder.AddAttribute(index++, "disabled", IsDisabled);
            builder.AddAttribute(index++, "multiple", IsMultipleSelect);
            builder.AddAttribute(index++, "size", SelectSize);
            builder.AddAttribute(index++, "selectedIndex", SelectedIndex);

            // Build checkbox
            if (InputType == InputType.Checkbox)
            {
                index = BuildCheckbox(builder, index);
            }
            // Build Radio
            else if (InputType == InputType.Radio)
            {
                index = BuildRadio(builder, index);
            }
            else
            {
                builder.AddAttribute(index++, "value", BindConverter.FormatValue(CurrentValueAsString));

                index = BuildValidation(builder, index);

                if (InputType == InputType.Date && !string.IsNullOrEmpty(MaxDate))
                {
                    builder.AddAttribute(index++, "max", MaxDate);
                }
            }

            builder.AddContent(index++, ChildContent);
            builder.AddElementReferenceCapture(index++, er => ElementReference = er);
            builder.CloseElement();
        }


        // Public Methods
        public void ForceValidate()
        {
            MyEditContext?.Validate();
            StateHasChanged();
        }

        public ValueTask<object> Focus() => BlazorStrapInterop.FocusElement(ElementReference);

        //Protected Methods
        protected void OnInput(string e)
        {
            RateLimitingExceptionForObject.Debounce(e, DebounceInterval, (CurrentValueAsString) => {
                InvokeAsync(() => OnChange(e));
            });
        }

        protected void OnChange(string e)
        {
            CurrentValueAsString = e;
        }

        private void OnRadioClick()
        {
            Value = (T)(object)(RadioValue);
            ValueChanged.InvokeAsync(Value);
        }

        protected void OnClick(MouseEventArgs e)
        {
            if (InputType == InputType.Radio)
            {
                OnRadioClick();
            }
            else
            {
                if (typeof(T) != typeof(bool) && typeof(T) != typeof(bool?))
                {
                    if (CheckValue != null)
                    {
                        if (Value != null)
                        {
                            Value = default(T);
                            ValueChanged.InvokeAsync(Value);
                        }
                        else
                        {
                            Value = CheckValue;
                            ValueChanged.InvokeAsync(Value);
                        }
                    }
                    else
                    {
                        if(Value == null)
                        {
                            Value = (T)(object)true;
                        }
                        else
                        {
                            Value = (T)(object) (((bool) (object) Value == true) ? false : true); 
                        }
                    }
                }
                else
                {

                    var tmp = (bool?)(object)Value;
                    Value = (T)(object)(!tmp);
                }
                ValueChanged.InvokeAsync(Value);
            }
        }

        protected string FormatValueAsString(T value)
        {
            if (typeof(T) == typeof(bool?))
            {
                if (value == null)
                {
                    return "";
                }
            }
            return value switch
            {
                null => null,
                bool @bool => BindConverter.FormatValue(@bool.ToString().ToLowerInvariant(), CultureInfo.InvariantCulture),
                int @int => BindConverter.FormatValue(@int, CultureInfo.InvariantCulture),
                long @long => BindConverter.FormatValue(@long, CultureInfo.InvariantCulture),
                float @float => BindConverter.FormatValue(@float, CultureInfo.InvariantCulture),
                double @double => BindConverter.FormatValue(@double, CultureInfo.InvariantCulture),
                decimal @decimal => BindConverter.FormatValue(@decimal, CultureInfo.InvariantCulture),
                DateTime dateTimeValue => BindConverter.FormatValue(dateTimeValue, _dateFormat, CultureInfo.InvariantCulture),
                DateTimeOffset dateTimeOffsetValue => BindConverter.FormatValue(dateTimeOffsetValue, _dateFormat, CultureInfo.InvariantCulture),
                _ => value?.ToString()
            };
        }

        protected bool TryParseValueFromString(string value, out T result, out string validationErrorMessage)
        {
            bool? boolToReturn;
            Type type = typeof(T);

            // Moved to static class in adherence to DRY
            boolToReturn = TryParseString<T>.ToValue(value, out result, out validationErrorMessage);

            if (typeof(T) == typeof(bool))
            {
                if (InputType != InputType.Select)
                {
                    result = (T)(object)false;
                    validationErrorMessage = string.Format(CultureInfo.InvariantCulture, "The bool valued must be used with select, checkboxes, or radios.");
                    boolToReturn = false;
                }
            }

            return boolToReturn != null
                ? boolToReturn.Value
                : throw new InvalidOperationException($"{GetType()} does not support the type '{typeof(T)}'.");
        }

        protected string CurrentValueAsString
        {
            get => FormatValueAsString(CurrentValue);
            set
            {
                _ = TryParseValueFromString(value, out T parsedValue, out var validationErrorMessage);
                CurrentValue = parsedValue;
                _ = ConversionError.InvokeAsync(validationErrorMessage);
            }
        }

        protected T CurrentValue
        {
            get => Value;
            set
            {
                var hasChanged = !EqualityComparer<T>.Default.Equals(value, Value);
                if (hasChanged)
                {
                    Value = value;
                    _ = ValueChanged.InvokeAsync(value);
                }
            }
        }

        // Private Methods
        private string GetClass()
        {
            return InputType switch
            {
                InputType.Checkbox => "form-check-input",
                InputType.Radio => "form-check-input",
                InputType.File => "form-control-file",
                InputType.Range => "form-control-range",
                _ => IsPlaintext ? "form-control-plaintext" : "form-control"
            };
        }

        private int BuildCheckbox(RenderTreeBuilder builder, int index)
        {
            if (BSLabel != null)
            {
                if (CurrentValue == null)
                    BSLabel.IsActive = false;
                else if (CurrentValue.GetType() == typeof(bool))
                    BSLabel.IsActive = (bool)(object)CurrentValue;
                else
                    BSLabel.IsActive = true;
            }
            builder.AddAttribute(index++, "checked", BindConverter.FormatValue(CurrentValue));
            builder.AddAttribute(index++, "onclick", EventCallback.Factory.Create(this, OnClick));
            // Kept for rollback if needed remove after next release
            //    if (InputType == InputType.Checkbox)
            //{
            //    if(typeof(T) == typeof(string))
            //    {
            //        Value = ((string)(object)Value).ToLowerInvariant() != "false" ? (T)(object)"true" : (T)(object)"false";
            //        if (BSLabel != null)
            //        {
            //            if (Convert.ToBoolean(Value, CultureInfo.InvariantCulture))
            //                BSLabel.IsActive = true;
            //            else
            //                BSLabel.IsActive = false;
            //        }
            //    }
            //    builder.AddAttribute(9, "checked", Convert.ToBoolean(Value, CultureInfo.InvariantCulture));
            //    builder.AddAttribute(10, "onclick", EventCallback.Factory.Create(this, OnClick));

            return index;
        }

        private int BuildRadio(RenderTreeBuilder builder, int index)
        {
            if (RadioValue != null)
            {
                if (RadioValue.Equals(Value))
                {
                    if (BSLabel != null)
                        BSLabel.IsActive = true;
                    builder.AddAttribute(index++, "checked", true);
                    builder.AddAttribute(index++, "onclick", EventCallback.Factory.Create(this, OnClick));
                }
                else
                {
                    if (BSLabel != null)
                        BSLabel.IsActive = false;
                    builder.AddAttribute(index++, "checked", false);
                    builder.AddAttribute(index++, "onclick", EventCallback.Factory.Create(this, OnClick));
                }
            }
            return index;
        }

        private int BuildValidation(RenderTreeBuilder builder, int index)
        {
            if (ValidateOnInput != true)
            {
                builder.AddAttribute(index++, "onchange", EventCallback.Factory.CreateBinder<string>(this, OnChange, CurrentValueAsString));
            }
            else
            {
                builder.AddAttribute(index++, "oninput", EventCallback.Factory.CreateBinder<string>(this, OnInput, CurrentValueAsString));
            }

            return index;
        }
    }
}
