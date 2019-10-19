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
        [Parameter] public EventCallback OnSubmit { get; set; }

        protected string Classname =>
            new CssBuilder()
            .AddClass("form-inline", IsInline)
            .AddClass(Class)
            .Build();

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            RenderFragment _form;
            _form = Formbuilder =>
            {
                Formbuilder.OpenElement(0, "form");
                Formbuilder.AddMultipleAttributes(1, AdditionalAttributes);
                Formbuilder.AddAttribute(2, "class", Classname);
                Formbuilder.AddAttribute(3, "OnSubmit", OnSubmit);
                Formbuilder.AddContent(4, ChildContent);
                Formbuilder.CloseElement();
            };

            builder?.OpenComponent<CascadingValue<BSBasicForm>>(0);
            builder.AddAttribute(1, "Value", this);
            builder.AddAttribute(2, "IsFixed", true);
            builder.AddAttribute(3, "ChildContent", _form);
            builder.CloseComponent();
        }
    }
}
