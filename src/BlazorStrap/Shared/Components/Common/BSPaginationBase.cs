using BlazorStrap.Extensions;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSPaginationBase<TSize> : BlazorStrapBase, IDisposable
    {
        protected string MyId { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Sets aligment of pagination elements. See 
        /// <see href="https://getbootstrap.com/docs/5.2/components/pagination/#alignment">Bootstrap Documentation</see>
        /// </summary>
        [Parameter] public Align Align { get; set; } = Align.Default;

        /// <summary>
        /// Event fired when <see cref="Value"/> changes.
        /// </summary>
        [Parameter] public EventCallback<int> ValueChanged { get; set; }

        protected int? CurrentValue
        {
            get => Value;
            set
            {
                var hasChanged = !EqualityComparer<int?>.Default.Equals(value, Value);
                if (hasChanged)
                {
                    Value = value;
                    _ = ValueChanged.InvokeAsync(Value ?? 0);
                }
            }
        }

        protected int Page => Value ?? 0;

        /// <summary>
        /// Sets color of pagination elements.
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        /// <summary>
        /// Sets size of pagination elements. See 
        /// <see href="https://getbootstrap.com/docs/5.2/components/pagination/#sizing">Bootstrap Documentation</see>
        /// </summary>
        [Parameter] public TSize Size { get; set; } 

        /// <summary>
        /// Sets number of pages.
        /// </summary>
        [Parameter] public int Pages { get; set; } = -1;

        /// <summary>
        /// Active page number.
        /// </summary>
        [Parameter] public int? Value { get; set; } = null;

        protected int PageWidth = 50;
        private int MaxItems { get; set; } = 0;
        private bool _resized;
        protected ElementReference NavReference { get; set; }
       
        protected override void OnInitialized()
        {
            if (Pages > 999)
                PageWidth = 66;
            if (Pages > 9999)
                PageWidth = 82;
            if (Pages > 99999)
                PageWidth = 98;
            BlazorStrapService.OnEvent += OnEventAsync;
            BlazorStrapService.OnEventForward += InteropEventCallback;
        }

        private async Task GetMaxItems()
        {
            var navWidth = await BlazorStrapService.JavaScriptInterop.GetWidthAsync(NavReference) - 140;
            if (navWidth is not null)
            {
                var max = (navWidth / PageWidth / 2);
                if (MaxItems != max.Value)
                {
                    MaxItems = max.Value;

                }
            }
        }

        protected void ChangePage(int page)
        {
            if (page < 1) page = 1;
            if (page > Pages) page = Pages;
            CurrentValue = page;
            try
            {
                _ = BlazorStrapService.JavaScriptInterop.BlurAllAsync();
            }
            catch { }
        }
        protected int GetPreviousPages()
        {
            var value = Page - MaxItems - GetExtraBefore();
            return value < 1 ? 1 : value;
        }

        protected int GetMaxAfter()
        {
            // MaxItems + the remaining spaces             
            return MaxItems + (MaxItems - (Page - GetPreviousPages()));
        }

        private int GetExtraBefore()
        {
            var value = (Pages - (Page + MaxItems)) * -1;
            return value < 0 ? 0 : value;
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && Pages != -1)
            {
                var _objectRef = DotNetObjectReference.Create<BSPaginationBase<TSize>>(this);
                await BlazorStrapService.JavaScriptInterop.AddEventAsync(MyId, MyId, EventType.Resize);
                // await BlazorStrap.Interop.AddEventAsync(_objectRef, MyId, EventType.Resize);
                await GetMaxItems();
                await InvokeAsync(StateHasChanged);
            }
            else if (_resized)
            {
                _resized = false;
                await GetMaxItems();
                await InvokeAsync(StateHasChanged);
            }
        }

        public override async Task OnEventAsync(string sender, string target, EventType type, object? data)
        {
            if(sender == MyId && type == EventType.Resize)
            {
                _resized = true;
                await InvokeAsync(StateHasChanged);
            }
        }

        public void Dispose()
        {
            BlazorStrapService.OnEvent -= OnEventAsync;
        }

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
    }
}
