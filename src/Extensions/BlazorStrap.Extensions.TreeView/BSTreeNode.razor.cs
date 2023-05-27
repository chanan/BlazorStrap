using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorStrap.Extensions.TreeView
{
    public partial class BSTreeNode : ComponentBase
    {
        protected bool IsRoot { get; set; }
        [Parameter] public RenderFragment? ChildContent { get; set; }
        [CascadingParameter] public BSTree? Root { get; set; } 
        [CascadingParameter] public BSTreeItem? Parent { get; set; }
        public bool IsOpen { get; set; }
        
        protected async override Task OnInitializedAsync()
        {
            if(Root == null)
            {
                throw new System.Exception("BSTreeNode must be a child of BSTree");
            }
            if (Parent == null)
            {
                IsRoot = true;
            }
            else
            {
                Parent.Child = this;
                await Parent.ChildSetAsync();
            }
            if (Root.IsExpanded)
            {
                IsOpen = true;
            }
        }
    }
}
