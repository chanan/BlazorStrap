using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap.V5
{
    public class BSInputCheckbox<TValue> : BSInputCheckboxBase<TValue>
    {
        /// <summary>
        /// Size of input.
        /// </summary>
        [Parameter] public Size Size { get; set; }

        /// <summary>
        /// Display as toggle buttons.
        /// </summary>
        [Parameter] public bool IsToggle { get => _isToggle; set => _isToggle = value; }
        protected override string? ContainerClassBuilder => new CssBuilder()
         .AddClass(ContainerClass).Build().ToNullString();
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder()
                .AddClass(ContainerClassBuilder, !string.IsNullOrEmpty(ContainerClassBuilder))
                .AddClass("form-check-input", !IsToggle && !RemoveDefaultClass)
                .AddClass("btn-check", IsToggle && !RemoveDefaultClass)
                .AddClass(ValidClass, IsValid)
                .AddClass(InvalidClass, IsInvalid)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();

        protected override string? ToggleClassBuilder => new CssBuilder("btn")
                .AddClass($"btn-outline-{Color.NameToLower()}", IsOutlined)
                .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
                .AddClass($"btn-{Color.NameToLower()}", Color != BSColor.Default && !IsOutlined)
                .Build().ToNullString();

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var id = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).ToString().Replace("==.", "");
            if (Helper?.Id != null )
            {
                id = Helper.Id;
            }
            builder.OpenElement(0, "input");
            builder.AddAttribute(1, "type", InputType);
            builder.AddAttribute(2, "class", ClassBuilder);
            builder.AddAttribute(3, "value", BindConverter.FormatValue(CurrentValue));
            builder.AddAttribute(4, "onclick", RadioOnClickEvent);
            builder.AddAttribute(7, "onblur", OnBlurEvent);
            builder.AddAttribute(8, "onfocus", OnFocusEvent);
            builder.AddAttribute(9, "checked", Checked());
            if(Helper?.Id != null && !IsToggle)
            {
                builder.AddAttribute(10, "id", Helper.Id);
            }
            builder.AddAttribute(11, "disabled", IsDisabled);
            if (IsToggle)
                builder.AddAttribute(12, "id", id);
            builder.AddMultipleAttributes(13, AdditionalAttributes);
            builder.AddElementReferenceCapture(14, elReference => Element = elReference);
            builder.CloseElement();
            if (IsToggle)
            {
                builder.OpenElement(15, "label");
                builder.AddAttribute(16, "for", id);
                builder.AddAttribute(17, "class", ToggleClassBuilder);
                builder.AddContent(18, @ChildContent);
                builder.CloseElement();
            }
        }
    }
}
