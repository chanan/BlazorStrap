using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSForm : ComponentBase 
    {
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
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
