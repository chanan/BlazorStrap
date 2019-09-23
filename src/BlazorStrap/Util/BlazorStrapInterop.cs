using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorStrap.Util
{
    public class BlazorStrapInterop
    {
        public static Func<Task> OnEscapeEvent { get; set; }

        public static Func<string, Task> OnAnimationEndEvent { get; set; }
        public static Func<string, string, Task> OnAddClassEvent { get; set; }
        protected IJSRuntime JSRuntime { get; }

        public BlazorStrapInterop(IJSRuntime jsRuntime)
        {
            JSRuntime = jsRuntime;
        }
        [JSInvokable]
        public static Task OnEscape()
        {
            OnEscapeEvent?.Invoke();
            return Task.CompletedTask;
        }
        [JSInvokable]
        public static Task OnAddClass(string id, string Classname)
        {
            OnAddClassEvent?.Invoke(id, Classname);
            return Task.CompletedTask;
        }
        [JSInvokable]
        public static Task OnAnimationEnd(string id)
        {
            OnAnimationEndEvent?.Invoke(id);
            return Task.CompletedTask;
        }
        public ValueTask<int> GetScrollHeight(ElementReference el)
        {
            return JSRuntime.InvokeAsync<int>("blazorStrap.getScrollHeight", el);
        }
        public ValueTask<bool> SetStyle(ElementReference el, string key, string value)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.setStyle", el, key, value);
        }
        public ValueTask<bool> AddEventAnimationEnd(ElementReference el)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.addEventAnimationEnd", el);
        }
        public ValueTask<bool> RemoveEventAnimationEnd(ElementReference el)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.removeEventAnimationEnd", el);
        }
        /// <summary>
        /// Use for animated classes only. Blazor can not track classes added this way.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Classname"></param>
        /// <returns></returns>
        public ValueTask<bool> AddClass(ElementReference el, string Classname)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.addClass", el, Classname);
        }
        /// <summary>
        /// Use for animated classes only. Blazor can not track classes added this way.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Classname"></param>
        /// <returns></returns>
        public ValueTask<bool> RemoveClass(ElementReference el, string Classname)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.removeClass", el, Classname);
        }
        public ValueTask<bool> AddClass2Elements(ElementReference el, ElementReference el2, string Classname)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.addClass2Elements", el, el2, Classname);
        }
        public ValueTask<bool> RemoveClass2Elements(ElementReference el, ElementReference el2, string Classname)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.removeClass2Elements", el, el2, Classname);
        }
        public ValueTask<bool> AddBodyClass(string Classname)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.addBodyClass", Classname);
        }
        public ValueTask<bool> RemoveBodyClass(string Classname)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.removeBodyClass", Classname);
        }
        public ValueTask<bool> ChangeBodyClass(string Classname)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.changeBodyClass", Classname);
        }
        /// <summary>
        /// Primary use is with modals when the scroll bar is hidden
        /// </summary>
        /// <param name="padding"></param>
        /// <returns></returns>
        public ValueTask<bool> ChangeBodyPaddingRight(string padding)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.changeBodyPaddingRight", padding);
        }

        public ValueTask<string> ModalEscapeKey()
        {
            return JSRuntime.InvokeAsync<string>("blazorStrap.modelEscape");
        }
        public ValueTask<bool> Log(string message)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.log", message);
        }

        public ValueTask<bool> Popper(string target, string popper, ElementReference arrow, string placement)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.popper", target, popper, arrow, placement);
        }

        public ValueTask<bool> Tooltip(string target, ElementReference tooltip, ElementReference arrow, string placement)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.tooltip", target, tooltip, arrow, placement);
        }

        public ValueTask<object> FocusElement(ElementReference el)
        {
            return JSRuntime.InvokeAsync<object>("blazorStrap.focusElement", el);
        }

        public ValueTask<bool> SetBootstrapCSS(string theme, string version)
        {
            return JSRuntime.InvokeAsync<bool>("blazorStrap.setBootstrapCSS", theme, version);
        }


    }

    public class StringReturn
    {
        public string Result { get; set; }
    }
}