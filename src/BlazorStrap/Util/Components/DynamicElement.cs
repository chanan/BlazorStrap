using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;

namespace BlazorStrap.Util.Components
{
    /// <summary>
    /// Renders an element with the specified name and attributes. This is useful
    /// when you want to combine a set of attributes declared at compile time with
    /// another set determined at runtime.
    /// </summary>
    public class DynamicElement : ComponentBase
    {
        /// <summary>
        /// Gets or sets the name of the element to render.
        /// </summary>
        [Parameter] public string TagName { get; set; }
        [Parameter] public bool OnClickPreventDefault { get; set; }
        [Parameter] public bool OnClickStopPropagation { get; set; }
        [Parameter] public ElementReference ElementRef { get; set; }
        [Parameter] public Action<ElementReference> ElementRefChanged { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> MyParams { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            builder?.OpenElement(0, TagName);
            
            builder.AddMultipleAttributes(1, MyParams);
            builder.AddEventPreventDefaultAttribute(2, "onclick", OnClickPreventDefault);
            builder.AddEventStopPropagationAttribute(3, "onclick", OnClickStopPropagation);
            builder.AddContent(4, ChildContent);
            builder.AddElementReferenceCapture(2, capturedRef =>
            {
                ElementRef = capturedRef;
                ElementRefChanged?.Invoke(ElementRef); // Invoke the callback for the binding to work.
            });
           
            builder.CloseElement();
        }
    }
}
