using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Forms;
using System.Timers;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap
{
    public class BSBasicForm : ComponentBase
    {

        protected string classname =>
                                new CssBuilder()
                                    .AddClass("form-inline", IsInline)
                                    .AddClass(Class)
                                .Build();
        [Parameter(CaptureUnmatchedValues = true)] public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public bool UserValidation { get; set; }
        [Parameter] public bool IsInline { get; set; }
        [Parameter] public string Class { get; set; }
        
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CascadingValue<BSBasicForm>>(0);
            builder.AddAttribute(1, "IsFixed", true);
            builder.AddAttribute(2, "Value", this);
            builder.AddAttribute(3, "ChildContent", ChildContent);
            builder.CloseComponent();
        }
    }
}
