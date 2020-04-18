using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Extensions.DataComponents
{
    public partial class BSDataListGroup<TItem>
    {
        // Private Variables
        private bool _isLoading => Items == null;
        private bool _hasNoData => Items.Count == 0;

        /// <summary>
        /// Data model to pass into the component
        /// </summary>
        [Parameter] public List<TItem> Items { get; set; }

        /// <summary>
        /// This template loops through each row of your data and adds it to the List Group
        /// </summary>
        [Parameter] public RenderFragment<TItem> ItemTemplate { get; set; }

        /// <summary>
        /// This template will be displayed if the list of your data is null, or not initialized.
        /// It will be displayed instead of the List Group.
        /// </summary>
        [Parameter] public RenderFragment LoadingTemplate { get; set; }

        /// <summary>
        /// This template will be displayed if the list of your data has no items.
        /// It will be displayed instead of the List Group.
        /// </summary>
        [Parameter] public RenderFragment NoDataTemplate { get; set; }

        // Pass through properties for the BSListGroup
        [Parameter] public ListGroupType ListGroupType { get; set; } = ListGroupType.List;
        [Parameter] public bool IsFlush { get; set; }
        [Parameter] public string Class { get; set; }
    }

}
