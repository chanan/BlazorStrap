using BlazorComponentUtilities;
using BlazorStrap.Util.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public partial class BSNavItem : ToggleableComponentBase, IDisposable
    {
        internal bool IsSubmenu { get; set; }
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }

        private BSDropdownMenu _selected;
        private bool _shouldClose { get; set; } = false;

        //Prevents NULL
        private BSDropdownMenu _dropDownMenu { get; set; } = new BSDropdownMenu();

        public BSDropdownMenu DropDownMenu
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
            get
            {
                if (Nav != null)
                {
                    if (Nav.Selected != this)
                    {
                        return null;
                    }
                }
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

        private bool _active;

        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                StateHasChanged();
            }
        }

        protected string Classname =>
            new CssBuilder()
                .AddClass("nav-item", !RemoveDefaultClass)
                .AddClass("dropdown", IsDropdown)
                .AddClass("dropdown-submenu", IsSubmenu)
                .AddClass("show", IsDropdown && (Nav?.Selected == this || (IsOpen ?? false)))
                .AddClass("active", _active)
                .AddClass(Class)
            .Build();

        protected string Tag => Nav.IsList ? "li" : "div";
        [Parameter] public bool RemoveDefaultClass { get; set; }
        [Parameter] public bool IsDropdown { get; set; }
        [Parameter] public bool CloseOnFocusout { get; set; } = true;
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [CascadingParameter] internal BSNav Nav { get; set; }
        [CascadingParameter] internal BSNavItem NavItem { get; set; }

        protected override Task OnInitializedAsync()
        {
            if (IsDropdown && !Manual)
            {
                Nav.Navitems.Add(this);
            }
            if (NavItem != null)
            {
                IsSubmenu = true;
            }
            return base.OnInitializedAsync();
        }

        protected void MouseLeave()
        {
            _shouldClose = true;
        }

        protected void MouseEnter()
        {
            _shouldClose = false;
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
            if (!CloseOnFocusout)
            {
                return;
            }
            if (_shouldClose)
            {
                if (Nav.Selected == this)
                {
                    Selected = null;
                }
                else
                {
                    if (Manual)
                    {
                        Hide();
                    }
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDropdown && disposing)
            {
                Nav.Navitems.Remove(this);
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }
    }
}