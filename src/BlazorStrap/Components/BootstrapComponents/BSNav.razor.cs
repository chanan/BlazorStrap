using BlazorComponentUtilities;
using BlazorStrap.InternalComponents;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSNav : BlazorStrapBase
    {
        [Parameter] public bool IsFill { get; set; }
        [Parameter] public bool IsJustified { get; set; }
        [Parameter] public bool IsNav { get; set; }
        [Parameter] public bool IsPills { get; set; }
        [Parameter] public bool IsTabs { get; set; }
        [Parameter] public bool IsVertical { get; set; }
        [Parameter] public Justify Justify { get; set; }
        [Parameter] public bool NoNavbarNav { get; set; }
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

        internal TabContentRender? TabRender { get; set; }

        public bool SetFirstChild(BSNavItem sender)
        {
            if (ActiveChild == null)
            {
                ActiveChild = sender;
                return true;
            }
            return false;
        }

        public void Invoke(BSNavItem sender)
        {
            ActiveChild = sender;
            if(IsTabs)
            {
                TabRender?.Render();
            }

            ChildHandler?.Invoke(sender);
        }

        internal event Action<BSNavItem>? ChildHandler;

        internal void Rerender ()
        {
            StateHasChanged();
        }
    }
}