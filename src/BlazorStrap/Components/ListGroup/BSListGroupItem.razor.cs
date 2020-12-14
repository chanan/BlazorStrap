using BlazorComponentUtilities;
using BlazorStrap.Util;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace BlazorStrap
{
    public partial class BSListGroupItem : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string Classname =>
        new CssBuilder("list-group-item")
            .AddClass($"list-group-item-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass("active", IsActive)
            .AddClass("disabled", IsDisabled && ListGroupType != ListGroupType.Button)
            .AddClass("list-group-item-action", ListGroupType != ListGroupType.List && 
                                                (ListGroupType == ListGroupType.Button || 
                                                ListGroupType == ListGroupType.Link))
            .AddClass(Class)
        .Build();

        protected string Tag => ListGroupType switch
        {
            ListGroupType.Button => "button",
            ListGroupType.Link => "a",
            ListGroupType.List => "li",
            _ => "li"
        };

        protected string href => ListGroupType == ListGroupType.Link ? "javascript:void(0)" : null;
        protected string IsButton => Tag == "button" ? "button" : "";

        [Parameter] public bool IsActive { get; set; }
        [Parameter] public bool IsDisabled { get; set; }
        [Parameter] public string Href { get; set; }
        [Parameter] public ListGroupType ListGroupType { get; set; } = ListGroupType.List;
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public Color Color { get; set; } = Color.None;
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}