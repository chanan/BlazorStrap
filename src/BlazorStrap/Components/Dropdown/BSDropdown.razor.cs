using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap 
{
    public class CodeBSDropdown : ToggleableComponentBase
    {
        protected string classname =>
     new CssBuilder()
         .AddClass("dropdown", !IsGroup)
         .AddClass("btn-group", IsGroup)
         .AddClass(DropdownDirection.ToDescriptionString(), DropdownDirection != DropdownDirection.Down)
         .AddClass("show", !_manual && DropDownMenuControl.Handler.IsOpen(DropDownMenuControl.Id))
         .AddClass("show", _manual && IsOpen.HasValue && IsOpen.Value)
         .AddClass(Class)
     .Build();

        [Parameter] protected DropdownDirection DropdownDirection { get; set; } = DropdownDirection.Down;
        [Parameter] protected bool IsGroup { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
        internal DropDownMenuControl DropDownMenuControl { get; set; } = new DropDownMenuControl();

        protected override void OnInit()
        {
            DropDownMenuControl.Handler.OnToggle += OnToggle;
            DropDownMenuControl.Id = Guid.NewGuid().ToString();
            DropDownMenuControl.Handler.AddDropDownMenu(DropDownMenuControl.Id);
            base.OnInit();
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
            if (DropDownMenuControl != null)
            {
                if (DropDownMenuControl.Handler.IsOpen(DropDownMenuControl.Id))
                {
                    Invoke(() => DropDownMenuControl.Handler.Toggle(DropDownMenuControl.Id));
                }
            }
        }

        private void OnToggle(Object Sender, EventArgs e)
        {
            DropDownMenuControl.Handler = DropDownMenuControl.Handler;
            StateHasChanged();
        }
    }
}
