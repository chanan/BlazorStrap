using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Threading.Tasks;

namespace BlazorStrap 
{
    public class CodeBSCollapseItem : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        internal CodeBSCollapse Collapse { get; set; }

        private bool _active = false;
        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                if (value)
                {
                    Collapse?.Show();
                }
                StateHasChanged();
            }
        }
    }
}
