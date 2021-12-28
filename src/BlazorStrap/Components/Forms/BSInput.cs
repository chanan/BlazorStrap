using System.Diagnostics.CodeAnalysis;
using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public class BSInput<T> : BSInputBase<T>
    {
        [Parameter] public InputType InputType { get; set; } = InputType.Text;
        [Parameter] public bool IsPlainText { get; set; }
        [Parameter] public bool IsReadonly { get; set; }
        [Parameter] public bool NoColorClass { get; set; }
        [Parameter] public Size InputSize { get; set; }
        [DisallowNull] public ElementReference? Element { get; protected set; }

        protected string Tag => InputType switch
        {
            InputType.Select => "select",
            InputType.TextArea => "textarea",
            _ => "input"
        };

        private string? ClassBuilder => new CssBuilder()
            .AddClass("form-range", InputType == InputType.Range)
            .AddClass("form-control", InputType != InputType.Select && InputType != InputType.Range)
            .AddClass("form-select", InputType == InputType.Select)
            .AddClass("form-control-color", InputType == InputType.Color && !NoColorClass)
            .AddClass("form-control-plaintext", IsPlainText)
            .AddClass($"form-control-{InputSize.ToDescriptionString()}",
                InputType != InputType.Select && InputSize != Size.None)
            .AddClass($"form-select-{InputSize.ToDescriptionString()}",
                InputType == InputType.Select && InputSize != Size.None)
            .AddClass(ValidClass, IsValid)
            .AddClass(InvalidClass, IsInvalid)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        protected override bool TryParseValueFromString(string? value, out T result,
            [NotNullWhen(false)] out string? validationErrorMessage)
        {
            return TryParseString<T>.ToValue(value ?? "", out result, out validationErrorMessage);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (InputType == InputType.Select && IsReadonly)
                IsDisabled = IsReadonly;
            builder.OpenElement(0, Tag);
            if (InputType != InputType.DataList)
                builder.AddAttribute(1, "type", InputType.ToDescriptionString());
            builder.AddAttribute(2, "class", ClassBuilder);
            builder.AddAttribute(3, "value", BindConverter.FormatValue(CurrentValue));
            builder.AddAttribute(4, "onchange",
                EventCallback.Factory.CreateBinder<string?>(this, OnChangeEvent, CurrentValueAsString));
            builder.AddAttribute(5, "oninput",
                EventCallback.Factory.CreateBinder<string?>(this, OnInputEvent, CurrentValueAsString));
            builder.AddAttribute(6, "onblur", OnBlurEvent);
            builder.AddAttribute(7, "onfocus", OnFocusEvent);
            builder.AddAttribute(8, "disabled", IsDisabled);
            if (Tag != "select")
                builder.AddAttribute(9, "readonly", IsReadonly);
            builder.AddMultipleAttributes(10, AdditionalAttributes);
            builder.AddElementReferenceCapture(11, elReference => Element = elReference);
            if (Tag == "select" || Tag == "textarea")
                builder.AddContent(10, @ChildContent);
            builder.CloseElement();
        }
    }
}