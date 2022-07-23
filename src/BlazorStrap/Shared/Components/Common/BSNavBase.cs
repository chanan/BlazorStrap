using BlazorStrap.InternalComponents;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [CascadingParameter] public BSNavbarBase? Navbar { get; set; }
        public BSNavItemBase? ActiveChild { get; set; }

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
        internal bool HasTabError { get; set; }

        protected TabContentRender? TabRender { get; set; }

        public bool SetFirstChild(BSNavItemBase sender)
        {
            if (ActiveChild != null) return false;
            ActiveChild = sender;
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
        }

        internal event Action<BSNavItemBase>? ChildHandler;

        internal void Rerender()
        {
            StateHasChanged();
        }
    }
}
