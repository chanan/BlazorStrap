using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSNav : BootstrapComponentBase
    {
        protected private string classname =>
        new CssBuilder("nav")
            .AddClass("navbar-nav", IsNavbar)
            .AddClass("nav-tabs", IsTabs)
            .AddClass("nav-pills", IsPills)
            .AddClass("nav-fill", IsFill)
            .AddClass("flex-column", IsVertical)
            .AddClass(GetAlignment())
            .AddClass(Class)
        .Build();

        protected string Tag => IsList ? "ul" : "nav";

        private bool _isList;
        [Parameter]
        private bool IsList
        {
            get
            {
                return _isList;
            }
            set
            {
                _isList = value;
                BlazorNavValues.IsList = value;
            }
        }
        [Parameter] protected Alignment Alignment { get; set; } = Alignment.None;
        [Parameter] protected bool IsVertical { get; set; }
        [Parameter] protected bool IsTabs { get; set; }
        [Parameter] protected bool IsPills { get; set; }
        [Parameter] protected bool IsFill { get; set; }
        [Parameter] protected bool IsNavbar { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
        internal BlazorNavValues BlazorNavValues { get; set; } = new BlazorNavValues { DropDownMenuHandler = new DropDownMenuHandler() };

        private string GetAlignment()
        {
            if (Alignment == Alignment.Center) { return "justify-content-center"; }
            if (Alignment == Alignment.Right) { return "justify-content-end"; }
            return null;
        }

        protected override void OnInit()
        {
            BlazorNavValues.DropDownMenuHandler.OnToggle += OnToggle;
            base.OnInit();
        }
        private void OnToggle(Object Sender, EventArgs e)
        {
            StateHasChanged();
        }
    }
}
