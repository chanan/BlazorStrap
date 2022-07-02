using BlazorStrap.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public partial class BSCloseButton : BlazorStrapBase, ICloseButtonParameters
    {
        private ICloseButton? closeButton;
        /// <summary>
        /// Whether or not the button is disabled.
        /// </summary>
        [Parameter] public bool IsDisabled { get; set; }

        /// <summary>
        /// Adds the btn-close-white class.
        /// </summary>
        [Parameter] public bool IsWhite { get; set; }

        /// <summary>
        /// Event called when button is clicked.
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected override void OnParametersSet()
        {
            if(closeButton == null)
            {
                Console.WriteLine((int)BlazorStrap.bootStrapVersion);
                var type = Type.GetType($"BlazorStrap.Bootstrap.V{(int)BlazorStrap.bootStrapVersion}.CloseButton");
                if (type != null)
                {
                    closeButton = (ICloseButton)(Activator.CreateInstance(type) ?? throw new NullReferenceException("Could not Create Instance"));
                    closeButton.SetParameters(IsDisabled,IsWhite,EventCallback.Factory.Create<MouseEventArgs>(this, ClickEventAsync),Attributes,ChildContent,DataId,Class,LayoutClass);
                }
            }
        }

        private async Task ClickEventAsync()
        {
            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync();
            }
        }
    }
}