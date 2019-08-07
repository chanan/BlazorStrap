using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorStrap
{
    public class BSForm : EditForm
    {

        //[Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder()
            .AddClass("form-inline", IsInline)
            .AddClass(Class)
        .Build();

        [Parameter] protected bool IsInline { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected bool ValidateOnChange { get; set; }
        private RenderFragment Form { get; set; }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            Form = formBuilder =>
            {
                formBuilder.OpenComponent<EditForm>(0);
                formBuilder.AddMultipleAttributes(1, AdditionalAttributes);
                formBuilder.AddAttribute(2, "class", classname);
                formBuilder.AddAttribute(3, "EditContext", EditContext);
                formBuilder.AddAttribute(4, "Model", Model);
                formBuilder.AddAttribute(5, "OnSubmit", OnSubmit);
                formBuilder.AddAttribute(6, "OnValidSubmit", OnValidSubmit);
                formBuilder.AddAttribute(7, "OnInvalidSubmit", OnInvalidSubmit);
                formBuilder.AddAttribute(8, "ChildContent", ChildContent);
                formBuilder.CloseComponent();
            };
            builder.OpenComponent<CascadingValue<bool>>(0);
            builder.AddAttribute(1, "Value", ValidateOnChange);
            builder.AddAttribute(2, "ChildContent", Form);
            builder.CloseComponent();
        }

        //   [Parameter] protected RenderFragment MyChildContent { get; set; }


        /*async Task InternalOnSubmit(object e)
        {
            if(OnSubmit.HasDelegate)
            {
                await OnSubmit.InvokeAsync(EventCallback.Empty);
            }
        }*/
    }
  
}
