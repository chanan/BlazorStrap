using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSNavItem : ToggleableComponentBase
    {

        private CodeBSDropdownMenu _selected;
        //Prevents NULL
        private CodeBSDropdownMenu _dropDownMenu { get; set; } = new BSDropdownMenu();

        public CodeBSDropdownMenu DropDownMenu
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
        protected string classname =>
            new CssBuilder("nav-item")
                .AddClass("dropdown", IsDropdown)
                .AddClass("show", IsDropdown && _manual == null && Nav?.Selected == this)
                .AddClass("show", IsDropdown && _manual != null && IsOpen.HasValue && IsOpen.Value)
                .AddClass(Class)
            .Build();

        protected string Tag => Nav.IsList ? "li" : "div";

        [Parameter] protected bool IsDropdown { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [CascadingParameter] internal BSNav Nav { get; set; }
        protected override Task OnInitAsync()
        {
            if (IsDropdown && _manual == null)
            {
                Nav.Navitems.Add(this);
            }
            return base.OnInitAsync();
        }

        protected void MouseDown()
        {
            if (_manual == null && IsDropdown)
            {
                _MouseDown = Nav.Selected == this && true;
            }
            else
            {
                _MouseDown = IsOpen.Value && true;
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
