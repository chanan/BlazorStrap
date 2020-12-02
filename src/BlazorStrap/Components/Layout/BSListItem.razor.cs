using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorStrap
{
    public partial class BSListItem : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        private bool _active = false;
        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                StateHasChanged();
            }
        }

        protected string Classname =>
        new CssBuilder()
            .AddClass("active", _active)
            .AddClass(Class)
        .Build();

        [Parameter] public string Class { get; set; }
    }
}