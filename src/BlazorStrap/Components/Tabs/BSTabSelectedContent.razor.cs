﻿using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSTabSelectedContent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        [CascadingParameter] protected BSTabGroup Group { get; set; }

        protected override void OnInit()
        {
            if(Group != null)
            {

            }
        }
    }
}
