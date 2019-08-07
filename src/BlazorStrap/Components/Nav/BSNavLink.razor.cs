using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using BlazorStrap.Util;
using Microsoft.AspNetCore.Components.Routing;

namespace BlazorStrap
{
    public class CodeBSNavLink : ComponentBase, IDisposable
    {
        [Inject] private IUriHelper UriHelper { get; set; }
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        [CascadingParameter] BSNavItem Parent { get; set; }
        protected string classname =>
        new CssBuilder("nav-item nav-link")
            .AddClass("active", IsActive)
            .AddClass("disabled", IsDisabled)
            .AddClass(Class)
        .Build();

        protected string disabled => IsDisabled ? "disabled" : null;

        [Parameter] protected bool IsActive { get; set; }
        [Parameter] protected bool IsDisabled { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected string Href { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }

        protected override void OnInit()
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
                if (Parent != null)
                {
                    Parent.Active = active;
                }
                IsActive = active;
                StateHasChanged();
            }
        }
    }
}
