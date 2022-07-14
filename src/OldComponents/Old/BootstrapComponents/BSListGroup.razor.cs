using BlazorComponentUtilities;
using BlazorStrap.Bootstrap.V5_1.Enums;
using BlazorStrap.Components.Base;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSListGroup : LayoutBase
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
        [Parameter] public Size Size { get; set; } = Size.None;

        private string? ClassBuilder => new CssBuilder("list-group")
          .AddClass($"list-group-flush", IsFlush)
          .AddClass($"list-group-numbered", IsNumbered)
          .AddClass($"list-group-horizontal", IsHorizontal && Size == Size.None)
          .AddClass($"list-group-horizontal-{Size.ToDescriptionString()}", IsHorizontal && Size != Size.None)
          .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
          .AddClass(Class, !string.IsNullOrEmpty(Class))
          .Build().ToNullString();
    }
}