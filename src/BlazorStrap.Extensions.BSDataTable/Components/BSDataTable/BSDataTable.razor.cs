using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorStrap.Extensions
{
    public partial class BSDataTable<TItem>
    {
        /// <summary>
        /// Data model to pass into the component.
        /// </summary>
        [Parameter] public List<TItem> Items { get; set; }

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
        /// This template loops through each row of your data and adds it to the table.
        /// It will be displayed in the body of the table.
        /// </summary>
        [Parameter] public RenderFragment<TItem> ItemTemplate { get; set; }

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

        /// <summary>
        /// This template will be displayed if the list of your data is null, or not initialized.
        /// It will be displayed in the body of the table.
        /// </summary>
        [Parameter] public RenderFragment LoadingTemplate { get; set; }

        /// <summary>
        /// This template will be displayed if the list of your data has no items.
        /// It will be displayed in the body of the table
        /// </summary>
        [Parameter] public RenderFragment NoDataTemplate { get; set; }

        // Private Variables
        private bool _isLoading => Items == null;
        private bool _hasNoData => Items.Count == 0;

    }
}
