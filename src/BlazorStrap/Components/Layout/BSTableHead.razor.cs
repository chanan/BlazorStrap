using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSTableHead : BootstrapComponentBase
    {
        protected string classname =>
             new CssBuilder()
                 .AddClass("thead-light", TableHeadType == TableHeadType.Light)
                 .AddClass("thead-dark", TableHeadType == TableHeadType.Dark)
                 .AddClass(Class)
             .Build();

        [Parameter] protected TableHeadType TableHeadType { get; set; } = TableHeadType.None;
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
