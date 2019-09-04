using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeSTableHeadCellBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
          new CssBuilder()
              .AddClass($"table-{Color.ToDescriptionString()}", Color != Color.None)
              .AddClass(Class)
          .Build();

        [Parameter] public Color Color { get; set; } = Color.None;
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
