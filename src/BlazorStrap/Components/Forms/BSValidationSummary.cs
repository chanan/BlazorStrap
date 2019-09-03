using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorStrap.Components.Forms
{
    public class BSValidationSummary : ComponentBase
    {
        [CascadingParameter] EditContext CurrentEditContext { get; set; }
        [Parameter] public bool IsValid { get; set; }
        [Parameter] public bool IsInvalid { get; set; }
        [Parameter] public bool Manual { get; set; }
        [Parameter] public Dictionary<string, string> ErrorMessages { get; set; } = new Dictionary<string, string>();

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (CurrentEditContext != null)
            {
                builder.OpenComponent(0, typeof(ValidationSummary));
                builder.CloseComponent();
            }

            var errors = ErrorMessages.GetEnumerator();
            if (errors.MoveNext())
            {
                builder.OpenElement(1, "ul");
                do
                {
                    builder.OpenElement(2, "li");
                    builder.AddAttribute(3, "class", "validation-message");
                    builder.AddContent(4, errors.Current);
                    builder.CloseElement();

                }
                while (errors.MoveNext());
                builder.CloseElement();
            }         
        }
    }
}
