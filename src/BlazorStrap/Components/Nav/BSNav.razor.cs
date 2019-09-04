using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using System.Timers;

namespace BlazorStrap
{
    public abstract class BSNavBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        private System.Timers.Timer _timer = new System.Timers.Timer(250);
        private BSNavItemBase _selected;
        internal List<BSNavItemBase> Navitems { get; set; } = new List<BSNavItemBase>();
        public BSNavItemBase Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
               InvokeAsync(StateHasChanged);
            }
        }
        protected private string classname =>
        new CssBuilder()
            .AddClass("nav", !RemoveDefaultClass)
            .AddClass("navbar-nav", IsNavbar )
            .AddClass("nav-tabs", IsTabs)
            .AddClass("nav-pills", IsPills)
            .AddClass("nav-fill", IsFill)
            .AddClass("flex-column", IsVertical)
            .AddClass(GetAlignment())
            .AddClass(Class)
        .Build();

        protected string Tag => IsList ? "ul" : "nav";

        [Parameter] public bool IsList { get;set;}
        [Parameter] public bool RemoveDefaultClass { get; set; }
        [Parameter] public Alignment Alignment { get; set; } = Alignment.None;
        [Parameter] public bool IsVertical { get; set; }
        [Parameter] public bool IsTabs { get; set; }
        [Parameter] public bool IsPills { get; set; }
        [Parameter] public bool IsFill { get; set; }
        [Parameter] public bool IsNavbar { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        internal void GotFocus()
        {
            _timer.Stop();
            _timer.Interval = 250;
        }
        protected void LostFocus()
        {
            _timer.Start();
        }

        private string GetAlignment()
        {
            if (Alignment == Alignment.Center) { return "justify-content-center"; }
            if (Alignment == Alignment.Right) { return "justify-content-end"; }
            return null;
        }

        protected override void OnInitialized()
        {
            _timer.Elapsed += OnTimedEvent;
        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if(Selected != null)
            Selected.Selected = null;
            _timer.Stop();
            _timer.Interval = 250;
        }
    }
}
