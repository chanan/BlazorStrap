using BlazorComponentUtilities;
using BlazorStrap.Util;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace BlazorStrap
{
    public partial class BSTableRow : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string Classname =>
        new CssBuilder()
            .AddClass($"table-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass(Class)
        .Build();

        [Parameter] public Color Color { get; set; } = Color.None;
        [Parameter] public EventCallback<MouseEventArgs> OnClick{ get; set; }
        [Parameter] public bool OnClickPreventDefault { get; set; }
        [Parameter] public bool OnClickStopPropagation { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected void OnClickEvent(MouseEventArgs e)
        {
            if (OnClick.HasDelegate)
            {
                OnClick.InvokeAsync(e);
            }
        }
    }
}
