using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSTabListBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
            new CssBuilder("nav")
                .AddClass("nav-tabs", !IsPills)
                .AddClass("nav-pills", IsPills)
                .AddClass("flex-column", IsVertical)
                .AddClass(GetAlignment())
                .AddClass(Class)
            .Build();

        protected string Tag => IsList ? "ul" : "nav";

        private bool _isList;
        [Parameter]
        public bool IsList
        {
            get
            {
                return _isList;
            }
            set
            {
                _isList = value;
            }
        }
        [Parameter] public Alignment Alignment { get; set; } = Alignment.None;
        [Parameter] public bool IsVertical { get; set; }
        [Parameter] public bool IsPills { get; set; }
        [Parameter] public bool IsFill { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        private string GetAlignment()
        {
            if (Alignment == Alignment.Center) { return "justify-content-center"; }
            if (Alignment == Alignment.Right) { return "justify-content-end"; }
            return null;
        }
    }
}
