using BlazorStrap.InternalComponents;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSNavBase : BlazorStrapBase
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
        /// Removes the <c>navbar-nav</c> class.
        /// </summary>
        [Parameter] public bool NoNavbarNav { get; set; }

        /// <summary>
        /// Removes the <c>nav</c> class.
        /// </summary>
        [Parameter] public bool NoNav { get; set; }
        /// <summary>
        /// Event Callback for when a new tab is selected
        /// </summary>
        [Parameter] public EventCallback<RenderFragment> OnTabChange { get; set; }
        [CascadingParameter] public BSTabWrapperBase? TabWrapper { get; set; }
        
        [CascadingParameter] public BSNavbarBase? Navbar { get; set; }
        public BSNavItemBase? ActiveChild { get; set; }

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
        internal bool HasTabError { get; set; }

        protected TabContentRender? TabRender { get; set; }

        protected override void OnInitialized()
        {
            if (TabWrapper != null) TabWrapper.Nav = this;
        }

        public bool SetFirstChild(BSNavItemBase sender)
        {
            if (ActiveChild != null) return false;
            ActiveChild = sender;
            
            ChildHandler?.Invoke(sender);

            if (OnTabChange.HasDelegate)
            {
                OnTabChange.InvokeAsync(ActiveChild.TabContent);
            }

            return true;
        }

        public void Invoke(BSNavItemBase sender)
        {
            ActiveChild = sender;
            if (IsTabs)
            {
                TabRender?.Render();
            }

            ChildHandler?.Invoke(sender);
            if (OnTabChange.HasDelegate)
                OnTabChange.InvokeAsync(ActiveChild.TabContent);
        }

        public event Action<BSNavItemBase>? ChildHandler;
        public event Action<string> ChildHandlerString;

        public void SetActiveTab(string tabId)
        {
            ChildHandlerString?.Invoke(tabId);
        }

        internal void Rerender()
        {
            StateHasChanged();
        }
    }
}
