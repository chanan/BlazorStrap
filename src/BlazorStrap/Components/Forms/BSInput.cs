using BlazorComponentUtilities;
using BlazorStrap.Util;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace BlazorStrap
{
    public class BSInput<T> : InputBase<T>
    {
        private bool _clean = true;
        private bool _touched = false;

        // [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [CascadingParameter] protected EditContext MyEditContext { get; set; }

        [CascadingParameter] protected BSForm Parent { get; set; }

        protected string Classname =>
        new CssBuilder()
           .AddClass($"form-control-{Size.ToDescriptionString()}", Size != Size.None)
           .AddClass("is-valid", IsValid)
           .AddClass("is-invalid", IsInvalid)
           .AddClass("is-valid", _touched && (!Parent?.UserValidation ?? false) && !HasValidationErrors())
           .AddClass("is-invalid", (!Parent?.UserValidation ?? false) && HasValidationErrors())
           .AddClass(GetClass())
           .AddClass(Class)
         .Build();

        protected bool HasValidationErrors()
        {
            if (_clean || MyEditContext == null)
            {
                _clean = false;
                return false;
            }
            return MyEditContext.GetValidationMessages(base.FieldIdentifier).Any();
        }

        protected string Tag => InputType switch
        {
            InputType.Select => "select",
            InputType.TextArea => "textarea",
            _ => "input"
        };


        [Parameter] public InputType InputType { get; set; } = InputType.Text;
        [Parameter] public Size Size { get; set; } = Size.None;
        [Parameter] public bool IsReadonly { get; set; }
        [Parameter] public bool IsPlaintext { get; set; }
        [Parameter] public bool IsDisabled { get; set; }
        [Parameter] public bool IsChecked { get; set; }
        [Parameter] public bool IsValid { get; set; }
        [Parameter] public bool IsInvalid { get; set; }
        [Parameter] public bool IsMultipleSelect { get; set; }
        [Parameter] public int? SelectSize { get; set; }
        [Parameter] public int? SelectedIndex { get; set; }
        [Parameter] public bool ValidateOnChange { get; set; }
        [Parameter] public string Class { get; set; }

        // [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected string Type => InputType.ToDescriptionString();

        private const string _dateFormat = "yyyy-MM-dd";

        protected override void OnInitialized()
        {
            MyEditContext.OnValidationRequested += MyEditContext_OnValidationRequested;
            //Preview 7 workaround
            if (Parent != null)
                Parent.FormIsReady(MyEditContext);
        }

        private void MyEditContext_OnValidationRequested(object sender, ValidationRequestedEventArgs e)
        {
            _touched = true;
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

        protected void OnClick(MouseEventArgs e)
        {
            var tmp = (bool)(object)Value;
            Value = (T)(object)(!tmp);
            ValueChanged.InvokeAsync(Value);
        }

        protected void OnChange(string e)
        {
            if (ValidateOnChange)
            {
                InvokeAsync(() => MyEditContext.Validate());
                StateHasChanged();
            }
            CurrentValueAsString = e;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder?.OpenElement(0, Tag);
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", Classname);
            builder.AddAttribute(3, "type", Type);
            builder.AddAttribute(4, "readonly", IsReadonly);
            builder.AddAttribute(5, "disabled", IsDisabled);
            builder.AddAttribute(6, "multiple", IsMultipleSelect);
            builder.AddAttribute(7, "size", SelectSize);
            builder.AddAttribute(8, "selectedIndex", SelectedIndex);
            if (InputType == InputType.Checkbox)
            {
                builder.AddAttribute(9, "checked", BindConverter.FormatValue(CurrentValue));
                builder.AddAttribute(10, "onclick", EventCallback.Factory.Create(this, OnClick));
            }
            else
            {
                builder.AddAttribute(9, "value", BindConverter.FormatValue(CurrentValueAsString));
                builder.AddAttribute(10, "onchange", EventCallback.Factory.CreateBinder<string>(this, OnChange, CurrentValueAsString));
            }

            builder.AddAttribute(11, "onblur", EventCallback.Factory.Create(this, () => { _touched = true; ValidateField(base.FieldIdentifier) ; }));
            builder.AddContent(12, ChildContent);
            builder.CloseElement();
        }

        protected override string FormatValueAsString(T value)
        {
            return value switch
            {
                null => null,
                int @int => BindConverter.FormatValue(@int, CultureInfo.InvariantCulture),
                long @long => BindConverter.FormatValue(@long, CultureInfo.InvariantCulture),
                float @float => BindConverter.FormatValue(@float, CultureInfo.InvariantCulture),
                double @double => BindConverter.FormatValue(@double, CultureInfo.InvariantCulture),
                decimal @decimal => BindConverter.FormatValue(@decimal, CultureInfo.InvariantCulture),
                DateTime dateTimeValue => BindConverter.FormatValue(dateTimeValue, _dateFormat, CultureInfo.InvariantCulture),
                DateTimeOffset dateTimeOffsetValue => BindConverter.FormatValue(dateTimeOffsetValue, _dateFormat, CultureInfo.InvariantCulture),
                _ => base.FormatValueAsString(value),
            };
        }

        public void ForceValidate()
        {
            InvokeAsync(() => MyEditContext.Validate());
            StateHasChanged();
        }

        protected override bool TryParseValueFromString(string value, out T result, out string validationErrorMessage)
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
                    validationErrorMessage = $"The {FieldIdentifier.FieldName} field is not valid.";
                    return false;
                }
            }
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(int?))
            {
                result = (T)(object)Convert.ToInt32(value, CultureInfo.InvariantCulture);
                validationErrorMessage = null;
                return true;
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
                }
                catch
                {
                    result = (T)(object)new Guid();
                    validationErrorMessage = "Invalid Guid format";
                }
                validationErrorMessage = null;
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
                    validationErrorMessage = string.Format(CultureInfo.InvariantCulture, "The {0} field must be a date.", FieldIdentifier.FieldName);
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
                    validationErrorMessage = string.Format(CultureInfo.InvariantCulture, "The {0} field must be a date.", FieldIdentifier.FieldName);
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
    
        private void ValidateField(FieldIdentifier fieldIdentifier)
        {
            var OnFieldChanged = (MulticastDelegate)MyEditContext.GetType().GetField("OnFieldChanged", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(MyEditContext);
            OnFieldChanged?.DynamicInvoke(new object[] { this, new FieldChangedEventArgs(fieldIdentifier) });
            StateHasChanged();
        }
    }
}
