using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorStrap
{
    public partial class BSPaginationLink : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string Classname =>
        new CssBuilder("page-link")
            .AddClass(Class)
        .Build();

        [Parameter] public string Href { get; set; }
        [Parameter] public bool OnClickStopPropagation { get; set; } = false;
        [Parameter] public bool OnClickPreventDefault { get; set; } = false;

        [Parameter] public PaginationLinkType PaginationLinkType { get; set; } = PaginationLinkType.Custom;
        private string _label
        {
            get
            {
                if (PaginationLinkType == PaginationLinkType.NextIcon || PaginationLinkType == PaginationLinkType.NextText) { return "Next"; }
                if (PaginationLinkType == PaginationLinkType.PreviousIcon || PaginationLinkType == PaginationLinkType.PreviousText) { return "Previous"; }
                return null;
            }
        }
        private string _sr
        {
            get
            {
                if (PaginationLinkType == PaginationLinkType.NextIcon || PaginationLinkType == PaginationLinkType.NextText) { return "Next"; }
                if (PaginationLinkType == PaginationLinkType.PreviousIcon || PaginationLinkType == PaginationLinkType.PreviousText) { return "Previous"; }
                return null;
            }
        }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
