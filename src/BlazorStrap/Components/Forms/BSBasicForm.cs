using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class BSBasicForm : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }

        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter] public string Class { get; set; }

        [Parameter] public bool IsInline { get; set; }

        [Parameter] public bool UserValidation { get; set; }

        protected string Classname =>
            new CssBuilder()
            .AddClass("form-inline", IsInline)
            .AddClass(Class)
            .Build();

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder?.OpenComponent<CascadingValue<BSBasicForm>>(0);
            builder.AddAttribute(1, "IsFixed", true);
            builder.AddAttribute(2, "Value", this);
            builder.AddAttribute(3, "ChildContent", ChildContent);
            builder.CloseComponent();
        }
    }
}
