using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSTableRow : BootstrapComponentBase
    {
        protected string classname =>
        new CssBuilder()
            .AddClass($"table-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass(Class)
        .Build();

        [Parameter] protected Color Color { get; set; } = Color.None;
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
