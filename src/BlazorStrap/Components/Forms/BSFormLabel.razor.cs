using BlazorComponentUtilities;
using BlazorStrap.Util;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSFormLabelBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }

        protected string Classname =>
        new CssBuilder()
            .AddClass($"col-form-label col-form-label-{Size.ToDescriptionString()}", Size != Size.None)
            .AddClass(Class)
        .Build();

        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public Size Size { get; set; } = Size.None;
    }
}
