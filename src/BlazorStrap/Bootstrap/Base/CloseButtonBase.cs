using BlazorStrap.Bootstrap.Interfaces;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap.Bootstrap.Base
{
    internal class CloseButtonBase : LayoutBase, ICloseButton
    {
        [Parameter] public bool IsDisabled { get; set; }
        [Parameter] public bool IsWhite { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected async Task ClickEventAsync(MouseEventArgs e)
        {
            if (OnClick.HasDelegate)
            {
                await EventUtil.AsNonRenderingEventHandler<MouseEventArgs>(OnClick.InvokeAsync).Invoke(e);
            }
        }
    }
}
