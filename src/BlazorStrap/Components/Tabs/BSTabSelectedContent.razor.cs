using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSTabSelectedContentBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [CascadingParameter] protected BSTabGroup Group { get; set; }

        protected override void OnInitialized()
        {
            if (Group != null)
            {

            }
        }
    }
}
