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
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Inject] protected BlazorStrapInterop BlazorStrapInterop { get; set; }
        [CascadingParameter] protected EditContext MyEditContext { get; set; }

        private const string _dateFormat = "yyyy-MM-dd";
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

        private FieldIdentifier _fieldIdentifier { get; set; }

        [CascadingParameter] public BSLabel BSLabel { get; set; }
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

        // [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        protected ElementReference ElementReference;

        protected string Type => InputType.ToDescriptionString();

        protected override void OnParametersSet()
        {
            if (For != null)
            {
                _fieldIdentifier = FieldIdentifier.Create(For);
            }
        }
        
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

        protected void OnClick(MouseEventArgs e)
        {
            if (InputType == InputType.Radio)
            {
                Value = (T)(object)(RadioValue);
                ValueChanged.InvokeAsync(Value);
            }
            else
            {
                if (typeof(T) != typeof(bool))
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
                }
                else
                {
                    var tmp = (bool)(object)Value;
                    Value = (T)(object)(!tmp);
                }
                ValueChanged.InvokeAsync(Value);
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder?.OpenElement(0, Tag);
            builder.AddMultipleAttributes(1, UnknownParameters);
            builder.AddAttribute(2, "class", Classname);
            builder.AddAttribute(3, "type", Type);
            builder.AddAttribute(4, "readonly", IsReadonly);
            builder.AddAttribute(5, "disabled", IsDisabled);
            builder.AddAttribute(6, "multiple", IsMultipleSelect);
            builder.AddAttribute(7, "size", SelectSize);
            builder.AddAttribute(8, "selectedIndex", SelectedIndex);

            if (InputType == InputType.Checkbox)
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
                builder.AddAttribute(9, "checked", BindConverter.FormatValue(CurrentValue));
                builder.AddAttribute(10, "onclick", EventCallback.Factory.Create(this, OnClick));
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
            }
            else if(InputType == InputType.Radio)
            {
                if (RadioValue != null)
                {
                    if (RadioValue.Equals(Value))
                    {
                        if (BSLabel != null)
                            BSLabel.IsActive = true;
                        builder.AddAttribute(9, "checked", true);
                        builder.AddAttribute(10, "onclick", EventCallback.Factory.Create(this, OnClick));
                    }
                    else
                    {
                        if (BSLabel != null)
                            BSLabel.IsActive = false;
                        builder.AddAttribute(9, "checked", false);
                        builder.AddAttribute(10, "onclick", EventCallback.Factory.Create(this, OnClick));
                    }
                }
            }
            else
            {
                builder.AddAttribute(9, "value", BindConverter.FormatValue(CurrentValueAsString));
                if(ValidateOnInput != true)
                {
                    builder.AddAttribute(11, "onchange", EventCallback.Factory.CreateBinder<string>(this, OnChange, CurrentValueAsString));
                }
                else
                {
                    builder.AddAttribute(11, "oninput", EventCallback.Factory.CreateBinder<string>(this, OnInput, CurrentValueAsString));
                }
                

                if (InputType == InputType.Date && !String.IsNullOrEmpty(MaxDate))
                {
                    builder.AddAttribute(12, "max", MaxDate);
                }
            }
            builder.AddContent(13, ChildContent);
            builder.AddElementReferenceCapture(14, er => ElementReference = er);
            builder.CloseElement();
        }

        public void ForceValidate()
        {
            MyEditContext?.Validate();
            StateHasChanged();
        }

        protected string FormatValueAsString(T value)
        {
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
            Type type = typeof(T);
            if (typeof(T) == typeof(string))
            {
                result = (T)(object)value;
                validationErrorMessage = null;
                return true;
            }
            else if (value == null && (Nullable.GetUnderlyingType(type) != null))
            {
                result = (T)(object)default(T);
                validationErrorMessage = null;
                return true;
            }
            else if (value?.Length == 0 && typeof(DateTime) != typeof(T) && typeof(DateTimeOffset) != typeof(T))
            {
                result = (T)(object)default(T);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(T).IsEnum)
            {
                // There's no non-generic Enum.TryParse (https://github.com/dotnet/corefx/issues/692)
                try
                {
                    result = (T)Enum.Parse(typeof(T), value);
                    validationErrorMessage = null;
                    return true;
                }
                catch (ArgumentException)
                {
                    result = default;
                    validationErrorMessage = $"The {type.Name} field is not valid.";
                    return false;
                }
            }
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(int?))
            {
                result = (T)(object)Convert.ToInt32(value, CultureInfo.InvariantCulture);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(T) == typeof(bool))
            {
                if (InputType != InputType.Select)
                {
                    result = (T)(object)false;
                    validationErrorMessage = string.Format(CultureInfo.InvariantCulture, "The bool valued must be used with select, checkboxes, or radios.");
                    return false;
                }
                try
                {
                    if (value.ToString().ToLowerInvariant() == "false")
                    {
                        result = (T)(object)false;
                        validationErrorMessage = null;
                        return true;
                    }
                    else if (value.ToString().ToLowerInvariant() == "true")
                    {
                        result = (T)(object)true;
                        validationErrorMessage = null;
                        return true;
                    }
                    else
                    {
                        result = (T)(object)false;
                        validationErrorMessage = string.Format(CultureInfo.InvariantCulture, "The {0} field must be a bool of true or false.");
                        return false;
                    }
                }
                catch
                {
                    result = (T)(object)false;
                    validationErrorMessage = string.Format(CultureInfo.InvariantCulture, "The {0} field must be a bool of true or false.");
                    return false;
                }
            }
            else if (typeof(T) == typeof(long) || typeof(T) == typeof(long?))
            {
                result = (T)(object)Convert.ToInt64(value, CultureInfo.InvariantCulture);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(T) == typeof(double) || typeof(T) == typeof(double?))
            {
                result = (T)(object)double.Parse(value, CultureInfo.InvariantCulture);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(T) == typeof(decimal) || typeof(T) == typeof(decimal?))
            {
                result = (T)(object)decimal.Parse(value, CultureInfo.InvariantCulture);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(T) == typeof(Guid) || typeof(T) == typeof(Guid?))
            {
                try
                {
                    result = (T)(object)Guid.Parse(value);
                    validationErrorMessage = null;
                }
                catch
                {
                    result = (T)(object) new Guid();
                    validationErrorMessage = "Invalid Guid format";
                }
                
                return true;
            }
            else if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime?))
            {
                if (TryParseDateTime(value, out result))
                {
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    validationErrorMessage = string.Format(CultureInfo.InvariantCulture, "The {0} field must be a date.", type.Name);
                    return false;
                }
            }
            else if (typeof(T) == typeof(DateTimeOffset) || typeof(T) == typeof(DateTimeOffset?))
            {
                if (TryParseDateTimeOffset(value, out result))
                {
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    validationErrorMessage = string.Format(CultureInfo.InvariantCulture, "The {0} field must be a date.", type.Name);
                    return false;
                }
            }
            throw new InvalidOperationException($"{GetType()} does not support the type '{typeof(T)}'.");
        }

        private static bool TryParseDateTime(string value, out T result)
        {
            var success = BindConverter.TryConvertToDateTime(value, CultureInfo.InvariantCulture, _dateFormat, out DateTime parsedValue);
            if (success)
            {
                result = (T)(object)parsedValue;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        private static bool TryParseDateTimeOffset(string value, out T result)
        {
            var success = BindConverter.TryConvertToDateTimeOffset(value, CultureInfo.InvariantCulture, _dateFormat, out DateTimeOffset parsedValue);
            if (success)
            {
                result = (T)(object)parsedValue;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
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

        public ValueTask<object> Focus() => BlazorStrapInterop.FocusElement(ElementReference);
    }
}
