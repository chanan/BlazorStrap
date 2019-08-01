using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Threading.Tasks;

namespace BlazorStrap 
{
    public class CodeBSDropdown : ToggleableComponentBase
    {

        [Parameter] protected EventCallback<BSDropdownEvent> ShowEvent { get; set; }
        [Parameter] protected EventCallback<BSDropdownEvent> ShownEvent { get; set; }
        [Parameter] protected EventCallback<BSDropdownEvent> HideEvent { get; set; }
        [Parameter] protected EventCallback<BSDropdownEvent> HiddenEvent { get; set; }

        internal BSDropdownEvent BSDropdownEvent { get; set; }
        internal List<EventCallback<BSDropdownEvent>> EventQue { get; set; } = new List<EventCallback<BSDropdownEvent>>();

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
            .AddClass("show", _manual == null && Selected != null)
            .AddClass("show", _manual != null && IsOpen.HasValue && IsOpen.Value)
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

        internal override void Changed(bool e)
        {
            BSDropdownEvent = new BSDropdownEvent() { Target = this };
            if (e)
            {
                ShowEvent.InvokeAsync(BSDropdownEvent);
                EventQue.Add(ShownEvent);
            }
            else
            {
                HideEvent.InvokeAsync(BSDropdownEvent);
                EventQue.Add(HiddenEvent);
            }
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
            if (_manual == null)
            {
                Invoke(() => Close());
            }
            _timer.Stop();
            _timer.Interval = 250;
        }

        protected override Task OnAfterRenderAsync()
        {
            for (int i = 0; i < EventQue.Count; i++)
            {
                EventQue[i].InvokeAsync(BSDropdownEvent);
                EventQue.RemoveAt(i);
            }
            return base.OnAfterRenderAsync();
        }
    }
}
