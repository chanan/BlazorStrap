using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSInput : BootstrapComponentBase
    {
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

        [Parameter] protected InputType InputType { get; set; } = InputType.Text;
        [Parameter] protected Size Size { get; set; } = Size.None;
        [Parameter] protected bool IsReadonly { get; set; }
        [Parameter] protected bool IsPlaintext { get; set; }
        [Parameter] protected bool IsDisabled { get; set; }
        [Parameter] protected bool IsChecked { get; set; }
        [Parameter] protected bool IsValid { get; set; }
        [Parameter] protected bool IsInvalid { get; set; }
        [Parameter] protected bool IsMultipleSelect { get; set; }
        [Parameter] protected int? SelectSize { get; set; }
        [Parameter] protected int? SelectedIndex { get; set; }
        [Parameter] protected virtual string Value { get; set; }
        [Parameter] protected virtual EventCallback<string> ValueChanged { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }

        protected string Type => InputType.ToDescriptionString();

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
    }
}
