using BlazorComponentUtilities;
using BlazorStrap.Util;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSDropdownItemBase : ComponentBase, IDisposable
    {
        [Inject] protected NavigationManager UriHelper { get; set; }

        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }

        protected string Classname =>
               new CssBuilder()
                   .AddClass("dropdown-divider", IsDivider)
                   .AddClass("dropdown-item", !IsDivider)
                   .AddClass("active", !IsDivider && IsActive)
                   .AddClass("disabled", !IsDivider && IsActive)
                   .AddClass(Class)
               .Build();

        protected string Tag => IsDivider ? "div" : IsButton ? "button" : "a";

        protected string Type => IsButton ? "button" : null;

        internal bool HasSubMenu { get; set; }
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

        protected void OnClickEvent(MouseEventArgs e)
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

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                UriHelper.LocationChanged -= OnLocationChanged;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            var active = e?.Location.MatchActiveRoute(UriHelper.BaseUri + Href) ?? false;
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
