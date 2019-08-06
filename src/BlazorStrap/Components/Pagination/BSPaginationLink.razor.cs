using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSPaginationLink : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
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
