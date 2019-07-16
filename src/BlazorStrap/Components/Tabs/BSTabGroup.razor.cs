using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSTabGroup : BootstrapComponentBase
    {
        public List<CodeBSTab> Tabs = new List<CodeBSTab>();
        private CodeBSTab _selected;
        public CodeBSTab Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                StateHasChanged();
            }
        }

        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
