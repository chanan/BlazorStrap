using BlazorComponentUtilities;
using BlazorStrap.Util;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSProgressBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string Classname =>
        new CssBuilder("progress-bar")
            .AddClass("progress-bar-striped", IsStriped)
            .AddClass("progress-bar-animated", IsAnimated)
            .AddClass($"bg-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass(Class)
        .Build();

        protected string ClassnameMulti =>
            new CssBuilder("progress")
                .AddClass(Class)
            .Build();

        [Parameter] public int Value { get; set; }
        [Parameter] public int Max { get; set; } = 100;
        protected string styles
        {
            get
            {
                if (Value == 0) { return null; }
                var percent = Math.Round((Value / (double)Max) * 100);
                return $"width: {percent}%; {Style}".Trim();
            }
        }
        [Parameter] public Color Color { get; set; } = Color.None;
        [Parameter] public bool IsMulti { get; set; }
        [Parameter] public bool IsBar { get; set; }
        [Parameter] public bool IsStriped { get; set; }
        [Parameter] public bool IsAnimated { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public string Style { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
