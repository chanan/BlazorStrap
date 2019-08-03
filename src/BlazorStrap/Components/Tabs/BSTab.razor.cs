using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSTab : BootstrapComponentBase, IDisposable
    {
        public RenderFragment Content { get; set; }
        public bool Selected
        {
            get
            {
                return (Group != null) ? Group.Selected == this : false;
            }
        }
        protected string classname =>
        new CssBuilder("nav-item")
            .AddClass("active", Selected)
            .AddClass(Class)
        .Build();

        protected string Tag => Parent.IsList ? "li" : "a";
        [CascadingParameter] protected BSTabGroup Group { get; set; }
        [CascadingParameter] protected BSTabList Parent { get; set; }
        [Parameter] public string Id { get; set; } = Guid.NewGuid().ToString();
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }



        protected override Task OnInitAsync()
        {
            Group.Tabs.Add(this);
            if (Group.Selected == null)
            {
                Group.Selected = this;
            }
            return base.OnInitAsync();
        }

        public void Select()
        {
            Group.Selected = this;
        }
        public void UpdateContent()
        {
            Group.Tabs.First(q => q == this).Content = Content;
            if (this == Group.Selected)
            {
                Group.Selected = null;
                Group.Selected = this;
                StateHasChanged();
            }
        }

        public void Dispose()
        {
            Group.Tabs.Remove(this);
            Group.Selected = (Group.Selected == this) ? Group.Tabs.FirstOrDefault() : Group.Selected;
        }
    }
}