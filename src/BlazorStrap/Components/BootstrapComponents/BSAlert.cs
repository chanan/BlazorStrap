using BlazorComponentUtilities;
using BlazorStrap.Bootstrap.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public class BSAlert : BlazorStrapBase, IAlert
    {
        private IAlert? _reference;
        /// <summary>
        /// Color class of alert
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        /// <summary>
        /// Alert body content
        /// </summary>
        [Parameter] public RenderFragment? Content { get; set; }

        /// <summary>
        /// Event triggered when alert is dismissed. Only called when <see cref="IsDismissible"/> is true
        /// </summary>
        [Parameter] public EventCallback Dismissed { get; set; }

        /// <summary>
        /// Sets whether or not an icon is shown
        /// </summary>
        [Parameter] public bool HasIcon { get; set; }

        /// <summary>
        /// Alert header content (optional)
        /// </summary>
        [Parameter] public RenderFragment? Header { get; set; }

        /// <summary>
        /// Heading size. Valid values are 1-6
        /// </summary>
        [Parameter] public int Heading { get; set; } = 1;

        /// <summary>
        /// Determines whether or not an alter is dismissible. See <see cref="Dismissed"/> for the callback
        /// </summary>
        [Parameter] public bool IsDismissible { get; set; }


        /// <summary>
        /// Dismisses the alert
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        public Task CloseEventAsync()
        {
           if(_reference != null)
                return _reference.CloseEventAsync();
           return Task.CompletedTask;   
        }

        /// <summary>
        /// Opens the alert
        /// </summary>
        public void Open()
        {
            //Console.WriteLine();
            _reference.Open();
        }
       
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.AddContent(0, this.BuildRenderFragment("Alert", (int)BlazorStrap.bootStrapVersion, (c) => _reference = (IAlert) c));
        }
    }
}
