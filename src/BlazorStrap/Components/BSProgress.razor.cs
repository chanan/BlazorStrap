using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSProgress : BootstrapComponentBase
    {
        protected string classname =>
        new CssBuilder("progress-bar")
            .AddClass("progress-bar-striped", IsStriped)
            .AddClass("progress-bar-animated", IsAnimated)
            .AddClass($"bg-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass(Class)
        .Build();

        protected string classnameMulti =>
            new CssBuilder("progress")
                .AddClass(Class)
            .Build();

        [Parameter] protected int Value { get; set; }
        [Parameter] protected int Max { get; set; } = 100;
        protected string styles
        {
            get
            {
                if (Value == 0) { return null; }
                var percent = Math.Round(((double)Value / (double)Max) * 100);
                return $"width: {percent}%; {Style}".Trim();
            }
        }
        [Parameter] protected Color Color { get; set; } = Color.None;
        [Parameter] protected bool IsMulti { get; set; }
        [Parameter] protected bool IsBar { get; set; }
        [Parameter] protected bool IsStriped { get; set; }
        [Parameter] protected bool IsAnimated { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected string Style { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
