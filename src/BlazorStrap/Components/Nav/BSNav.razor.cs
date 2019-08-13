using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSNav : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        private CodeBSNavItem _selected;
        internal List<CodeBSNavItem> Navitems { get; set; } = new List<CodeBSNavItem>();
        public CodeBSNavItem Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                StateHasChanged();
            }
        }
        protected private string classname =>
        new CssBuilder("nav")
            .AddClass("navbar-nav", IsNavbar)
            .AddClass("nav-tabs", IsTabs)
            .AddClass("nav-pills", IsPills)
            .AddClass("nav-fill", IsFill)
            .AddClass("flex-column", IsVertical)
            .AddClass(GetAlignment())
            .AddClass(Class)
        .Build();

        protected string Tag => IsList ? "ul" : "nav";

        [Parameter] public bool IsList { get;set;} 
        [Parameter] public Alignment Alignment { get; set; } = Alignment.None;
        [Parameter] public bool IsVertical { get; set; }
        [Parameter] public bool IsTabs { get; set; }
        [Parameter] public bool IsPills { get; set; }
        [Parameter] public bool IsFill { get; set; }
        [Parameter] public bool IsNavbar { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        private string GetAlignment()
        {
            if (Alignment == Alignment.Center) { return "justify-content-center"; }
            if (Alignment == Alignment.Right) { return "justify-content-end"; }
            return null;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}
