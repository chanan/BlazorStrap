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
        internal BSTabEvent BSTabEvent { get; set; }
        internal List<EventCallback<BSTabEvent>> EventQue { get; set; } = new List<EventCallback<BSTabEvent>>();

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

        [Parameter] protected EventCallback<BSTabEvent> Show { get; set; }
        [Parameter] protected EventCallback<BSTabEvent> Shown { get; set; }
        [Parameter] protected EventCallback<BSTabEvent> Hide { get; set; }
        [Parameter] protected EventCallback<BSTabEvent> Hidden { get; set; }

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
            Show.InvokeAsync(BSTabEvent);
            EventQue.Add(Shown);
        }
        public void UnSelected()
        {
            Show.InvokeAsync(BSTabEvent);
            EventQue.Add(Hidden);
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
        

        protected override Task OnAfterRenderAsync()
        {
            for (int i = 0; i < EventQue.Count; i++)
            {
                EventQue[i].InvokeAsync(BSTabEvent);
                EventQue.RemoveAt(i);
            }

            return base.OnAfterRenderAsync();
        }
    }
}