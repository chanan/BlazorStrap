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
        [Parameter] public RenderFragment<EditContext> ChildContent { get; set; }
        [Parameter] public bool UserValidation { get; set; }
        [Parameter] public bool IsInline { get; set; }
        [Parameter] public string Class { get; set; }
        
        private RenderFragment Form { get; set; }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
                Form = Formbuilder =>
                {
                    Formbuilder.OpenComponent<EditForm>(0);
                    Formbuilder.AddMultipleAttributes(1, AdditionalAttributes);
                    Formbuilder.AddAttribute(2, "class", classname);
                    Formbuilder.AddAttribute(3, "ChildContent", ChildContent);
                    Formbuilder.CloseComponent();
                };

                builder.OpenComponent<CascadingValue<BSForm>>(3);
                builder.AddAttribute(4, "IsFixed", true);
                builder.AddAttribute(5, "Value", this);
                builder.AddAttribute(6, "ChildContent", Form);
                builder.CloseComponent();
        }
    }
}
