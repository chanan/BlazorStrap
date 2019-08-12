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
        private FieldIdentifier _fieldIdentifier;
        ///  [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder()
            .AddClass("valid-tooltip", MyEditContext != null && Regex.IsMatch(GetErrorCount(), @"\bvalid\b") && IsTooltip)
            .AddClass("valid-feedback", MyEditContext != null && Regex.IsMatch(GetErrorCount(), @"\bvalid\b") && !IsTooltip)
            .AddClass("invalid-tooltip", MyEditContext != null && Regex.IsMatch(GetErrorCount(), @"\binvalid\b") && IsTooltip)
            .AddClass("invalid-feedback", MyEditContext != null && Regex.IsMatch(GetErrorCount(), @"\binvalid\b") && !IsTooltip)

            .AddClass("valid-tooltip", (Parent?.UserValidation ?? false) && IsValid && IsTooltip)
            .AddClass("valid-feedback", (Parent?.UserValidation ?? false) && IsValid && !IsTooltip)
            .AddClass("invalid-tooltip", (Parent?.UserValidation ?? false) && IsInvalid && IsTooltip)
            .AddClass("invalid-feedback", (Parent?.UserValidation ?? false) && IsInvalid && !IsTooltip)
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

        [CascadingParameter] BSForm Parent { get; set; }
        [CascadingParameter] EditContext MyEditContext { get; set; }
        [Parameter] protected bool IsValid { get; set; }
        [Parameter] protected bool IsInvalid { get; set; }
        [Parameter] protected bool IsTooltip { get; set; }
        [Parameter] protected string Class { get; set; }

        /// <summary>
        /// ValidMessage is the string that gets returned if validation is valid.
        /// </summary>
        [Parameter] protected string ValidMessage { get; set; }
        /// <summary>
        /// InvalidMessage is the string that gets returned if validation is invalid.
        /// </summary>
        [Parameter] protected string InvalidMessage { get; set; }
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
            if ((Regex.IsMatch(GetErrorCount(), @"\bvalid\b") || IsValid == true ) && IsInvalid == false)
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", classname);
                builder.AddContent(6, ValidMessage);
                builder.CloseElement();
            }
            else if(IsInvalid)
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", classname);
                builder.AddContent(6, InvalidMessage);
                builder.CloseElement();
            }
            else {
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
