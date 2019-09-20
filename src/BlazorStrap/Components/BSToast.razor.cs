using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSToastBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string Classname =>
        new CssBuilder("toast fade")
            .AddClass("show", IsVisible && !_isDismissed)
            .AddClass("hide", !IsVisible || _isDismissed)
            .AddClass(Class)
        .Build();
        [Parameter] public string DateFormat {get; set;} = "dd/MM/yyyy h:mm tt";
        [Parameter] public string ImgSrc { get; set; }
        [Parameter] public string ImgDescription { get; set; }
        [Parameter] public DateTime? TimeStamp { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public bool IsVisible { get; set; }
        [Parameter] public RenderFragment BSToastHeader { get; set; }
        [Parameter] public RenderFragment BSToastBody { get; set; }

        /// <summary>
        /// Gets or sets an action to be invoked when the alert is dismissed.
        ///</summary>
        [Parameter] public EventCallback OnDismiss { get; set; }

        private bool _isDismissed { get; set; } = false;

        protected async Task OnClick()
        {
            _isDismissed = true;
            await OnDismiss.InvokeAsync(null).ConfigureAwait(false);
            StateHasChanged();
        }
    }
}
