using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class BSListGroupItemBase  : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder("list-group-item")
            .AddClass($"list-group-item-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass("active", IsActive)
            .AddClass("disabled", IsDisabled && ListGroupType != ListGroupType.Button)
            .AddClass(Class)
        .Build();

        protected string Tag => ListGroupType switch
        {
            ListGroupType.Button => "button",
            ListGroupType.Link => "a",
            ListGroupType.List => "li",
            _ => "li"
        };

        protected string href =>  ListGroupType == ListGroupType.Link ? "javascript:void(0)" : null;
        protected string IsButton => Tag == "button" ? "button" : "";

        [Parameter] public bool IsActive { get; set; }
        [Parameter] public bool IsDisabled { get; set; }
        [Parameter] public ListGroupType ListGroupType { get; set; } = ListGroupType.List;
        [Parameter] public EventCallback<UIMouseEventArgs> OnClick { get; set; }
        [Parameter] public Color Color { get; set; } = Color.None;
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}