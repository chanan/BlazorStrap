using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSListItemBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        private bool _active = false;
        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                StateHasChanged();
            }
        }

        protected string classname =>
        new CssBuilder()
            .AddClass("active", _active)
            .AddClass(Class)
        .Build();

        [Parameter] public string Class { get; set; }
    }
}