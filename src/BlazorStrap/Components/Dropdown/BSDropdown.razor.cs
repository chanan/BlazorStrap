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
    public abstract class BSDropdownBase : ToggleableComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Parameter] public EventCallback<BSDropdownEvent> ShowEvent { get; set; }
        [Parameter] public EventCallback<BSDropdownEvent> ShownEvent { get; set; }
        [Parameter] public EventCallback<BSDropdownEvent> HideEvent { get; set; }
        [Parameter] public EventCallback<BSDropdownEvent> HiddenEvent { get; set; }

        internal BSDropdownEvent BSDropdownEvent { get; set; }
        internal List<EventCallback<BSDropdownEvent>> EventQue { get; set; } = new List<EventCallback<BSDropdownEvent>>();

        // Prevents rogue closing
        private System.Timers.Timer _timer = new System.Timers.Timer(250);
        private BSDropdownMenuBase _selected;
        private BSDropdownMenuBase _dropDownMenu { get; set; } = new BSDropdownMenu();
        public bool Active = false;
        internal BSDropdownMenuBase DropDownMenu
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
        public BSDropdownMenuBase Selected
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

        protected string classname =>
        new CssBuilder()
            .AddClass("dropdown", !IsGroup)
            .AddClass("btn-group", IsGroup)
            .AddClass("dropdown-submenu", IsSubmenu)
            .AddClass(DropdownDirection.ToDescriptionString(), DropdownDirection != DropdownDirection.Down)
            .AddClass("show", _manual == null && Selected != null)
            .AddClass("show", _manual != null && IsOpen.HasValue && IsOpen.Value)
            .AddClass("active", Active)
            .AddClass(Class)
        .Build();

        internal bool IsSubmenu;
        [Parameter] public DropdownDirection DropdownDirection { get; set; } = DropdownDirection.Down;
        [Parameter] public bool IsGroup { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [CascadingParameter] protected BSDropdown Dropdown { get; set; }
        [CascadingParameter] internal BSNavItem NavItem { get; set; }

        protected override void OnInitialized()
        {
            _timer.Elapsed += OnTimedEvent;
            
            if (Dropdown != null || NavItem != null)
            {
                IsSubmenu = true;
            }
            base.OnInitialized();
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
                InvokeAsync(() => Close());
            }
            _timer.Stop();
            _timer.Interval = 250;
        }

        protected override Task OnAfterRenderAsync(bool firstrun)
        {
            for (int i = 0; i < EventQue.Count; i++)
            {
                EventQue[i].InvokeAsync(BSDropdownEvent);
                EventQue.RemoveAt(i);
            }
            return base.OnAfterRenderAsync(false);
        }
    }
}
