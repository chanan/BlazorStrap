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
    public abstract class BSDropdownBase : ToggleableComponentBase , IDisposable
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Parameter] public EventCallback<BSDropdownEvent> ShowEvent { get; set; }
        [Parameter] public EventCallback<BSDropdownEvent> ShownEvent { get; set; }
        [Parameter] public EventCallback<BSDropdownEvent> HideEvent { get; set; }
        [Parameter] public EventCallback<BSDropdownEvent> HiddenEvent { get; set; }

        internal BSDropdownEvent BSDropdownEvent { get; set; }
        internal List<EventCallback<BSDropdownEvent>> EventQue { get; set; } = new List<EventCallback<BSDropdownEvent>>();

        // Prevents rogue closing
        private BSDropdownMenuBase _selected;
        private BSDropdownMenuBase _dropDownMenu { get; set; } = new BSDropdownMenu();
        public bool Active = false;
        private bool ShouldClose = false;
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
        protected void MouseLeave()
        {
            ShouldClose = true;
        }
        protected void MouseEnter()
        {
            ShouldClose = false;
        }
        protected string classname =>
        new CssBuilder()
            .AddClass("dropdown", !IsGroup)
            .AddClass(AnimationClass, !DisableAnimations)
            .AddClass("btn-group", IsGroup)
            .AddClass("dropdown-submenu", IsSubmenu)
            .AddClass(DropdownDirection.ToDescriptionString(), DropdownDirection != DropdownDirection.Down)
            .AddClass("show", Selected != null || (IsOpen ?? false))
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
            if (Dropdown != null || NavItem != null)
            {
                IsSubmenu = true;
            }
            BlazorStrapInterop.OnAnimationEndEvent += OnAnimationEnd;
            base.OnInitialized();
        }
        private async Task OnAnimationEnd(string id)
        {
            BSDropdownEvent = new BSDropdownEvent() { Target = this };
            if (id != MyRef.Id)
            {
                if (IsOpen ?? false)
                {
                    await ShownEvent.InvokeAsync(BSDropdownEvent);
                }
                else
                {
                    await HiddenEvent.InvokeAsync(BSDropdownEvent);
                }
            }

        }

        public void Dispose()
        {
            BlazorStrapInterop.OnAnimationEndEvent -= OnAnimationEnd;
        }
        internal override async Task Changed(bool e)
        {
            BSDropdownEvent = new BSDropdownEvent() { Target = this };
            if (e)
            {
                await ShowEvent.InvokeAsync(BSDropdownEvent);
            }
            else
            {
                await HideEvent.InvokeAsync(BSDropdownEvent);
            }
        }

        
        protected void LostFocus()
        {
            if(ShouldClose)
            {
                Close();
            }
        }

        protected void Close()
        {
            Selected = null;
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
