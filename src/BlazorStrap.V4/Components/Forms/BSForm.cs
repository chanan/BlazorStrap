using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap.V4
{
    public class BSForm<TValue> : BSFormBase<TValue, Justify>
    {
        protected override string? ClassBuilder => new CssBuilder()
                .AddClass("row", IsRow)
                .AddClass("form-floating", IsFloating)
                .AddClass($"g-{Gutters.ToIndex()}", Gutters != Gutters.Default && IsRow)
                .AddClass($"gx-{HorizontalGutters.ToIndex()}", HorizontalGutters != Gutters.Default && IsRow)
                .AddClass($"gy-{VerticalGutters.ToIndex()}", VerticalGutters != Gutters.Default && IsRow)
                .AddClass($"justify-content-{Justify.NameToLower()}", Justify != Justify.Default && IsRow)
                .AddClass($"align-items-{Align.NameToLower()}", Align != Align.Default && IsRow)
                .AddClass(Class)
                .Build().ToNullString();

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Model != null && Model.Equals(default(TValue)) && EditContext == null)
            {
                builder.OpenElement(0, "form");
                builder.AddAttribute(1, "class", ClassBuilder);
                builder.AddMultipleAttributes(2, Attributes);
                builder.AddContent(3, ChildContent);
                builder.CloseElement();
                return;
            }
            EditFormChildContent = content =>
            {
                if (content == null) throw new ArgumentNullException(nameof(content));
                return child =>
                {
                    if (EditContext != null)
                        content = EditContext;
                    child.AddContent(1, ChildContent);
                };
            };

            Form = formBuilder =>
            {
                formBuilder.OpenComponent<EditForm>(0);
                formBuilder.AddMultipleAttributes(1, Attributes);
                formBuilder.AddAttribute(2, "class", ClassBuilder);
                if (EditContext != null)
                {
                    formBuilder.AddAttribute(3, "EditContext", EditContext);
                }
                else
                {
                    formBuilder.AddAttribute(3, "Model", Model);
                }
                if(OnSubmit.HasDelegate)
                    formBuilder.AddAttribute(4, "OnSubmit", EventCallback.Factory.Create<EditContext>(this, OnSubmitEvent));
                if(OnValidSubmit.HasDelegate)
                    formBuilder.AddAttribute(5, "OnValidSubmit", EventCallback.Factory.Create<EditContext>(this, OnValidSubmitEvent));
                formBuilder.AddAttribute(6, "OnInvalidSubmit", OnInvalidSubmit);
                if (OnReset.HasDelegate)
                {
                    formBuilder.AddAttribute(7, "onreset", EventCallback.Factory.Create(this, OnResetEvent));
                    formBuilder.AddEventPreventDefaultAttribute(8, "onreset", true);
                }
                formBuilder.AddAttribute(9, "ChildContent", EditFormChildContent);
                formBuilder.CloseComponent();
            };

            builder.OpenComponent<CascadingValue<BSForm<TValue>>>(3);
            builder.AddAttribute(4, "IsFixed", true);
            builder.AddAttribute(5, "Value", this);
            builder.AddAttribute(6, "ChildContent", Form);
            builder.CloseComponent();
        }
    }
}
