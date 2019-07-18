using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSLabel : ColumnBase
    {
        protected string classname =>
        new CssBuilder()
           .AddClass("form-check-label", IsCheck)
           .AddClass("col-form-label", GetColumnClass(null) != null)
           .AddClass(GetColumnClass(null))
           .AddClass(Class)
        .Build();

        [Parameter] protected bool IsCheck { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
