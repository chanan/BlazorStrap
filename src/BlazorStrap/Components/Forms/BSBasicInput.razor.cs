using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components.RenderTree;

namespace BlazorStrap
{
    public class BSBasicInput : ComponentBase
    {
         [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        [CascadingParameter] EditContext MyEditContext { get; set; }
        protected string classname =>
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
        private FieldIdentifier _fieldIdentifier;

        [Parameter] protected Expression<Func<object>> For { get; set; }
        [Parameter] protected InputType InputType { get; set; } = InputType.Text;
        [Parameter] protected Size Size { get; set; } = Size.None;
        [Parameter] protected string Value { get; set; }
        [Parameter] public EventCallback<string> ValueChanged { get; set; }
        [Parameter] protected bool IsReadonly { get; set; }
        [Parameter] protected bool IsPlaintext { get; set; }
        [Parameter] protected bool IsDisabled { get; set; }
        [Parameter] protected bool IsChecked { get; set; }
        [Parameter] protected bool IsValid { get; set; }
        [Parameter] protected bool IsInvalid { get; set; }
        [Parameter] protected bool IsMultipleSelect { get; set; }
        [Parameter] protected int? SelectSize { get; set; }
        [Parameter] protected int? SelectedIndex { get; set; }
        [Parameter] protected string Class { get; set; }

        // [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }

        protected string Type => InputType.ToDescriptionString();

        protected override void OnParametersSet()
        {
            if (For != null)
            {
                _fieldIdentifier = FieldIdentifier.Create(For);
            }
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
            ValueChanged.InvokeAsync((string)e.Value);
            Value = (string)e.Value;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, Tag);
            builder.AddMultipleAttributes(1, UnknownParameters);
            builder.AddAttribute(2, "class", classname);
            builder.AddAttribute(3, "type", Type);
            builder.AddAttribute(4, "readonly", IsReadonly);
            builder.AddAttribute(5, "disabled", IsDisabled);
            builder.AddAttribute(6, "multiple", IsMultipleSelect);
            builder.AddAttribute(7, "size", SelectSize);
            builder.AddAttribute(8, "selectedIndex", SelectedIndex);
            builder.AddAttribute(8, "value", Value);
            builder.AddAttribute(9, "onchange", onchange);
            builder.AddAttribute(10, "ChildContent", ChildContent);
            builder.CloseElement();

        }

        public void ForceValidate()
        {
            MyEditContext?.Validate();
            StateHasChanged();
        }
    }
}
