using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using System.Timers;

namespace BlazorStrap 
{
    public class CodeBSDropdown : ToggleableComponentBase
    {
        // Prevents rogue closing
        private System.Timers.Timer _timer = new System.Timers.Timer(250);
        private CodeBSDropdownMenu _selected;
        private CodeBSDropdownMenu _dropDownMenu { get; set; } = new BSDropdownMenu();

        internal CodeBSDropdownMenu DropDownMenu
        {
            get
            {
                return _dropDownMenu;
            }
            set
            {
                _dropDownMenu = value;
                StateHasChanged();
            }
        }
        public CodeBSDropdownMenu Selected
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

        protected string classname =>
        new CssBuilder()
            .AddClass("dropdown", !IsGroup)
            .AddClass("btn-group", IsGroup)
            .AddClass("dropdown-submenu", IsSubmenu)
            .AddClass(DropdownDirection.ToDescriptionString(), DropdownDirection != DropdownDirection.Down)
            .AddClass("show", !_manual && Selected != null)
            .AddClass("show", _manual && IsOpen.HasValue && IsOpen.Value)
            .AddClass(Class)
        .Build();

        internal bool IsSubmenu;
        [Parameter] protected DropdownDirection DropdownDirection { get; set; } = DropdownDirection.Down;
        [Parameter] protected bool IsGroup { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [CascadingParameter] protected BSDropdown Dropdown { get; set; }
        [CascadingParameter] internal BSNavItem NavItem { get; set; }

        protected override void OnInit()
        {
            _timer.Elapsed += OnTimedEvent;
            if (Dropdown != null || NavItem != null)
            {
                IsSubmenu = true;
            }
            base.OnInit();
        }
        internal void GotFocus()
        {
            Dropdown?.GotFocus();
            _timer.Stop();
            _timer.Interval = 250;
        }
        protected void LostFocus()
        {
            _timer.Start();
        }

        protected void Close()
        {
            Selected = null;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (!_manual)
            {
                Invoke(() => Close());
            }
            _timer.Stop();
            _timer.Interval = 250;
        }
    }
}
