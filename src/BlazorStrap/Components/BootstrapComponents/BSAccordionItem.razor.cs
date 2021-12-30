using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSAccordionItem : BlazorStrapToggleBase<BSAccordionItem>, IDisposable
    {
        private bool _lock;
        [Parameter] public bool NoAnimations { get; set; }
        [Parameter] public bool AlwaysOpen { get; set; }
        [Parameter] public RenderFragment? Content { get; set; }

        [Parameter]
        public bool DefaultShown
        {
            get => _defaultShown;
            set { _defaultShown = value; Shown = value; }
        }

        protected override bool ShouldRender()
        {
            return !_lock;
        }

        [Parameter] public RenderFragment? Header { get; set; }

        private bool _defaultShown;
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
        private bool Shown { get; set; }

        public override async Task ShowAsync()
        {
            if (OnShow.HasDelegate)
                await OnShow.InvokeAsync(this);
            Parent?.Invoke(this);
            
            if (_lock) return;
            _lock = true;
            if(!NoAnimations) await Js.InvokeVoidAsync("blazorStrap.AnimateCollapse", MyRef, true, DataId, "bsAccordionItem", "transitionend");
            Shown = true;
            if (NoAnimations)
                await TransitionEndAsync();
        }
        public override async Task HideAsync()
        {
            if (OnHide.HasDelegate)
                await OnHide.InvokeAsync(this);
            if (_lock) return;
            _lock = true;
            if(!NoAnimations) await Js.InvokeVoidAsync("blazorStrap.AnimateCollapse", MyRef, false, DataId, "bsAccordionItem", "transitionend");
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

        protected override void OnInitialized()
        {
            JSCallback.EventHandler += OnEventHandler;
        }

        protected override void OnParametersSet()
        {
            if(Parent != null)
            {
                if(Parent.FirstChild())
                {
                    DefaultShown = true;
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
        }

        private async void OnEventHandler(string id, string name, string type, Dictionary<string, string>? classList, JavascriptEvent? e)
        {
            if (DataId == id && name == "bsAccordionItem" && type == "transitionend")
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
            if (Parent != null) Parent.ChildHandler -= Parent_ChildHandler;
            JSCallback.EventHandler -= OnEventHandler;
        }
    }
}
