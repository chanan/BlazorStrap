using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Timers;

namespace BlazorStrap
{
    public class CodeBSDropdownMenu : BootstrapComponentBase
    {
        protected string classname =>
             new CssBuilder("dropdown-menu")
                 .AddClass("show", !IsOpen.HasValue && DropDownMenuControl != null && DropDownMenuControl.Handler.IsOpen(DropDownMenuControl.Id))
                 .AddClass("show", IsOpen.HasValue && IsOpen.Value)
                 .AddClass(Class)
             .Build();

        [Parameter] protected bool? IsOpen { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected bool AutoClose { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [CascadingParameter] protected DropDownMenuControl DropDownMenuControl { get; set; }

        // WORKAROUND. Below is a work around for auto close. Until on mouse leave event exist so not default behaviour.
        private System.Timers.Timer _timer = new System.Timers.Timer(1000);

        protected override void OnInit()
        {
            if (AutoClose)
            {
                _timer.Elapsed += OnTimedEvent;
            }
            base.OnInit();
        }
        public void MouseOut(UIMouseEventArgs e)
        {
            if (AutoClose)
            {
                _timer.Start();
            }

        }

        public void MouseOver(UIMouseEventArgs e)
        {
            if (AutoClose)
            {
                _timer.Stop();
                _timer.Interval = 1000;
            }

        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (DropDownMenuControl != null)
            {
                if (DropDownMenuControl.Handler.IsOpen(DropDownMenuControl.Id))
                {
                    Invoke(() => DropDownMenuControl.Handler.Toggle(DropDownMenuControl.Id));
                    _timer.Stop();
                    _timer.Interval = 1000;
                }
            }
        }

        public void Dispose()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }
        }
    }
}
