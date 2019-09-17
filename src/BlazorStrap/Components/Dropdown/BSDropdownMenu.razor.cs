using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Timers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public abstract class BSDropdownMenuBase : ToggleableComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
             new CssBuilder("dropdown-menu")
                 .AddClass("show", DropDown?.Selected == this)
                 .AddClass("show", NavItem?.Selected == this)
                 .AddClass("show", NavItem?.Selected != this && DropDown?.Selected != this && (IsOpen ?? false))
                 .AddClass(Class)
             .Build();

        [Parameter] public string Class { get; set; }
        [Parameter] public bool AutoClose { get; set; } 
        [Parameter] public RenderFragment ChildContent { get; set; }
        [CascadingParameter] internal BSDropdown DropDown { get; set; }
        [CascadingParameter] internal BSNavItem NavItem { get; set; }
        [CascadingParameter] internal BSButtonGroup ButtonGroup { get; set; }

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
           
            base.OnInitialized();
        }
        public void MouseOut(EventArgs e)
        {
            if (AutoClose)
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
        }
        public void Dispose()
        {
        }
    }
}
