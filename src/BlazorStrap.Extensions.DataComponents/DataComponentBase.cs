using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Components;


namespace BlazorStrap.Extensions.DataComponents
{
    /// <summary>
    /// Base class for adding Data Components to BlazorStrap.
    /// Contains code for Loading Templates, No Data Templates, and Item Templates.
    /// It should be used to avoid violation of DRY Principle and helps to keep the code clean
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public abstract class DataComponentBase<TItem> : ComponentBase
    {
        // Private Variables
        protected bool IsLoading => Items == null;
        protected bool HasNoData => Items.Count == 0;

        /// <summary>
        /// Data model to pass into the component
        /// </summary>
        [Parameter] public List<TItem> Items { get; set; }

        /// <summary>
        /// This template loops through each row of your data and adds it to the Carousel
        /// </summary>
        [Parameter] public RenderFragment<TItem> ItemTemplate { get; set; }

        /// <summary>
        /// This template will be displayed if the list of your data is null, or not initialized.
        /// </summary>
        [Parameter] public RenderFragment LoadingTemplate { get; set; }

        /// <summary>
        /// This template will be displayed if the list of your data has no items.
        /// </summary>
        [Parameter] public RenderFragment NoDataTemplate { get; set; }
    }
}
