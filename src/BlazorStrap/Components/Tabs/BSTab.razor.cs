using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSTabBase : ComponentBase, IDisposable
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
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
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string Name { get; set; }



        protected override Task OnInitializedAsync()
        {
            Group.Tabs.Add(this);
            if (Group.Selected == null)
            {
                Group.Selected = this;
            }
            return base.OnInitializedAsync();
        }

        public void Select()
        {
            if (Group.Disposing)
            {
                //Unlocks tab updates. This prevents rouge mouse clicks.
                Group.Disposing = false;
                return;
            }
            Group.Selected = this;
        }
        public void UpdateContent()
        {
            try
            {
                Group.Tabs.First(q => q == this).Content = Content;
                if (this == Group.Selected)
                {
                    Group.Selected = null;
                    Group.Selected = this;
                    InvokeAsync(StateHasChanged);
                }
            }
            catch
            {

            }
        }

        public void Dispose()
        {
            //Prevent blazor form removing the wrong tab
            if (Group.Disposing) return;
            //Locks updates when deleting tabs
            
            Group.Tabs.Remove(this);
            Group.Selected = (Group.Selected == this) ? Group.Tabs.FirstOrDefault() : Group.Selected;
            Group.Disposing = true;
        }
    }
}