using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Extensions.TreeView
{
    public partial class BSTreeNode : ComponentBase
    {
        protected bool IsRoot { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [CascadingParameter] public BSTree Root { get; set; }
        [CascadingParameter] public BSTreeItem Parent { get; set; }
        public bool IsOpen { get; set; }

        protected override void OnInitialized()
        {
            if(Parent == null)
            {
                IsRoot = true;
            }
            else
            {
                Parent.Child = this;
                Parent.ChildSet();
            }
            if(Root.IsExpanded)
            {
                IsOpen = true;
            }
        }

    

    }
}
