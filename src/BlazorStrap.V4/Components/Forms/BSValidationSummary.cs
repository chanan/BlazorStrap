using BlazorStrap.Shared.Components.Forms;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap.V4
{
    public class BSValidationSummary : BSValidationSummaryBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (!DidSubmit) return;
            if (CurrentEditContext != null)
            {
                builder.OpenComponent(0, typeof(ValidationSummary));
                builder.CloseComponent();
            }

            if (ValidationMessages.Any(q => q.Value == false))
            {
                builder.OpenElement(1, "ul");
                builder.AddMultipleAttributes(1, AdditionalAttributes);
                builder.AddAttribute(2, "class", "validation-errors");
                foreach (var item in ValidationMessages.Where(q => q.Value == false))
                {
                    builder.OpenElement(2, "li");
                    builder.AddAttribute(3, "class", "validation-message");
                    builder.AddContent(4, item.Key);
                    builder.CloseElement();
                }
                builder.CloseElement();
            }
            if (ValidationMessages.Any(q => q.Value))
            {
                builder.OpenElement(1, "ul");
                builder.AddMultipleAttributes(1, AdditionalAttributes);
                builder.AddAttribute(2, "class", "validation-success");
                foreach (var item in ValidationMessages.Where(q => q.Value))
                {
                    builder.OpenElement(2, "li");
                    builder.AddAttribute(3, "class", "validation-message");
                    builder.AddContent(4, item.Key);
                    builder.CloseElement();
                }
                builder.CloseElement();
            }
        }
    }
}
