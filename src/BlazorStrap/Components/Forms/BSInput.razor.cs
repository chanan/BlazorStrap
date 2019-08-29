using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Text.RegularExpressions;
using System.Linq;

namespace BlazorStrap
{
    public class BSInput<T> : InputBase<T>
    {
        private bool Clean = true;
        private bool Touched = false;
        // [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [CascadingParameter] EditContext MyEditContext { get; set; }
        [CascadingParameter] BSForm Parent { get; set; }
        protected string classname =>
        new CssBuilder()
           .AddClass($"form-control-{Size.ToDescriptionString()}", Size != Size.None)
           .AddClass("is-valid", IsValid)
           .AddClass("is-invalid", IsInvalid)
           .AddClass("is-valid", Touched && (!Parent?.UserValidation ?? false) && !HasValidationErrors())
           .AddClass("is-invalid", (!Parent?.UserValidation ?? false) && HasValidationErrors())
           .AddClass(GetClass())
           .AddClass(Class)
         .Build();
        
        protected bool HasValidationErrors()
        {
            if(Clean || MyEditContext == null)
            {
                Clean = false;
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
        private FieldIdentifier _fieldIdentifier;

        [Parameter] public InputType InputType { get; set; } = InputType.Text;
        [Parameter] public Size Size { get; set; } = Size.None;
        [Parameter] public string InputValue { get; set; }
        [Parameter] public EventCallback<string> InputValueChanged { get; set; }
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


        protected override void OnInitialized()
        {
            MyEditContext.OnValidationRequested += MyEditContext_OnValidationRequested;
            //Preview 7 workaround
            if (Parent !=null)
               Parent.FormIsReady(MyEditContext);
        }

        private void MyEditContext_OnValidationRequested(object sender, ValidationRequestedEventArgs e)
        {
            Touched = true;
        }

        protected override void OnParametersSet()
        {
            _fieldIdentifier = FieldIdentifier.Create(ValueExpression);
        }
        private string GetClass() => this.InputType switch
        {
            InputType.Checkbox => "form-check-input",
            InputType.Radio => "form-check-input",
            InputType.File => "form-control-file",
            InputType.Range => "form-control-range",
            _ => IsPlaintext ? "form-control-plaintext" : "form-control"
        };

        protected void OnChange(string e)
        {
            if(ValidateOnChange)
            {
                InvokeAsync(() => MyEditContext.Validate());
                StateHasChanged();
            }
            CurrentValueAsString = e;
            InputValueChanged.InvokeAsync(e);
            InputValue = e;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
                builder.OpenElement(0, Tag);
                builder.AddMultipleAttributes(1, AdditionalAttributes);
                builder.AddAttribute(2, "class", classname);
                builder.AddAttribute(3, "type", Type);
                builder.AddAttribute(4, "readonly", IsReadonly);
                builder.AddAttribute(5, "disabled", IsDisabled);
                builder.AddAttribute(6, "multiple", IsMultipleSelect);
                builder.AddAttribute(7, "size", SelectSize);
                builder.AddAttribute(8, "selectedIndex", SelectedIndex);
                builder.AddAttribute(9, "value", Value);
                builder.AddAttribute(10, "onchange", EventCallback.Factory.CreateBinder<string>(this, OnChange, CurrentValueAsString));
                builder.AddAttribute(11, "onfocus", EventCallback.Factory.Create(this, () => { Touched = true; StateHasChanged(); }));
            builder.AddContent(12, ChildContent);
                builder.CloseElement();
        }

        public void ForceValidate()
        {
            InvokeAsync(() => MyEditContext.Validate());
            StateHasChanged();
        }

        protected override bool TryParseValueFromString(string value, out T result, out string validationErrorMessage)
        {
            if (typeof(T) == typeof(string))
            {
                result = (T)(object)value;
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
            else if(typeof(T) == typeof(int))
            {
                result = (T)(object)Convert.ToInt32(value);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(T) == typeof(long))
            {
                result = (T)(object)Convert.ToInt64(value);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(T) == typeof(double))
            {
                result = (T)(object)Convert.ToDouble(value);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(T) == typeof(Guid))
            {
                result = (T)(object)Guid.Parse(value);
                validationErrorMessage = null;
                return true;
            }
            throw new InvalidOperationException($"{GetType()} does not support the type '{typeof(T)}'.");
        }
    }
}
