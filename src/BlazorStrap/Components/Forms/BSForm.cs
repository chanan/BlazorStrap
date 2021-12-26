using System.Reflection.Metadata;
using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap
{
    public class BSForm<TValue> : BlazorStrapBase
    {
        /// <summary>
        /// Align
        /// </summary>
        [Parameter] public Align Align { get; set; }
        [Parameter] public EditContext? EditContext { get; set; }
        /// <summary>
        /// Gutters
        /// </summary>
        [Parameter] public Gutters Gutters { get; set; }
        [Parameter] public Gutters HorizontalGutters { get; set; }
        [Parameter] public TValue IsBasic { get; set; }
        [Parameter] public bool IsFloating { get; set; }
        [Parameter] public bool IsRow { get; set; }
        /// <summary>
        /// Justify
        /// </summary>
        [Parameter] public Justify Justify { get; set; }
        [Parameter] public TValue Model { get; set; }
        [Parameter] public EventCallback<EditContext> OnInvalidSubmit { get; set; }
        [Parameter] public EventCallback<EditContext> OnSubmit { get; set; }
        [Parameter] public EventCallback<EditContext> OnValidSubmit { get; set; }
       // [Parameter] public bool ValidateOnInit { get; set; }

        /// <summary>
        /// Vertical Gutters
        /// </summary>
        [Parameter] public Gutters VerticalGutters { get; set; }

        private string? ClassBuilder => new CssBuilder()
            .AddClass("row", IsRow)
            .AddClass("form-floating", IsFloating)
            .AddClass($"g-{HorizontalGutters.ToIndex()}", HorizontalGutters != Gutters.Default && IsRow)
            .AddClass($"gx-{VerticalGutters.ToIndex()}", VerticalGutters != Gutters.Default && IsRow)
            .AddClass($"gy-{Gutters.ToIndex()}", Gutters != Gutters.Default && IsRow)
            .AddClass($"justify-content-{Justify.GetName<Justify>(Justify).ToLower()}", Justify != Justify.Default && IsRow)
            .AddClass($"align-items-{Align.GetName<Align>(Align).ToLower()}", Align != Align.Default && IsRow)
            .AddClass(Class)
            .Build().ToNullString();

        private RenderFragment<EditContext> EditFormChildContent { get; set; }

        private RenderFragment? Form { get; set; }

        // Is there even a good use for this?
        /*public void FormIsReady(EditContext e)
        {
            EditContext = e;
            if (ValidateOnInit)
            {
                ForceValidate();
            }
        }*/

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Model.Equals(default(TValue)) && EditContext == null)
            {
                builder.OpenElement(0, "form");
                builder.AddAttribute(1, "class", ClassBuilder);
                builder.AddMultipleAttributes(2, Attributes);
                builder.AddContent(0, ChildContent);
                builder.CloseElement();
                return;
            }
            EditFormChildContent = content => child =>
            {
                content = EditContext;
                child.AddContent(1, ChildContent);
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

                formBuilder.AddAttribute(4, "OnSubmit", OnSubmit);
                formBuilder.AddAttribute(5, "OnValidSubmit", OnValidSubmit);
                formBuilder.AddAttribute(6, "OnInvalidSubmit", OnInvalidSubmit);
                formBuilder.AddAttribute(7, "ChildContent", EditFormChildContent);
                formBuilder.CloseComponent();
            };

            builder.OpenComponent<CascadingValue<BSForm<TValue>>>(3);
            builder.AddAttribute(4, "IsFixed", true);
            builder.AddAttribute(5, "Value", this);
            builder.AddAttribute(6, "ChildContent", Form);
            builder.CloseComponent();
        }

        private void ForceValidate()
        {
            InvokeAsync(() => EditContext?.Validate());
            StateHasChanged();
        }
    }
}