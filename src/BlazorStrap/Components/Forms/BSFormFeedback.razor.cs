using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace BlazorStrap
{
    public class BSFormFeedback<T> : ValidationMessage<T> 
    {
        private bool Clean = true;
        private bool Touched = false;
        private FieldIdentifier _fieldIdentifier;
        ///  [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder()
            .AddClass("valid-tooltip", MyEditContext != null && Regex.IsMatch(GetErrorCount(), @"\bvalid\b") && IsTooltip)
            .AddClass("valid-feedback", MyEditContext != null && Regex.IsMatch(GetErrorCount(), @"\bvalid\b") && !IsTooltip)
            .AddClass("invalid-tooltip", MyEditContext != null && Regex.IsMatch(GetErrorCount(), @"\binvalid\b") && IsTooltip)
            .AddClass("invalid-feedback", MyEditContext != null && Regex.IsMatch(GetErrorCount(), @"\binvalid\b") && !IsTooltip)
            .AddClass(Class)
        .Build();

        protected string GetErrorCount()
        {
            if (Clean)
            {
                Clean = false;
                return "";
            }
            return MyEditContext.FieldClass(_fieldIdentifier).ToLower();
        }

        [CascadingParameter] EditContext MyEditContext { get; set; }
        [Parameter] protected bool IsValid { get; set; }
        [Parameter] protected bool IsInvalid { get; set; }
        [Parameter] protected bool IsTooltip { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected string ValidMessage { get; set; }
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
            ;
            if (Regex.IsMatch(GetErrorCount(), @"\bvalid\b"))
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", classname);
                builder.AddContent(6, ValidMessage);
                builder.CloseElement();
            }
            else
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", classname);
                builder.OpenComponent<ValidationMessage<T>>(2);
                builder.AddMultipleAttributes(3, AdditionalAttributes);
                builder.AddAttribute(4, "For", For);
                builder.CloseComponent();
                builder.AddContent(5, ChildContent);
                builder.CloseElement();
            }
        }

       
    }
}
