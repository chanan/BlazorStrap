using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSNavItemBase : ToggleableComponentBase , IDisposable
    {
        internal bool IsSubmenu;
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }

        private BSDropdownMenuBase _selected;
        //Prevents NULL
        private BSDropdownMenuBase _dropDownMenu { get; set; } = new BSDropdownMenu();

        public BSDropdownMenuBase DropDownMenu
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
                if (value == null)
                {
                    _selected = null;
                    Nav.Selected = null;
                }
                else
                {
                    Nav.Selected = this;
                    _selected = value;
                }
            }
        }
        private bool _MouseDown = false;
        private bool _active;
        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                StateHasChanged();
            }
        }
        protected string classname =>
            new CssBuilder()
                .AddClass("nav-item", !RemoveDefaultClass)
                .AddClass("dropdown", IsDropdown)
                .AddClass("dropdown-submenu", IsSubmenu)
                .AddClass("show", IsDropdown && _manual == null && Nav?.Selected == this)
                .AddClass("show", IsDropdown && _manual != null && IsOpen.HasValue && IsOpen.Value)
                .AddClass("active", _active)
                .AddClass(Class)
            .Build();

        protected string Tag => Nav.IsList ? "li" : "div";

        [Parameter] public bool RemoveDefaultClass { get; set; }
        [Parameter] public bool IsDropdown { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [CascadingParameter] internal BSNav Nav { get; set; }
        [CascadingParameter] internal BSNavItem NavItem { get; set; }
        protected override Task OnInitializedAsync()
        {
            if (IsDropdown && _manual == null)
            {
                Nav.Navitems.Add(this);
            }
            if ( NavItem != null)
            {
                IsSubmenu = true;
            }
            return base.OnInitializedAsync();
        }

        protected void MouseDown()
        {
            if (_manual == null && IsDropdown)
            {
                _MouseDown = Nav.Selected == this && true;
            }
            else
            {
                if (IsOpen != null)
                {
                    _MouseDown = IsOpen.Value && true;
                }
            }
        }

        public override void Toggle()
        {
            if (Nav.Selected == this)
            {
                Selected = null;
            }
            else
            {
                Selected = DropDownMenu;
            }
            base.Toggle();
        }

        public override void Show()
        {
            Selected = DropDownMenu;
            base.Show();
        }

        public override void Hide()
        {
            Selected = null;
            base.Hide();
        }

        protected void LostFocus()
        {
            if (IsDropdown && _manual != null && !_MouseDown)
            {
                if (Nav.Selected == this)
                {
                    Selected = null;
                }
            }
            _MouseDown = false;
        }

        public void Dispose()
        {
            if (IsDropdown && IsOpen == null)
            {
                Nav.Navitems.Remove(this);
            }
        }
    }
}
