using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSNavbarBase : BlazorStrapBase
    {
        /// <summary>
        /// Removes the css <c>nav</c> class.
        /// </summary>
        [Parameter] public bool NoNavbarClass { get; set; }

        /// <summary>
        /// Sets navbar color.
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;


        /// <summary>
        /// Sets the navbar to be dark.
        /// </summary>
        [Parameter] public bool IsDark { get; set; }

        /// <summary>
        /// Adds the <c>fixed-bottom</c> class to the nav bar.
        /// </summary>
        [Parameter] public bool IsFixedBottom { get; set; }

        /// <summary>
        /// Adds the <c>fixed-top</c> class to the nav bar.
        /// </summary>
        [Parameter] public bool IsFixedTop { get; set; }

        /// <summary>
        /// Uses the HTML &lt;Header&gt; element instead of the &lt;Nav&gt; element.
        /// </summary>
        [Parameter] public bool IsHeader { get; set; }

        /// <summary>
        /// Sets the navbar to be top sticky.
        /// </summary>
        [Parameter] public bool IsStickyTop { get; set; }

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
    }
}
