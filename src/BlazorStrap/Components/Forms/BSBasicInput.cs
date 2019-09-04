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
    public class BSBasicInput  : ComponentBase
    {
         [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
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

        [Parameter] public Expression<Func<object>> For { get; set; }
        [Parameter] public InputType InputType { get; set; } = InputType.Text;
        [Parameter] public Size Size { get; set; } = Size.None;
        [Parameter] public virtual string Value { get; set; }
        [Parameter] public virtual EventCallback<string> ValueChanged { get; set; }
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

        // [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

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
            ValueChanged.InvokeAsync(e.Value.ToString());
            Value = e.Value.ToString();
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
            builder.AddAttribute(9, "onchange", EventCallback.Factory.Create(this, onchange));
            builder.AddContent(10, ChildContent);
            builder.CloseElement();

        }

        public void ForceValidate()
        {
            MyEditContext?.Validate();
            StateHasChanged();
        }
    }
}
