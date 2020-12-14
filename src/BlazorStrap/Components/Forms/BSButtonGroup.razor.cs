using BlazorComponentUtilities;
using BlazorStrap.Util;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorStrap
{
    public partial class BSButtonGroup : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }

        protected string Classname =>
        new CssBuilder()
            .AddClass("btn-toolbar", IsToolbar)
            .AddClass("btn-group", !IsToolbar && !IsVertical)
            .AddClass("btn-group-vertical", !IsToolbar && IsVertical)
            .AddClass($"btn-group-{Size.ToDescriptionString()}", Size != Size.None)
            .AddClass("btn-group-toggle", IsToggle)
            .AddClass(DropdownDirection.ToDescriptionString(), DropdownDirection != DropdownDirection.Down)
            .AddClass("show", IsOpen)
            .AddClass(Class)
        .Build();

        [Parameter] public bool IsOpen { get; set; }
        [Parameter] public bool IsToggle { get; set; }
        [Parameter] public bool IsToolbar { get; set; }
        [Parameter] public bool IsVertical { get; set; }
        [Parameter] public DropdownDirection DropdownDirection { get; set; } = DropdownDirection.Down;
        [Parameter] public Size Size { get; set; } = Size.None;
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
