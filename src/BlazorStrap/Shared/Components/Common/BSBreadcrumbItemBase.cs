using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSBreadcrumbItemBase : BlazorStrapBase
    {
        /// <summary>
        /// Whether or not the breadcrumb item is active.
        /// </summary>
        [Parameter] public bool IsActive { get; set; }

        /// <summary>
        /// Link for breadcrumb item.
        /// </summary>
        [Parameter] public string? Url { get; set; }
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
    }
}
