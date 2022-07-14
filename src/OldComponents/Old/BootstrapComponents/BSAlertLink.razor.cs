using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorComponentUtilities;
using BlazorStrap.Utilities;

namespace BlazorStrap
{
    public partial class BSAlertLink
    {
        /// <summary>
        /// Url to link to.
        /// </summary>
        [Parameter] public string? Url { get; set; }
    }
}