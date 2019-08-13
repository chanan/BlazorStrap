using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Routing;
using BlazorStrap.Util;

namespace BlazorStrap
{
    public class CodeBSDropdownItem : ComponentBase, IDisposable
    {
        [Inject] private IUriHelper UriHelper { get; set; }

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
        [Parameter] public EventCallback<UIMouseEventArgs> OnClick { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [CascadingParameter] internal CodeBSDropdown DropDown { get; set; }

        protected void onClickEvent(UIMouseEventArgs e)
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
            UriHelper.OnLocationChanged += OnLocationChanged;
            OnLocationChanged(this, new LocationChangedEventArgs(UriHelper.GetAbsoluteUri(), true));
        }
        public void Dispose()
        {
            UriHelper.OnLocationChanged -= OnLocationChanged;
        }

        public void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {

            var active = e.Location.MatchActiveRoute(UriHelper.GetBaseUri() + Href);
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
