using BlazorComponentUtilities;
using BlazorStrap.Util;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public class BSInput<T> : InputBase<T>
    {
        // Constants
        private string _dateFormat = "yyyy-MM-dd";

        // Private variables
        private bool _clean = true;
        private bool _touched = false;


        // protected variables
        protected string Classname =>
            new CssBuilder()
               .AddClass($"form-control-{Size.ToDescriptionString()}", Size != Size.None)
               .AddClass("is-valid", GenerateIsValid())
               .AddClass("is-invalid", GenerateIsInvalid())
               .AddClass(GetClass())
               .AddClass(Class)
             .Build();

        protected string Tag => InputType switch
        {
            InputType.Select => "select",
            InputType.TextArea => "textarea",
            _ => "input"
        };
        protected string Type => InputType.ToDescriptionString();
        protected ElementReference ElementReference;

        //Dependency Injection
        [Inject] protected BlazorStrapInterop BlazorStrapInterop { get; set; }

        // Parameters
        [Parameter] public EventCallback<FocusEventArgs> OnBlur { get; set; }
        [Parameter] public EventCallback<FocusEventArgs> OnFocus { get; set; }
        [Parameter] public InputType InputType { get; set; } = InputType.Text;
        [Parameter] public Size Size { get; set; } = Size.None;
        [Parameter] public string MaxDate { get; set; } = "9999-12-31";
        [Parameter] public virtual T RadioValue { get; set; }
        [Parameter] public virtual T CheckValue { get; set; }
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
        [Parameter] public bool ValidateOnInput { get; set; } = false;
        [Parameter] public bool ValidateOnBlur { get; set; } = true;
        [Parameter] public string Class { get; set; }
        [Parameter] public int DebounceInterval { get; set; } = 500;
        [Parameter] public RenderFragment ChildContent { get; set; }

        // Cascading Parameters
        [CascadingParameter] protected EditContext MyEditContext { get; set; }
        [CascadingParameter] protected BSForm Parent { get; set; }
        [CascadingParameter] public BSLabel BSLabel { get; set; }

        // Overrides
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }
        //Consider getting rid of BSBasicInput
        //public override Task SetParametersAsync(ParameterView parameters)
        //{
        //    parameters.SetParameterProperties(this);
        //    if (MyEditContext == null)
        //    {
        //        MyEditContext = new EditContext(new object());
        //        var basecontext = this.GetType().BaseType.GetProperty("CascadedEditContext", BindingFlags.NonPublic | BindingFlags.Instance);
        //        basecontext.SetValue(this, MyEditContext);
        //    }
        //    else
        //    {
        //        base.EditContext = null;
        //    }
        //    return base.SetParametersAsync(parameters);

        //}

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
            }
            else if (InputType == InputType.Radio)
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
            else
            {
                builder.AddAttribute(9, "value", BindConverter.FormatValue(CurrentValueAsString));
                if (ValidateOnInput)
                {
                    builder.AddAttribute(10, "oninput", EventCallback.Factory.CreateBinder<string>(this, OnInput, CurrentValueAsString));
                }
                else
                {
                    builder.AddAttribute(10, "onchange", EventCallback.Factory.CreateBinder<string>(this, OnChange, CurrentValueAsString));
                }

                if (InputType == InputType.Date && !String.IsNullOrEmpty(MaxDate))
                {
                    builder.AddAttribute(11, "max", MaxDate);
                }
            }
            builder.AddAttribute(12, "onfocus", EventCallback.Factory.Create(this, (e) =>
            {
                OnFocus.InvokeAsync(e);
            }));

            builder.AddAttribute(12, "onblur", EventCallback.Factory.Create(this, (FocusEventArgs e) =>
            {

                if (ValidateOnBlur)
                {
                    ValidateField(FieldIdentifier);
                }
                OnBlur.InvokeAsync(e);
            }));

            builder.AddContent(13, ChildContent);
            builder.AddElementReferenceCapture(14, er => ElementReference = er);
            builder.CloseElement();
        }

        protected override void OnInitialized()
        {
            MyEditContext.OnValidationRequested += MyEditContext_OnValidationRequested;
            //Preview 7 workaround
            if (Parent != null)
                Parent.FormIsReady(MyEditContext);

            if (InputType == InputType.Time)
                _dateFormat = "HH:mm:ss.fff";
        }

        protected override string FormatValueAsString(T value)
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
                _ => base.FormatValueAsString(value),
            };
        }

        protected override bool TryParseValueFromString(string value, out T result, out string validationErrorMessage)
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

            if (type.GenericTypeArguments.Length > 0 && type.GenericTypeArguments[0].IsEnum)
            {
                try
                {
                    result = (T)Enum.Parse(type.GenericTypeArguments[0].UnderlyingSystemType, value);
                    validationErrorMessage = null;
                    boolToReturn = true;
                }
                catch (ArgumentException)
                {
                    result = default;
                    validationErrorMessage = $"The {FieldIdentifier.FieldName} field is not valid.";
                    boolToReturn = false;
                }
            }

            return boolToReturn != null
                ? boolToReturn.Value
                : throw new InvalidOperationException($"{GetType()} does not support the type '{typeof(T)}'.");
        }

        // public methods
        public void ForceValidate()
        {
            InvokeAsync(() => MyEditContext.Validate());
            StateHasChanged();
        }

        public ValueTask<object> Focus() => BlazorStrapInterop.FocusElement(ElementReference);

        // Protected Methods
        protected bool HasValidationErrors()
        {
            if (_clean || MyEditContext == null)
            {
                _clean = false;
                return false;
            }
            return MyEditContext.GetValidationMessages(base.FieldIdentifier).Any();
        }

        protected void OnClick(MouseEventArgs e)
        {
            if (InputType == InputType.Radio)
            {
                Value = (T)(object)(RadioValue);
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
                        }
                        else
                        {
                            Value = CheckValue;
                        }
                    }
                    else
                    {
                        if (Value == null)
                        {
                            Value = (T)(object)true;
                        }
                        else
                        {
                            Value = (T)(object)(((bool)(object)Value == true) ? false : true);
                        }
                    }
                }
                else
                {
                    if (Value == null)
                    {
                        Type underlyingType = Nullable.GetUnderlyingType(typeof(T));
                        // T is a Nullable<>
                        if (underlyingType != null)
                        {
                            Value = (T)GetDefault(underlyingType);
                        }
                    }
                    if (Value != null && Value is bool)
                    {
                        var tmp = (bool)(object)Value;
                        Value = (T)(object)(!tmp);
                    }
                }
                ValueChanged.InvokeAsync(Value);
            }
        }
        protected void OnInput(string e)
        {
            RateLimitingExceptionForObject.Debounce(e, DebounceInterval, (CurrentValueAsString) => {
                InvokeAsync(() => OnChange(e));
            });
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

        // Private Methods
        private bool GenerateIsValid()
        {
            var isValid = _touched && (!Parent?.UserValidation ?? false) && !HasValidationErrors();

            return IsValid != isValid ? isValid : IsValid;
        }

        private bool GenerateIsInvalid()
        {
            var isInvalid = (!Parent?.UserValidation ?? false) && HasValidationErrors();

            return IsInvalid != isInvalid ? isInvalid : IsInvalid;
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

        private bool TryParseDateTime(string value, out T result)
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

        private bool TryParseDateTimeOffset(string value, out T result)
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
            OnFieldChanged?.DynamicInvoke(new object[] { EditContext, new FieldChangedEventArgs(fieldIdentifier) });
            StateHasChanged();
        }

        private object GetDefault(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
