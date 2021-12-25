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
        protected string Tag => InputType switch
        {
            InputType.Select => "select",
            InputType.TextArea => "textarea",
            _ => "input"
        };
        [DisallowNull] public ElementReference? Element { get; protected set; }
        [Parameter] public bool IsPlainText { get; set; }
        [Parameter] public bool IsReadonly { get; set; }
        [Parameter] public InputType InputType { get; set; } = InputType.Text;
        [Parameter] public Size Size { get; set; }

        private string? ClassBuilder => new CssBuilder("form-control")
            .AddClass(ValidClass, IsValid)
            .AddClass(InvalidClass, IsInvalid)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "input");
            builder.AddAttribute(1, "type", InputType.ToDescriptionString());
            builder.AddAttribute(2, "class", ClassBuilder);
            builder.AddAttribute(3, "value", BindConverter.FormatValue(CurrentValue));
            builder.AddAttribute(4, "onchange", EventCallback.Factory.CreateBinder<string?>(this, OnChangeEvent, CurrentValueAsString));
            builder.AddAttribute(5, "oninput", EventCallback.Factory.CreateBinder<string?>(this, OnInputEvent, CurrentValueAsString));
            builder.AddAttribute(6, "onblur", OnBlurEvent);
            builder.AddAttribute(7, "onfocus", OnFocusEvent);
            builder.AddMultipleAttributes(8, AdditionalAttributes);
            builder.AddElementReferenceCapture(9, elReference => Element = elReference);
            builder.CloseElement();
        }

        protected override bool TryParseValueFromString(string? value, out T result, [NotNullWhen(false)] out string? validationErrorMessage)
        {
            return TryParseString<T>.ToValue(value, out result, out validationErrorMessage);
        }
    }
}