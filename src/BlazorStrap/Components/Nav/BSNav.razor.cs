using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public partial class BSNav : ComponentBase, IDisposable
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        private BSNavItem _selected;
        private bool _disposed;
        internal List<BSNavItem> Navitems { get; set; } = new List<BSNavItem>();
        public BSNavItem Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                InvokeAsync(StateHasChanged);
            }
        }
        protected string Classname =>
        new CssBuilder()
            .AddClass("nav", !RemoveDefaultClass)
            .AddClass("navbar-nav", IsNavbar)
            .AddClass("nav-tabs", IsTabs)
            .AddClass("nav-pills", IsPills)
            .AddClass("nav-fill", IsFill)
            .AddClass("flex-column", IsVertical)
            .AddClass(GetAlignment())
            .AddClass(Class)
        .Build();

        protected string Tag => IsList ? "ul" : "nav";

        [Parameter] public bool IsList { get; set; }
        [Parameter] public bool RemoveDefaultClass { get; set; }
        [Parameter] public Alignment Alignment { get; set; } = Alignment.None;
        [Parameter] public bool IsVertical { get; set; }
        [Parameter] public bool IsTabs { get; set; }
        [Parameter] public bool IsPills { get; set; }
        [Parameter] public bool IsFill { get; set; }
        [Parameter] public bool IsNavbar { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        private string GetAlignment()
        {
            return Alignment == Alignment.Center ? "justify-content-center" : Alignment == Alignment.Right ? "justify-content-end" : null;
        }

        protected override void OnInitialized()
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _selected?.Dispose();
            }
        }
    }
}
