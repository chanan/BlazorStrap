using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSNav : BootstrapComponentBase
    {
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

        [Parameter] internal bool IsList { get;set;} 
        [Parameter] protected Alignment Alignment { get; set; } = Alignment.None;
        [Parameter] protected bool IsVertical { get; set; }
        [Parameter] protected bool IsTabs { get; set; }
        [Parameter] protected bool IsPills { get; set; }
        [Parameter] protected bool IsFill { get; set; }
        [Parameter] protected bool IsNavbar { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }

        private string GetAlignment()
        {
            if (Alignment == Alignment.Center) { return "justify-content-center"; }
            if (Alignment == Alignment.Right) { return "justify-content-end"; }
            return null;
        }

        protected override void OnInit()
        {
            base.OnInit();
        }
    }
}
