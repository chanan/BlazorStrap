using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Forms;
using System.Timers;

namespace BlazorStrap
{
    public class BSForm : EditForm
    {

        protected string classname =>
        new CssBuilder()
            .AddClass("form-inline", IsInline)
            .AddClass(Class)
        .Build();
        [Parameter] public bool UserValidation { get; set; }
        [Parameter] public bool ValidateOnInit { get; set; }
        [Parameter] public bool IsInline { get; set; }
        [Parameter] public string Class { get; set; }
        
        private RenderFragment Form { get; set; }
        private EditContext MyEditContext { get; set; }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            Form = Formbuilder =>
            {
                Formbuilder.OpenComponent<EditForm>(0);
                Formbuilder.AddMultipleAttributes(1, AdditionalAttributes);
                Formbuilder.AddAttribute(2, "class", classname);
                Formbuilder.AddAttribute(3, "Model", Model);
                Formbuilder.AddAttribute(4, "EditContext", EditContext);
                Formbuilder.AddAttribute(5, "OnSubmit", OnSubmit);
                Formbuilder.AddAttribute(6, "OnValidSubmit", OnValidSubmit);
                Formbuilder.AddAttribute(7, "OnInvalidSubmit", OnInvalidSubmit);
                Formbuilder.AddAttribute(8, "ChildContent", ChildContent);
                Formbuilder.CloseComponent();
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
        public void ForceValidate()
        {
            InvokeAsync(() => MyEditContext?.Validate());
            StateHasChanged();
        }
    }
}
