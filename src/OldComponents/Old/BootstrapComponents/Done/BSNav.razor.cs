using BlazorComponentUtilities;
using BlazorStrap.Components.Base;
using BlazorStrap.InternalComponents;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSNav : LayoutBase
    {
        /// <summary>
        /// Adds the <c>nav-fill</c> class.
        /// </summary>
        [Parameter] public bool IsFill { get; set; }

        /// <summary>
        /// Adds the <c>nav-justified</c> class.
        /// </summary>
        [Parameter] public bool IsJustified { get; set; }

        /// <summary>
        /// Renders using a HTML &lt;Nav&gt; element instead of a &lt;ul&gt;
        /// </summary>
        [Parameter] public bool IsNav { get; set; }

        /// <summary>
        /// Adds the <c>nav-pills</c> class to render as pills.
        /// </summary>
        [Parameter] public bool IsPills { get; set; }

        /// <summary>
        /// Adds the <c>nav-tabs</c> class to render as tabs.
        /// </summary>
        [Parameter] public bool IsTabs { get; set; }

        /// <summary>
        /// Displays nav items vertically.
        /// </summary>
        [Parameter] public bool IsVertical { get; set; }

        /// <summary>
        /// Alignment of content.
        /// </summary>
        [Parameter] public Justify Justify { get; set; }

        /// <summary>
        /// Removes the <c>navbar-nav</c> class.
        /// </summary>
        [Parameter] public bool NoNavbarNav { get; set; }

        /// <summary>
        /// Removes the <c>nav</c> class.
        /// </summary>
        [Parameter] public bool NoNav { get; set; }

        [CascadingParameter] public BSNavbar? Navbar { get; set; }
        internal BSNavItem? ActiveChild { get; set; }

        private string? ClassBuilder => new CssBuilder()
            .AddClass("nav", Navbar == null && !NoNav)
            .AddClass("navbar-nav", Navbar != null && !NoNavbarNav)
            .AddClass("nav-tabs", IsTabs)
            .AddClass("nav-pills", IsPills)
            .AddClass("flex-column", IsVertical)
            .AddClass("nav-fill", IsFill)
            .AddClass("nav-justified", IsJustified)
            .AddClass($"justify-content-{Justify.NameToLower()}", Justify != Justify.Default)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        internal bool HasTabError { get; set; }

        private TabContentRender? TabRender { get; set; }

        public bool SetFirstChild(BSNavItem sender)
        {
            if (ActiveChild != null) return false;
            ActiveChild = sender;
            return true;
        }

        public void Invoke(BSNavItem sender)
        {
            ActiveChild = sender;
            if (IsTabs)
            {
                TabRender?.Render();
            }

            ChildHandler?.Invoke(sender);
        }

        internal event Action<BSNavItem>? ChildHandler;

        internal void Rerender()
        {
            StateHasChanged();
        }
    }
}