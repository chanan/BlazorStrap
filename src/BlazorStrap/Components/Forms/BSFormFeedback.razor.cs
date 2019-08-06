using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Linq.Expressions;

namespace BlazorStrap
{
    public class BSFormFeedback<T> : ValidationMessage<T> 
    {
        private bool Clean = true;
        private FieldIdentifier _fieldIdentifier;
        ///  [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder()
            .AddClass("valid-tooltip", MyEditContext != null &&  GetErrorCount() == 0 && !Clean && IsTooltip)
            .AddClass("valid-feedback", MyEditContext != null && GetErrorCount() == 0 && !Clean && !IsTooltip)
            .AddClass("invalid-tooltip", MyEditContext != null && GetErrorCount() > 0 && IsTooltip)
            .AddClass("invalid-feedback", MyEditContext != null &&  GetErrorCount() > 0  && !IsTooltip)
            .AddClass(Class)
        .Build();

        protected int GetErrorCount()
        {
            int i = 0;
            foreach (var message in MyEditContext.GetValidationMessages(_fieldIdentifier))
            {
                i++;
            }
            Clean = false;
            return i;
        }

        [CascadingParameter] EditContext MyEditContext { get; set; }
        [Parameter] protected bool IsValid { get; set; }
        [Parameter] protected bool IsInvalid { get; set; }
        [Parameter] protected bool IsTooltip { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }

        protected override void OnParametersSet()
        {
            if (For != null)
            {
                _fieldIdentifier = FieldIdentifier.Create(For);
            }
        }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", classname);
            builder.OpenComponent<ValidationMessage<T>>(2);
            builder.AddMultipleAttributes(3, AdditionalAttributes);
            builder.AddAttribute(4, "For", For);
            builder.AddAttribute(5, "ChildContent", ChildContent);
            builder.CloseComponent();
            builder.CloseElement();
        }

       
    }
}
