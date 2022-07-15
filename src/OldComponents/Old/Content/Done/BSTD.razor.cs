using BlazorComponentUtilities;
using BlazorStrap.Components.Base;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSTD : LayoutBase
    {
        /// <summary>
        /// Cell content vertical alignment.
        /// </summary>
        [Parameter] public AlignRow AlignRow { get; set; }

        /// <summary>
        /// Cell background color.
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        /// <summary>
        /// Colspan of the cell.
        /// </summary>
        [Parameter] public string? ColSpan { get; set; }

        /// <summary>
        /// Whether or not the cell is active.
        /// </summary>
        [Parameter] public bool IsActive { get; set; }

        internal string? ClassBuilder => new CssBuilder()
          .AddClass("table-active", IsActive)
          .AddClass($"table-{Color.NameToLower()}", Color != BSColor.Default)
          .AddClass($"align-{AlignRow.NameToLower()}", AlignRow != AlignRow.Default)
          .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
          .AddClass(Class, !string.IsNullOrEmpty(Class))
          .Build().ToNullString();

        [CascadingParameter] internal BSTHead? TableHead { get; set; }
    }
}
