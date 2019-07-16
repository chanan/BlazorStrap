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
                 .AddClass("show", !IsOpen.HasValue && DropDown?.Selected == this)
                 .AddClass("show", !IsOpen.HasValue && NavItem?.Selected == this)
                 .AddClass("show", IsOpen.HasValue && IsOpen.Value)
                 .AddClass(Class)
             .Build();

        [Parameter] protected bool? IsOpen { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected bool AutoClose { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [CascadingParameter] internal BSDropdown DropDown { get; set; }
        [CascadingParameter] internal BSNavItem NavItem { get; set; }

        internal bool IsSubmenu
        {
            get
            {
                return DropDown.IsSubmenu;
            }
        }
        // WORKAROUND. Below is a work around for auto close. Until on mouse leave event exist so not default behaviour.
        private System.Timers.Timer _timer = new System.Timers.Timer(1000);

        internal bool Open
        {
            get
            {
                if (DropDown?.Selected == this)
                {
                    return true;
                }
                if (NavItem?.Selected == this)
                {
                    return true;
                }
                return false;
            }
        }
        internal void Show()
        {
            if (DropDown != null)
            {
                DropDown.Selected = this;
            }
            if (NavItem != null)
            {
                NavItem.Selected = this;
            }
        }
        internal void Hide()
        {
            if (DropDown?.Selected == this)
            {
                DropDown.Selected = null;
            }
            if (NavItem?.Selected == this)
            {
                NavItem.Selected = null;
            }
        }
        internal void Toggle()
        {
            if (DropDown != null)
            {
                if (DropDown.Selected == this)
                {
                    DropDown.Selected = null;
                }
                else
                {
                    DropDown.Selected = this;
                }
            }
            if (NavItem != null)
            {
                if (NavItem.Selected == this)
                {
                    NavItem.Selected = null;
                }
                else
                {
                    NavItem.Selected = this;
                }
            }
        }
        protected override void OnInit()
        {
            if (DropDown != null)
            {
                DropDown.DropDownMenu = this;
            }
            if(NavItem != null)
            {
                NavItem.DropDownMenu = this;
            }
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
            if(DropDown?.Selected == this)
            {
                DropDown.Selected = null;
            }
            if (NavItem?.Selected == this)
            {
                NavItem.Selected = null;
            }
            _timer.Stop();
            _timer.Interval = 1000;
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
