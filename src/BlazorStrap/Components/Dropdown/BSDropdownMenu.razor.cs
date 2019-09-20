using BlazorComponentUtilities;
using BlazorStrap.Util.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSDropdownMenuBase : ToggleableComponentBase
    {
        [Inject] protected IJSRuntime JSRuntime { get; set; }
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }

        protected string Classname =>
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

        internal bool IsSubmenu => DropDown == null ? false : DropDown.IsSubmenu;

        internal bool Open => DropDown?.Selected == this ? true : NavItem?.Selected == this;

        public BSModalEvent BSModalEvent { get; private set; }

        public override void Show()
        {
            if (DropDown != null)
            {
                DropDown.Selected = this;
            }
            if (NavItem != null)
            {
                NavItem.Selected = this;
            }
        }

        public override void Hide()
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

        public override void Toggle()
        {
            if (DropDown != null)
            {
                DropDown.Selected = DropDown.Selected == this ? null : (this);
            }
            if (NavItem != null)
            {
                NavItem.Selected = NavItem.Selected == this ? null : (this);
            }
        }

        protected override void OnInitialized()
        {
            if (DropDown != null)
            {
                DropDown.DropDownMenu = this;
            }
            if (NavItem != null)
            {
                NavItem.DropDownMenu = this;
            }
            if (AnimationClass == null)
            {
                AnimationClass = "fade";
            }

            base.OnInitialized();
        }

        public void MouseOut()
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
    }
}
