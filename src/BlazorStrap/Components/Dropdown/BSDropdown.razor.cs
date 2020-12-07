using BlazorComponentUtilities;
using BlazorStrap.Util;
using BlazorStrap.Util.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public partial class BSDropdown : ToggleableComponentBase, IDisposable
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Parameter] public EventCallback<BSDropdownEvent> ShowEvent { get; set; }
        [Parameter] public EventCallback<BSDropdownEvent> ShownEvent { get; set; }
        [Parameter] public EventCallback<BSDropdownEvent> HideEvent { get; set; }
        [Parameter] public EventCallback<BSDropdownEvent> HiddenEvent { get; set; }

        [Inject] public BlazorStrapInterop BlazorStrapInterop { get; set; }

        internal BSDropdownEvent BSDropdownEvent { get; set; }
        internal List<EventCallback<BSDropdownEvent>> EventQue { get; set; } = new List<EventCallback<BSDropdownEvent>>();

        // Prevents rogue closing
        private BSDropdownMenu _selected;

        private BSDropdownMenu _dropDownMenu { get; set; } = new BSDropdownMenu();
        public bool Active { get; set; } = false;
        private bool _shouldClose { get; set; } = false;

        internal BSDropdownMenu DropDownMenu
        {
            get => _dropDownMenu;
            set
            {
                _dropDownMenu = value;
                StateHasChanged();
            }
        }

        public BSDropdownMenu Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                if (_selected != null) _selected.Changed(true);
                if(_selected == null)
                {
                    if(Dropdown != null)
                    {
                        Dropdown.Selected = _selected;
                    }
                    else if(NavItem != null)
                    {
                        NavItem.Selected = _selected;
                    }
                }
                InvokeAsync(StateHasChanged);
            }
        }

        protected void MouseLeave()
        {
            _shouldClose = true;
        }

        protected void MouseEnter()
        {
            _shouldClose = false;
        }

        protected string Classname =>
        new CssBuilder()
            .AddClass("dropdown", !IsGroup)
            .AddClass("btn-group", IsGroup)
            .AddClass("dropdown-submenu", IsSubmenu)
            .AddClass(DropdownDirection.ToDescriptionString(), DropdownDirection != DropdownDirection.Down)
            .AddClass("show", Selected != null || (IsOpen ?? false))
            .AddClass("active", Active)
            .AddClass(Class)
        .Build();

        internal bool IsSubmenu { get; set; }
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
                    await ShownEvent.InvokeAsync(BSDropdownEvent).ConfigureAwait(false);
                }
                else
                {
                    await HiddenEvent.InvokeAsync(BSDropdownEvent).ConfigureAwait(false);
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                BlazorStrapInterop.OnAnimationEndEvent -= OnAnimationEnd;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal override async Task Changed(bool e)
        {
            BSDropdownEvent = new BSDropdownEvent() { Target = this };
            if (e)
            {
                await ShowEvent.InvokeAsync(BSDropdownEvent).ConfigureAwait(false);
            }
            else
            {
                await HideEvent.InvokeAsync(BSDropdownEvent).ConfigureAwait(false);
            }
        }

        protected void LostFocus()
        {
            if (_shouldClose)
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
            for (var i = 0; i < EventQue.Count; i++)
            {
                EventQue[i].InvokeAsync(BSDropdownEvent);
                EventQue.RemoveAt(i);
            }
            return base.OnAfterRenderAsync(false);
        }
    }
}
