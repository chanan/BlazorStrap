using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSDataTableRow : BSTR
    {
        /// <summary>
        /// Hides the row.
        /// </summary>
        [Parameter]
        public bool IsHidden { get; set; } = false;

        private string? ClassBuilder => new CssBuilder()
          .AddClass("table-active", IsActive)
          .AddClass($"table-{Color.NameToLower()}", Color != BSColor.Default)
          .AddClass($"align-{AlignRow.NameToLower()}", AlignRow != AlignRow.Default)
          .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
          .AddClass(Class, !string.IsNullOrEmpty(Class))
          .Build().ToNullString();
    }
}
