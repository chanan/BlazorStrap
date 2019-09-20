using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using System.Timers;

namespace BlazorStrap
{
    public abstract class BSNavBase : ComponentBase, IDisposable
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        private Timer _timer { get; set; } = new Timer(250);
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
        protected string Classname =>
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
            return Alignment == Alignment.Center ? "justify-content-center" : Alignment == Alignment.Right ? "justify-content-end" : null;
        }

        protected override void OnInitialized()
        {
            _timer.Elapsed += OnTimedEvent;
        }
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if(Selected != null)
            Selected.Selected = null;
            _timer.Stop();
            _timer.Interval = 250;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                _timer.Dispose();
                _selected.Dispose();
            }
        }
    }
}
