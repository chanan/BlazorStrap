using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public partial class BSTab : ComponentBase, IDisposable
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        public RenderFragment Content { get; set; }
        public bool Selected => (Group != null) ? Group.Selected == this : false;
        protected string Classname =>
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
        [Parameter] public bool? InitialSelection { get; set; }
        [CascadingParameter] public DynamicItem DynamicObject { get; set; }



        protected override Task OnInitializedAsync()
        {
            Group.Tabs.Add(this);
            if (Group.Selected == null || InitialSelection.GetValueOrDefault())
            {
                Group.Selected = this;
            }

            if (!String.IsNullOrEmpty(Group?.ReturnId))
            {
                if (Group.ReturnId == Id)
                {
                    Group.Selected = this;
                }
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

            Group.DynamicSelected = DynamicObject;
            Group.Selected = this;
        }
        public void UpdateContent()
        {
            if (Group == null) return;
            Group.Tabs.First(q => q == this).Content = Content;
            if (this == Group.Selected)
            {
                Group.Selected = null;
                Group.Selected = this;
                InvokeAsync(StateHasChanged);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Prevent blazor form removing the wrong tab
                if (Group.Disposing) return;
                //Locks updates when deleting tabs
                Group.Tabs.Remove(this);
                Group.Disposing = true;
                Group.Selected = (Group.Selected == this) ? Group.Tabs.FirstOrDefault() : Group.Selected;
            }
        }
    }
}