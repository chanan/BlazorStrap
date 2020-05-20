using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap
{
    public class BSForm : EditForm, IBSForm
    {
        protected string Classname =>
        new CssBuilder()
            .AddClass("form-inline", IsInline)
            .AddClass(Class)
        .Build();

        [Parameter] public bool UserValidation { get; set; }
        [Parameter] public bool ValidateOnInit { get; set; }
        [Parameter] public bool IsInline { get; set; }
        [Parameter] public string Class { get; set; }

        private RenderFragment _form { get; set; }
        protected EditContext MyEditContext { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Model == null && EditContext == null)
            {
                _form = Formbuilder =>
                {
                    Formbuilder.OpenComponent<EditForm>(0);
                    Formbuilder.AddMultipleAttributes(1, AdditionalAttributes);
                    Formbuilder.AddAttribute(2, "class", Classname);
                    Formbuilder.AddAttribute(3, "ChildContent", ChildContent);
                    Formbuilder.CloseComponent();
                };

                builder?.OpenComponent<CascadingValue<BSForm>>(3);
                builder.AddAttribute(4, "IsFixed", true);
                builder.AddAttribute(5, "Value", this);
                builder.AddAttribute(6, "ChildContent", _form);
                builder.CloseComponent();
                return;
            }
            _form = Formbuilder =>
            {
                Formbuilder.OpenComponent<EditForm>(0);
                Formbuilder.AddMultipleAttributes(1, AdditionalAttributes);
                Formbuilder.AddAttribute(2, "class", Classname);
                Formbuilder.AddAttribute(3, "Model", Model);
                Formbuilder.AddAttribute(4, "EditContext", EditContext);
                Formbuilder.AddAttribute(5, "OnSubmit", OnSubmit);
                Formbuilder.AddAttribute(6, "OnValidSubmit", OnValidSubmit);
                Formbuilder.AddAttribute(7, "OnInvalidSubmit", OnInvalidSubmit);
                Formbuilder.AddAttribute(8, "ChildContent", ChildContent);
                Formbuilder.CloseComponent();
            };

            builder?.OpenComponent<CascadingValue<BSForm>>(3);
            builder.AddAttribute(4, "IsFixed", true);
            builder.AddAttribute(5, "Value", this);
            builder.AddAttribute(6, "ChildContent", _form);
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

        public void ForceValidate()
        {
            InvokeAsync(() => MyEditContext?.Validate());
            StateHasChanged();
        }
    }
}
