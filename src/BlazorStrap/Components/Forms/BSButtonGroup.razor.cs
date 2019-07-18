using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSButtonGroup : BootstrapComponentBase
    {
        protected string classname =>
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

        [Parameter] protected bool IsOpen { get; set; }
        [Parameter] protected bool IsToggle { get; set; }
        [Parameter] protected bool IsToolbar { get; set; }
        [Parameter] protected bool IsVertical { get; set; }
        [Parameter] protected DropdownDirection DropdownDirection { get; set; } = DropdownDirection.Down;
        [Parameter] protected Size Size { get; set; } = Size.None;
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
