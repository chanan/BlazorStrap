using Microsoft.AspNetCore.Components;
using System;

namespace BlazorStrap.Extensions.DataComponents
{
    public partial class RemotePagination : ComponentBase
    {
        [Parameter] public int Page { get; set; } = 1;
        [Parameter] public int TotalRecords { get; set; }
        [Parameter] public int RecordsPerPage { get; set; } = 50;
        [Parameter] public bool IsLoading { get; set; }
        [Parameter] public bool HasNoData { get; set; }
        [Parameter] public string UrlPattern { get; set; }

        private int Pages => (int)Math.Ceiling((float)TotalRecords / RecordsPerPage);
        private bool IsPreviousDisabled => Page == 1;
        private bool IsNextDisabled => Page == Pages;

        /// <summary>
        /// Replace variables in UrlPattern to create a url to a page.
        /// In the future other variables can be added such as sort and filter
        /// 
        /// Variables are:
        /// 
        /// {page} - will get replaced with page number
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private string GetUrl(int page)
        {
            return UrlPattern.Replace("{page}", page.ToString());
        }
    }
}
