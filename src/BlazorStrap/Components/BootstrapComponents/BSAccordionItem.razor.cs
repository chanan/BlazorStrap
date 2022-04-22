using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSAccordionItem : BlazorStrapToggleBase<BSAccordionItem>, IDisposable
    {
        private DotNetObjectReference<BSAccordionItem> _objectRef;
        private bool _lock;
        [Parameter] public bool NoAnimations { get; set; }
        [Parameter] public bool AlwaysOpen { get; set; }
        [Parameter] public RenderFragment? Content { get; set; }

        [Parameter]
        public bool DefaultShown
        {
            get => _defaultShown;
            set { _defaultShown = value; _isDefaultShownSet = true; }
        }

        protected override void OnInitialized()
        {
            _objectRef = DotNetObjectReference.Create<BSAccordionItem>(this);
            Shown = DefaultShown;
        }

        protected override bool ShouldRender()
        {
            return !_lock;
        }

        [Parameter] public RenderFragment? Header { get; set; }

        private bool _defaultShown;
        private bool _isDefaultShownSet;

        [CascadingParameter] public BSAccordion? Parent {get;set; }
        private ElementReference ButtonRef { get; set; }

        private string? ClassBuilder => new CssBuilder("accordion-collapse collapse")
            .AddClass("show", Shown)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private bool HasRendered { get; set; }

        private ElementReference MyRef { get; set; }

        // Can be access by @ref
        public bool Shown { get; private set; }

        public override async Task ShowAsync()
        {
            if (Shown) return;
            CanRefresh = false;
            await BlazorStrap.Interop.RemoveClassAsync(ButtonRef, "collapsed");
            await BlazorStrap.Interop.AddAttributeAsync(ButtonRef, "aria-expanded", (!Shown).ToString().ToLower());
            if (OnShow.HasDelegate)
                await OnShow.InvokeAsync(this);
            Parent?.Invoke(this);
            
            if (_lock) return;
            _lock = true;
            if (!NoAnimations)
                await BlazorStrap.Interop.AnimateCollapseAsync(_objectRef, MyRef, DataId, true);
            Shown = true;
            if (NoAnimations)
                await TransitionEndAsync();
        }
        public override async Task HideAsync()
        {
            if (!Shown) return;
            CanRefresh = false;
            await BlazorStrap.Interop.AddClassAsync(ButtonRef, "collapsed");
            await BlazorStrap.Interop.AddAttributeAsync(ButtonRef, "aria-expanded", (!Shown).ToString().ToLower());
            if (OnHide.HasDelegate)
                await OnHide.InvokeAsync(this);
            if (_lock) return;
            _lock = true;
            if(!NoAnimations)
                await BlazorStrap.Interop.AnimateCollapseAsync(_objectRef, MyRef, DataId, false);
            Shown = false;
            if (NoAnimations)
                await TransitionEndAsync();
        }
        
        public override Task ToggleAsync()
        {
            return Shown ? HideAsync() : ShowAsync();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                HasRendered = true;
            }
        }

        protected override void OnParametersSet()
        {
            if(Parent != null)
            {
                if(!_isDefaultShownSet && Parent.FirstChild())
                {
                    DefaultShown = true;
                    Shown = true;
                }
                Parent.ChildHandler += Parent_ChildHandler;
            }
        }

        private async Task TransitionEndAsync()
        {
            _lock = false;
            await InvokeAsync(StateHasChanged);
            
            if (OnShown.HasDelegate && Shown)
                _ = Task.Run(() => { _ = OnShown.InvokeAsync(this); });

            if (OnHidden.HasDelegate && !Shown)
                _ = Task.Run(() => { _ = OnHidden.InvokeAsync(this); });
            CanRefresh = true;
        }
        
        [JSInvokable]
        public override async Task InteropEventCallback(string id, CallerName name, EventType type, Dictionary<string, string>? classList, JavascriptEvent? e)
        {
            if (DataId == id && name.Equals(this) && type == EventType.TransitionEnd)
            {
                await TransitionEndAsync();
            }
        }

        private async void Parent_ChildHandler(BSAccordionItem sender)
        {
            if(sender != this && !AlwaysOpen && !sender.AlwaysOpen)
            {
                await HideAsync();
            }
        }

        public void Dispose()
        {
            _objectRef.Dispose();
            if (Parent != null) Parent.ChildHandler -= Parent_ChildHandler;
        }
    }
}
