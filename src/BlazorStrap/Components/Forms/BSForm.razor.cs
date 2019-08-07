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
       
        private RenderFragment Form { get; set; }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {

            builder.OpenComponent<EditForm>(0);
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", classname);
            builder.AddAttribute(3, "EditContext", EditContext);
            builder.AddAttribute(4, "Model", Model);
            builder.AddAttribute(5, "OnSubmit", OnSubmit);
            builder.AddAttribute(6, "OnValidSubmit", OnValidSubmit);
            builder.AddAttribute(7, "OnInvalidSubmit", OnInvalidSubmit);
            builder.AddAttribute(8, "ChildContent", ChildContent);
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
