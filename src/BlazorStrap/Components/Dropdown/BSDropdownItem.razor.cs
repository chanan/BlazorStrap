using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Routing;
using BlazorStrap.Util;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public abstract class BSDropdownItemBase : ComponentBase, IDisposable
    {
        [Inject] private NavigationManager UriHelper { get; set; }

        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
               new CssBuilder()
                   .AddClass("dropdown-divider", IsDivider)
                   .AddClass("dropdown-item", !IsDivider)
                   .AddClass("active", !IsDivider && IsActive)
                   .AddClass("disabled", !IsDivider && IsActive)
                   .AddClass(Class)
               .Build();

        protected string Tag
        {
            get
            {
                if (IsDivider) { return "div"; }
                if (IsButton) { return "button"; }
                else { return "a"; }
            }
        }
        protected string Type
        {
            get
            {
                if (IsButton) { return "button"; }
                else { return null; }
            }
        }
        internal bool HasSubMenu {get;set;}
        [Parameter] public bool IsDivider { get; set; }
        [Parameter] public bool IsDisabled { get; set; }
        [Parameter] public bool IsButton { get; set; }
        [Parameter] public bool IsActive { get; set; }
        [Parameter] public bool StayOpen { get; set; }
        [Parameter] public string Href { get; set; } = "javascript:void(0)";
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [CascadingParameter] internal BSDropdown DropDown { get; set; }

        protected void onClickEvent(MouseEventArgs e)
        { 
            if (OnClick.HasDelegate)
            {
                OnClick.InvokeAsync(e);
            }
            if (!StayOpen && DropDown?.IsSubmenu == false && !HasSubMenu)
            {
                DropDown.Selected = null;
            }
        }

        protected override void OnInitialized()
        {
            UriHelper.LocationChanged += OnLocationChanged;
            OnLocationChanged(this, new LocationChangedEventArgs(UriHelper.Uri, true));
        }
        public void Dispose()
        {
            UriHelper.LocationChanged -= OnLocationChanged;
        }

        public void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {

            var active = e.Location.MatchActiveRoute(UriHelper.BaseUri + Href);
            if (active != IsActive)
            {
                if (DropDown != null)
                {
                    DropDown.Active = active;
                }
                IsActive = active;
            }
        }
    }
}
