using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap
{
    public class BSForm : EditForm
    {
        private string? ClassBuilder => new CssBuilder("form-control")
            .AddClass("form-inline", IsInline)
            .AddClass(Class)
            .Build().ToNullString();
        [Parameter] public bool ValidateOnInit { get; set; }
        [Parameter] public bool IsInline { get; set; }
        [Parameter] public string? Class { get; set; }

        private RenderFragment? Form { get; set; }
        private EditContext? MyEditContext { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Model == null && EditContext == null)
            {
                Form = formBuilder =>
                {
                    formBuilder.OpenComponent<EditForm>(0);
                    formBuilder.AddMultipleAttributes(1, AdditionalAttributes);
                    formBuilder.AddAttribute(2, "class", ClassBuilder);
                    formBuilder.AddAttribute(3, "ChildContent", ChildContent);
                    formBuilder.CloseComponent();
                };

                builder.OpenComponent<CascadingValue<BSForm>>(3);
                builder.AddAttribute(4, "IsFixed", true);
                builder.AddAttribute(5, "Value", this);
                builder.AddAttribute(6, "ChildContent", Form);
                builder.CloseComponent();
                return;
            }

            Form = formBuilder =>
            {
                formBuilder.OpenComponent<EditForm>(0);
                formBuilder.AddMultipleAttributes(1, AdditionalAttributes);
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
                formBuilder.AddAttribute(7, "ChildContent", ChildContent);
                formBuilder.CloseComponent();
            };

            builder.OpenComponent<CascadingValue<BSForm>>(3);
            builder.AddAttribute(4, "IsFixed", true);
            builder.AddAttribute(5, "Value", this);
            builder.AddAttribute(6, "ChildContent", Form);
            builder.CloseComponent();
        }

        public void FormIsReady(EditContext e)
        {
            MyEditContext = e;
            if (ValidateOnInit)
            {
                ForceValidate();
            }
        }

        private void ForceValidate()
        {
            InvokeAsync(() => MyEditContext?.Validate());
            StateHasChanged();
        }
    }
}