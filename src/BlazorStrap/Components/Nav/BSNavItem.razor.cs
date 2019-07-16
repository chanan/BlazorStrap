using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public class CodeBSNavItem : ToggleableComponentBase
    {
        private bool _MouseDown = false;
        protected string classname =>
            new CssBuilder("nav-item")
                .AddClass("dropdown", IsDropdown)
                .AddClass("show", IsDropdown && !_manual && DropDownMenuControl.Handler.IsOpen(DropDownMenuControl.Id))
                .AddClass("show", IsDropdown && _manual && IsOpen.HasValue && IsOpen.Value)
                .AddClass(Class)
            .Build();

        protected string Tag => BlazorNavValues.IsList ? "li" : "div";

        [Parameter] protected bool IsDropdown { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [CascadingParameter] internal BlazorNavValues BlazorNavValues { get; set; }
        public DropDownMenuControl DropDownMenuControl { get; set; } = new DropDownMenuControl();
        protected override Task OnInitAsync()
        {
            if (BlazorNavValues.DropDownMenuHandler != null && IsDropdown && !_manual)
            {
                DropDownMenuControl.Handler = BlazorNavValues.DropDownMenuHandler;
                DropDownMenuControl.Id = Guid.NewGuid().ToString();
                BlazorNavValues.DropDownMenuHandler.AddDropDownMenu(DropDownMenuControl.Id);
            }
            return base.OnInitAsync();
        }

        protected void MouseDown()
        {
            if (!_manual && IsDropdown)
            {
                _MouseDown = DropDownMenuControl.Handler.IsOpen(DropDownMenuControl.Id) && true;
            }
            else
            {
                _MouseDown = IsOpen.Value && true;
            }
        }

        public override void Toggle()
        {
            DropDownMenuControl.Handler.Toggle(DropDownMenuControl.Id);
            base.Toggle();
        }

        public override void Show()
        {
            DropDownMenuControl.Handler.Show(DropDownMenuControl.Id);
            base.Show();
        }

        public override void Hide()
        {
            DropDownMenuControl.Handler.Hide(DropDownMenuControl.Id);
            base.Hide();
        }

        protected void LostFocus()
        {
            if (DropDownMenuControl != null && IsDropdown && !_manual && !_MouseDown)
            {
                if (DropDownMenuControl.Handler.IsOpen(DropDownMenuControl.Id))
                {
                    Invoke(() => DropDownMenuControl.Handler.Toggle(DropDownMenuControl.Id));
                }
            }
            _MouseDown = false;
        }

        public void Dispose()
        {
            if (BlazorNavValues.DropDownMenuHandler != null && IsDropdown && IsOpen == null)
            {
                BlazorNavValues.DropDownMenuHandler.RemoveDropDownMenu(DropDownMenuControl.Id);
            }

        }
    }
}
