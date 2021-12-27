using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSAccordionItem : BlazorStrapBase, IAsyncDisposable
    {
        [Parameter] public bool AlwaysOpen { get; set; }
        [Parameter] public RenderFragment? Content { get; set; }

        [Parameter]
        public bool DefaultShown
        {
            get => _defaultShown;
            set { _defaultShown = value; Shown = value; }
        }

        [Parameter] public RenderFragment? Header { get; set; }

        private bool _defaultShown;
        private DotNetObjectReference<BSAccordionItem>? _objRef;
        [CascadingParameter] public BSAccordion? Parent {get;set; }
        private ElementReference ButtonRef { get; set; }

        private string? ClassBuilder => new CssBuilder("accordion-collapse collapse")
            .AddClass("show", Shown)
            .AddClass(LayoutClass, !String.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !String.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private bool HasRendered { get; set; }

        private ElementReference MyRef { get; set; }

        // Can be access by @ref
        private bool Shown { get; set; }

        public async Task ToggleAsync()
        {
            if (!EventsSet)
            {
                await Js.InvokeVoidAsync("blazorStrap.AddEvent", DataId, "bsAccordionItem", "transitionend");
                EventsSet = true;
            }
            _objRef = DotNetObjectReference.Create(this);
            Parent?.Invoke(this);
            if (Shown)
            {
                var height = await Js.InvokeAsync<int>("blazorStrap.GetHeight", MyRef); 
                await Js.InvokeVoidAsync("blazorStrap.RemoveClass", MyRef, "collapse");
                await Js.InvokeVoidAsync("blazorStrap.SetStyle", MyRef,"height", $"{height}px");
                await Js.InvokeVoidAsync("blazorStrap.RemoveClass", MyRef, "show");
                await Js.InvokeVoidAsync("blazorStrap.AddClass", MyRef, "collapsing",100);
                await Js.InvokeVoidAsync("blazorStrap.AddClass", ButtonRef, "collapsed");
                await Js.InvokeVoidAsync("blazorStrap.SetStyle", MyRef,"height", "");
                
            }
            else
            {
                var height = await Js.InvokeAsync<int>("blazorStrap.PeakHeight", MyRef); 
                await Js.InvokeVoidAsync("blazorStrap.RemoveClass", MyRef, "collapse");
                await Js.InvokeVoidAsync("blazorStrap.RemoveClass", ButtonRef, "collapsed");
                await Js.InvokeVoidAsync("blazorStrap.AddClass", MyRef, "collapsing",100);
                await Js.InvokeVoidAsync("blazorStrap.SetStyle", MyRef,"height", $"{height}px");
            }
            if (await Js.InvokeAsync<bool>("blazorStrap.TransitionDidNotStart", MyRef))
            {
                await TransitionEndAsync();
            }
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                HasRendered = true;
            }
            Console.WriteLine("Rendered");
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
            
            await Js.InvokeVoidAsync("blazorStrap.SetStyle", MyRef,"height", "");
            await Js.InvokeVoidAsync("blazorStrap.RemoveClass", MyRef, "collapsing");
            await Js.InvokeVoidAsync("blazorStrap.AddClass", MyRef, "collapse");
            if (!Shown)
            {
                await Js.InvokeVoidAsync("blazorStrap.AddClass", MyRef, "show");
            }
            Shown = !Shown;
            await InvokeAsync(StateHasChanged);
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
            if(sender != this && Shown && !AlwaysOpen && !sender.AlwaysOpen)
            {
                await ToggleAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if(EventsSet)
                await Js.InvokeVoidAsync("blazorStrap.RemoveEvent", DataId, "bsAccordionItem", "transitionend");
            JSCallback.EventHandler -= OnEventHandler;
            if (Parent != null)
            {
                Parent.ChildHandler -= Parent_ChildHandler;
            }
            GC.SuppressFinalize(this);
        }
    }
}
