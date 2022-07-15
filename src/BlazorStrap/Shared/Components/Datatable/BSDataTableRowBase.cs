using BlazorStrap.Shared.Components.Content;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Datatable
{
    public abstract class BSDataTableRowBase : BSTRBase
    {
        /// <summary>
        /// Hides the row.
        /// </summary>
        [Parameter]
        public bool IsHidden { get; set; } = false;
    }
}
