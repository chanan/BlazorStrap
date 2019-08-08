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

namespace BlazorStrap
{
    public class BSInput<T> : InputBase<T>
    {
        private bool Clean = true;
        private bool Touched = false;
        // [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        [CascadingParameter] EditContext MyEditContext { get; set; }
        [CascadingParameter] BSForm Parent { get; set; }
        protected string classname =>
        new CssBuilder()
           .AddClass($"form-control-{Size.ToDescriptionString()}", Size != Size.None)
           .AddClass("is-valid", IsValid)
           .AddClass("is-invalid", IsInvalid)
           .AddClass("is-valid", Regex.IsMatch(GetErrorCount(), @"\bvalid\b") && Touched && !Parent.UserValidation )
           .AddClass("is-invalid", Regex.IsMatch(GetErrorCount(), @"\binvalid\b") && !Parent.UserValidation)
           .AddClass(GetClass())
           .AddClass(Class)
         .Build();
        
        protected string GetErrorCount()
        {
            if(Clean)
            {
                Clean = false;
                return "";
            }
            return MyEditContext.FieldClass(_fieldIdentifier).ToLower();
        }
        protected string Tag => InputType switch
        {
            InputType.Select => "select",
            InputType.TextArea => "textarea",
            _ => "input"
        };
        private FieldIdentifier _fieldIdentifier;

        [Parameter] protected InputType InputType { get; set; } = InputType.Text;
        [Parameter] protected Size Size { get; set; } = Size.None;
        [Parameter] protected string InputValue { get; set; }
        [Parameter] public EventCallback<string> InputValueChanged { get; set; }
        [Parameter] protected bool IsReadonly { get; set; }
        [Parameter] protected bool IsPlaintext { get; set; }
        [Parameter] protected bool IsDisabled { get; set; }
        [Parameter] protected bool IsChecked { get; set; }
        [Parameter] protected bool IsValid { get; set; }
        [Parameter] protected bool IsInvalid { get; set; }
        [Parameter] protected bool IsMultipleSelect { get; set; }
        [Parameter] protected int? SelectSize { get; set; }
        [Parameter] protected int? SelectedIndex { get; set; }
        [Parameter] protected bool ValidateOnChange { get; set; }

            // [Parameter] protected string Class { get; set; }
            [Parameter] protected RenderFragment ChildContent { get; set; }

        protected string Type => InputType.ToDescriptionString();


        protected override void OnInit()
        {
            MyEditContext.OnValidationRequested += MyEditContext_OnValidationRequested;
            //Preview 7 workaround
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

        protected void onchange(UIChangeEventArgs e)
        {
            if(ValidateOnChange)
            {
               Invoke(() => MyEditContext.Validate());
               StateHasChanged();
            }
            CurrentValueAsString = e.Value.ToString();
            InputValueChanged.InvokeAsync((string)e.Value);
            InputValue = (string)e.Value;
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
                builder.AddAttribute(10, "onchange", onchange);
                builder.AddAttribute(11, "onfocus", () => { Touched = true; StateHasChanged(); });
                builder.AddAttribute(12, "ChildContent", ChildContent);
                builder.CloseElement();
        }

        public void ForceValidate()
        {
            Invoke(() => MyEditContext.Validate());
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

            throw new InvalidOperationException($"{GetType()} does not support the type '{typeof(T)}'.");
        }
    }
}
