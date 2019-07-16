using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSForm : BootstrapComponentBase 
    {
        protected string classname =>
        new CssBuilder()
            .AddClass("form-inline", IsInline)
            .AddClass(Class)
        .Build();

        [Parameter] protected bool IsInline { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [Parameter] protected EventCallback<UIEventArgs> OnSubmit { get; set; }

        /*async Task InternalOnSubmit(object e)
        {
            if(OnSubmit.HasDelegate)
            {
                await OnSubmit.InvokeAsync(EventCallback.Empty);
            }
        }*/
    }
}
