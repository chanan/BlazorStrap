using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Timers;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSDropdownMenu : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
             new CssBuilder("dropdown-menu")
                 .AddClass("show", !IsOpen.HasValue && DropDown?.Selected == this)
                 .AddClass("show", !IsOpen.HasValue && NavItem?.Selected == this)
                 .AddClass("show", IsOpen.HasValue && IsOpen.Value)
                 .AddClass(Class)
             .Build();

        [Parameter] public bool? IsOpen { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public bool AutoClose { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [CascadingParameter] internal BSDropdown DropDown { get; set; }
        [CascadingParameter] internal BSNavItem NavItem { get; set; }

        internal bool IsSubmenu
        {
            get
            {
                if (DropDown == null)
                {
                    return false;
                }
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
                DropDown.Selected =this;
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
        protected override void OnInitialized()
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
            base.OnInitialized();
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
