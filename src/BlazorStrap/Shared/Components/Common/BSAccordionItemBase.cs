using BlazorStrap.Extensions;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSAccordionItemBase : BlazorStrapToggleBase<BSAccordionItemBase>, IDisposable
    {
        public override bool Shown
        {
            get => _shown;
            protected set => _shown = value;
        }
        private bool _shown;
        private IList<EventQue> _eventQue = new List<EventQue>();
        // This is for nesting allows the child to jump to transition end if the parent is hidden or shown while in transition.
        internal Action? NestedHandler { get; set; }
        private DotNetObjectReference<BSAccordionItemBase>? _objectRef;

        /// <summary>
        /// Disables animations during collapsing of the accoridion item.
        /// </summary>
        [Parameter] public bool NoAnimations { get; set; }

        /// <summary>
        /// Makes accordion item stay open when another item is opened.
        /// See <see href="https://getbootstrap.com/docs/5.2/components/accordion/#always-open">Bootstrap Documentation</see>
        /// </summary>
        [Parameter] public bool AlwaysOpen { get; set; }

        /// <summary>
        /// Accordion item content.
        /// </summary>
        [Parameter] public RenderFragment? Content { get; set; }

        /// <summary>
        /// Accordion item is shown by default.
        /// </summary>
        [Parameter]
        public bool DefaultShown
        {
            get => _defaultShown;
            set { _defaultShown = value; _isDefaultShownSet = true; }
        }

        protected override void OnInitialized()
        {
            _shown = DefaultShown;
        }
        
        /// <summary>
        /// Accordion item header content.
        /// </summary>
        [Parameter] public RenderFragment? Header { get; set; }

        private bool _defaultShown;
        private bool _isDefaultShownSet;

        [CascadingParameter] public BSAccordionBase? Parent { get; set; }
        protected ElementReference? ButtonRef { get; set; }

        private bool HasRendered { get; set; }

        protected ElementReference? MyRef { get; set; }

        /// <summary>
        /// Returns whether or not the accordion item is shown.
        /// </summary>
        /// <remarks>Can be accessed using @ref</remarks>
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }

        /// <inheritdoc/>
        public override async Task ShowAsync()
        {
            if (_shown) return ;
            _ = Task.Run(() => { _ = OnShow.InvokeAsync(this); });
            //Kick off to event que
            var taskSource = new TaskCompletionSource<bool>();
                     var func = async () =>
            {
                CanRefresh = false;
                
                try
                {
                    await BlazorStrapService.Interop.RemoveClassAsync(ButtonRef, "collapsed");
                    await BlazorStrapService.Interop.AddAttributeAsync(ButtonRef, "aria-expanded", (!Shown).ToString().ToLower());
                    Parent?.Invoke(this);
                    if (!NoAnimations)
                    {
                        await BlazorStrapService.Interop.AnimateCollapseAsync(_objectRef, MyRef, DataId, true);
                        await BlazorStrapService.Interop.WaitForTransitionEnd(MyRef, 400);
                        await Task.Delay(50);
                        await BlazorStrapService.Interop.SetStyleAsync(MyRef, "height", "");
                    }
                }
                catch //Animation failed cleaning up
                {
                }
                _shown = true;
                await InvokeAsync(StateHasChanged);
                _ = Task.Run(() => { _ = OnShown.InvokeAsync(this); });
                taskSource.SetResult(true);
                CanRefresh = true;
            };
            _eventQue.Add(new EventQue { TaskSource = taskSource, Func = func});

            // Run event que if only item.
            if (_eventQue.Count == 1)
            {
                await InvokeAsync(StateHasChanged);
            }
            await taskSource.Task;
            
        }

        /// <inheritdoc/>
        public override async Task HideAsync()
        {
            if(!_shown) return ;
            _ = Task.Run(() => { _ = OnHide.InvokeAsync(this); });
            //Kick off to event que
            var taskSource = new TaskCompletionSource<bool>();
            var func = async () =>
            {
                CanRefresh = false;

                try
                {
                    await BlazorStrapService.Interop.AddClassAsync(ButtonRef, "collapsed");
                    await BlazorStrapService.Interop.AddAttributeAsync(ButtonRef, "aria-expanded", (!Shown).ToString().ToLower());
                    if (!NoAnimations)
                    {
                        await BlazorStrapService.Interop.AnimateCollapseAsync(_objectRef, MyRef, DataId, false);
                        await BlazorStrapService.Interop.WaitForTransitionEnd(MyRef, 400);
                    }
                }
                catch //Animation failed cleaning up
                {
                }

                _shown = false;
                await InvokeAsync(StateHasChanged);
                _ = Task.Run(() => { _ = OnHidden.InvokeAsync(this); });
                taskSource.SetResult(true);
                CanRefresh = true;

            };

            _eventQue.Add(new EventQue { TaskSource = taskSource, Func = func });
            // Run event que if only item.
            if (_eventQue.Count == 1) {
                await InvokeAsync(StateHasChanged);
            }
            await taskSource.Task;
        }

        /// <inheritdoc/>
        public override Task ToggleAsync()
        {
            return Shown ? HideAsync() : ShowAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                if (_eventQue.Count > 0)
                {
                    var eventItem = _eventQue.First();
                    if (eventItem != null)
                    {
                        _eventQue.Remove(eventItem);
                        await eventItem.Func.Invoke();
                    }
                }
            }
            else
            {
                _objectRef = DotNetObjectReference.Create(this);
            }
        }

        protected override void OnParametersSet()
        {
            if (Parent != null)
            {
                if (!_isDefaultShownSet && Parent.FirstChild())
                {
                    DefaultShown = true;
                    Shown = true;
                }
                Parent.ChildHandler += Parent_ChildHandler;
            }
        }
        
        [JSInvokable]
        public override async Task InteropEventCallback(string id, CallerName name, EventType type, Dictionary<string, string>? classList, JavascriptEvent? e)
        {
        }

        private async void Parent_ChildHandler(BSAccordionItemBase? sender)
        {
            if (sender != null)
            {
                if (sender != this && !AlwaysOpen && !sender.AlwaysOpen)
                {
                    await HideAsync();
                }
            }
        }

        public void Dispose()
        {
            _objectRef?.Dispose();
            if (Parent != null) Parent.ChildHandler -= Parent_ChildHandler;
        }
    }
}