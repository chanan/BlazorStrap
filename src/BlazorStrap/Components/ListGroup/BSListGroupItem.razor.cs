using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSListGroupItem : BootstrapComponentBase
    {
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

        protected string IsButton => Tag == "button" ? "button" : "";

        [Parameter] protected bool IsActive { get; set; }
        [Parameter] protected bool IsDisabled { get; set; }
        [Parameter] protected ListGroupType ListGroupType { get; set; } = ListGroupType.List;
        [Parameter] protected EventCallback<UIMouseEventArgs> OnClick { get; set; }
        [Parameter] protected Color Color { get; set; } = Color.None;
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}