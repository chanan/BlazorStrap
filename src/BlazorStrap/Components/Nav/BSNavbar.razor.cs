using BlazorComponentUtilities;
using BlazorStrap.Util;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Timers;

namespace BlazorStrap
{
    public abstract class BSNavbarBase : ComponentBase
    {
        [Parameter] public bool Header { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public Color Color { get; set; } = Color.None;
        [Parameter] public bool IsDark { get; set; }
        [Parameter] public bool IsExpand { get; set; }
        [Parameter] public bool IsFixedBottom { get; set; }
        [Parameter] public bool IsFixedTop { get; set; }
        [Parameter] public bool IsStickyTop { get; set; }
        [Parameter] public bool HideLight { get; set; } = false;
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        internal bool HasCollapsed { get; set; }
        internal bool Visible
        {
            get => _visible;
            set
            {
                VisibleChange.Invoke(this, value);
                _visible = value;
            }
        }


        internal EventHandler<bool> VisibleChange { get; set; }
        protected string Classname =>
        new CssBuilder("navbar")
            .AddClass("fixed-top", IsFixedTop)
            .AddClass("fixed-bottom", IsFixedBottom)
            .AddClass("sticky-top", IsStickyTop)
            .AddClass("navbar-dark", IsDark)
            .AddClass("navbar-light", !IsDark && !HideLight)
            .AddClass($"bg-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass("navbar-expand-lg", IsExpand)
            .AddClass(Class)
        .Build();

        protected string Tag => Header? "Header" : "Nav";
        

        private bool _visible { get; set; }

    }
}
