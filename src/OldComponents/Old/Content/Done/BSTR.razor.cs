using BlazorComponentUtilities;
using BlazorStrap.Components.Base;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSTR : LayoutBase
    {
        /// <summary>
        /// Content alignment within the row.
        /// </summary>
        [Parameter] public AlignRow AlignRow { get; set; }

        /// <summary>
        /// Row background color.
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        /// <summary>
        /// Whether or not the row is active.
        /// </summary>
        [Parameter] public bool IsActive { get; set; }

        private string? ClassBuilder => new CssBuilder()
          .AddClass("table-active", IsActive)
          .AddClass($"table-{Color.NameToLower()}", Color != BSColor.Default)
          .AddClass($"align-{AlignRow.NameToLower()}", AlignRow != AlignRow.Default)
          .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
          .AddClass(Class, !string.IsNullOrEmpty(Class))
          .Build().ToNullString();
    }
}
