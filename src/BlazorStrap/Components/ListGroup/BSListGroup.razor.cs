using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSListGroupBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string Classname =>
        new CssBuilder()
            .AddClass("list-group list-group-flush", IsFlush)
            .AddClass("list-group", !IsFlush)
            .AddClass(Class)
        .Build();

        protected string Tag => ListGroupType == ListGroupType.List ? "ul" : "div";

        [Parameter] public ListGroupType ListGroupType { get; set; } = ListGroupType.List;
        [Parameter] public bool IsFlush { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
