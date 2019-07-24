using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSListGroup : BootstrapComponentBase
    {
        protected string classname =>
        new CssBuilder()
            .AddClass("list-group list-group-flush", IsFlush)
            .AddClass("list-group", !IsFlush)
            .AddClass(Class)
        .Build();

        protected string Tag => ListGroupType == ListGroupType.List ? "ul" : "div";

        [Parameter] protected ListGroupType ListGroupType { get; set; } = ListGroupType.List;
        [Parameter] protected bool IsFlush { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
