using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSPaginationLink : BootstrapComponentBase
    {
        protected string classname =>
        new CssBuilder("page-link")
            .AddClass(Class)
        .Build();

        [Parameter] protected string Href { get; set; }
        [Parameter] protected PaginationLinkType PaginationLinkType { get; set; } = PaginationLinkType.Custom;
        private string label
        {
            get
            {
                if (PaginationLinkType == PaginationLinkType.NextIcon || PaginationLinkType == PaginationLinkType.NextText) { return "Next"; }
                if (PaginationLinkType == PaginationLinkType.PreviousIcon || PaginationLinkType == PaginationLinkType.PreviousText) { return "Previous"; }
                return null;
            }
        }
        private string sr
        {
            get
            {
                if (PaginationLinkType == PaginationLinkType.NextIcon || PaginationLinkType == PaginationLinkType.NextText) { return "Next"; }
                if (PaginationLinkType == PaginationLinkType.PreviousIcon || PaginationLinkType == PaginationLinkType.PreviousText) { return "Previous"; }
                return null;
            }
        }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
