using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Extensions.DataComponents
{
    public partial class BSDataTable<TItem> : DataComponentBase<TItem>
    {
        // Original BSTable attributes to pass on.
        [Parameter] public bool IsDark { get; set; }
        [Parameter] public bool IsStriped { get; set; }
        [Parameter] public bool IsBordered { get; set; }
        [Parameter] public bool IsBorderless { get; set; }
        [Parameter] public bool IsHoverable { get; set; }
        [Parameter] public bool IsSmall { get; set; }
        [Parameter] public bool IsResponsive { get; set; }
        [Parameter] public string Class { get; set; }

        // Render Fragments

        /// <summary>
        /// For adding header definitions to the Table.
        /// It will be displayed in the head of the table.
        /// </summary>
        [Parameter] public RenderFragment HeaderTemplate { get; set; }

        /// <summary>
        /// For adding a footer to the table.
        /// It will be displayed in the foot section of the table.
        /// </summary>
        [Parameter] public RenderFragment FooterTemplate { get; set; }
    }
}
