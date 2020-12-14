using BlazorComponentUtilities;
using BlazorStrap.Util;
using BlazorStrap.Util.Components;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorStrap
{
    public partial class BSLabel : ColumnBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }

        protected string Classname =>
        new CssBuilder()
           .AddClass("form-check-label", IsCheck && !IsButton)
           .AddClass("btn", IsButton)
           .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None && IsButton)
           .AddClass("active", IsActive)
           .AddClass("col-form-label", GetColumnClass(null) != null)
           .AddClass(GetColumnClass(null))
           .AddClass(Class)
        .Build();

        [CascadingParameter] public BSButtonGroup BSButtonGroup { get; set; }
        [Parameter] public Color Color { get; set; } = Color.Primary;
        [Parameter] public bool IsCheck { get; set; }
        [Parameter] public bool IsButton { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        private bool _isActive { get; set; }
        public bool IsActive { get => _isActive ; 
            set {
                if (value != _isActive)
                    StateHasChanged();
                _isActive = value;
            } }
    }
}
