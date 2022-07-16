using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSListGroupBase<TSize> : BlazorStrapBase
    {
        /// <summary>
        /// Adds the list-group-flush class.
        /// See <see href="https://getbootstrap.com/docs/5.2/components/list-group/#flush">Bootstrap Documentation</see>
        /// </summary>
        [Parameter] public bool IsFlush { get; set; }

        /// <summary>
        /// Adds the list-group-horizontal class.
        /// See <see href="https://getbootstrap.com/docs/5.2/components/list-group/#horizontal">Bootstrap Documentation</see>
        /// </summary>
        [Parameter] public bool IsHorizontal { get; set; }

        /// <summary>
        /// Adds the list-group-numbered class.
        /// See <see href="https://getbootstrap.com/docs/5.2/components/list-group/#numbered">Bootstrap Documentation</see>
        /// </summary>
        [Parameter] public bool IsNumbered { get; set; }

        /// <summary>
        /// Sets size of list group. Only used when <see cref="IsHorizontal"/> is true.
        /// </summary>
        [Parameter] public TSize Size { get; set; } = (TSize)(object)0;
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
    }
}
