using BlazorComponentUtilities;
using BlazorStrap.Util;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public abstract class BSAlertBase : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public EventCallback<BSAlertEvent> ClosedEvent { get; set; }
        [Parameter] public EventCallback<BSAlertEvent> CloseEvent { get; set; }
        [Parameter] public Color Color { get; set; } = Color.Primary;
        [Parameter] public bool IsDismissible { get; set; }

        private bool _isOpen = true;
        [Parameter] public bool IsOpen 
        {
            get => _isOpen;
            set
            {
                if (_isOpen == value) return;

                _isOpen = value;
                IsOpenChanged.InvokeAsync(value);
                if (value)
                    CheckAutoHide().ConfigureAwait(false);
            }
        }
        [Parameter] public EventCallback<bool> IsOpenChanged { get; set; }
        /// <summary>
        /// When using AutoHide, you also need to use @bind-IsOpen to be able
        /// to properly re-open alert again
        /// </summary>
        [Parameter] public bool AutoHide { get; set; }
        /// <summary>
        /// Delay in milliseconds to wait to AutoHide the alert. 
        /// It is only used when AutoHide is set to true.
        /// Default = 4000 (4 seconds). 
        /// </summary>
        [Parameter] public int AutoHideDelay { get; set; } = 4000;
        /// <summary>
        /// Gets or sets an action to be invoked when the alert is dismissed.
        /// </summary>
        [Parameter] public EventCallback OnDismiss { get; set; }

        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        internal BSAlertEvent BSAlertEvent { get; set; }
        internal List<EventCallback<BSAlertEvent>> EventQue { get; set; } = new List<EventCallback<BSAlertEvent>>();
        protected string Classname =>
       new CssBuilder().AddClass("alert")
           .AddClass(IsDismissible ? "alert-dismissible" : "")
           .AddClass($"alert-{Color.ToDescriptionString()}")
           .AddClass(Class)
       .Build();
        

        protected override Task OnAfterRenderAsync(bool firstrun)
        {
            for (var i = 0; i < EventQue.Count; i++)
            {
                EventQue[i].InvokeAsync(BSAlertEvent);
                EventQue.RemoveAt(i);
            }

            return base.OnAfterRenderAsync(false);
        }

        protected void OnClick()
        {
            IsOpen = false;
            OnDismiss.InvokeAsync(EventCallback.Empty);
            CloseAlert();
        }

        protected void CloseAlert()
        {
            BSAlertEvent = new BSAlertEvent() { Target = this };
            CloseEvent.InvokeAsync(BSAlertEvent);
            EventQue.Add(ClosedEvent);
        }

        protected async Task CheckAutoHide()
        {
            if (IsOpen && AutoHide)
            {
                await Task.Delay(AutoHideDelay).ConfigureAwait(true);
                IsOpen = false;
                await InvokeAsync(StateHasChanged).ConfigureAwait(false);
                CloseAlert();
            }
        }
    }
}
