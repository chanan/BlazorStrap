using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap.Extensions.BSDataTable
{
    public partial class BSDataListGroupItem<TItem>
    {
        /// <summary>
        /// Data model to pass into the component
        /// </summary>
        [Parameter] public TItem Item { get; set; }

        // Pass through properties for the BSListGroupItem
        [Parameter] public bool IsActive { get; set; }
        [Parameter] public bool IsDisabled { get; set; }
        [Parameter] public string Href { get; set; }
        [Parameter] public ListGroupType ListGroupType { get; set; } = ListGroupType.List;
        [Parameter] public EventCallback OnClick { get; set; }
        [Parameter] public Color Color { get; set; } = Color.None;
        [Parameter] public string Class { get; set; }
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Parameter] public RenderFragment<TItem> ChildContent { get; set; }

        // OnClick Handler
        public void HandleOnClick(MouseEventArgs args)
        {
            Console.WriteLine("BSDataListGroupItem_HandleOnClick");
            OnClick.InvokeAsync(Item);
        }
    }
}
