using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSNavbarBrandBase : BlazorStrapBase
    {
        /// <summary>
        /// Url for brand link. See <see href="https://getbootstrap.com/docs/5.2/components/navbar/#brand">Bootstrap Documentation</see>
        /// </summary>
        [Parameter] public string? Url { get; set; }
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
    }
}
