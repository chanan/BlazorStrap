using System.Collections.Immutable;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSDataTableHead : BSTD
    {
        [Parameter] public string Column { get; set; }
        [Parameter] public bool Sortable { get; set; }
        [Parameter] public bool ColumnFilter { get; set; }
    }
}