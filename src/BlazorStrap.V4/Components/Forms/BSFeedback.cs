using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Web;

namespace BlazorStrap.V4
{
    public class BSFeedback<TValue> : BSFeedbackBase<TValue>
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder()
                .AddClass("valid-tooltip", IsTooltip && IsValid)
                .AddClass("valid-feedback", !IsTooltip && IsValid)
                .AddClass("invalid-tooltip", IsTooltip && IsInvalid)
                .AddClass("invalid-feedback", !IsTooltip && IsInvalid)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (EditContext != null)
            {
                if (ActualInvalidMessage == null)
                {
                    var first = true;
                    foreach (var message in EditContext.GetValidationMessages(FieldIdentifier))
                    {
                        if (first)
                        {
                            ActualInvalidMessage = message;
                            first = false;
                        }
                        else
                        {
                            ActualInvalidMessage += $"\n{message}";
                        }
                    }
                }
            }

            var content = IsInvalid ? ActualInvalidMessage : IsValid ? ValidMessage : null;
            if (content == null) return;
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", ClassBuilder);
            builder.AddMultipleAttributes(2, Attributes);
            builder.AddContent(3, (MarkupString)HttpUtility.HtmlEncode(content).Replace("\n", "<br/>"));
            builder.CloseElement();
        }
    }
}
