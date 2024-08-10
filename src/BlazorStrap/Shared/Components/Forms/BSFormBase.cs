using BlazorStrap.Shared.Components.Modal;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorStrap.Shared.Components.Forms
{
    public abstract class BSFormBase<TValue, TJustify> : BlazorStrapBase, IBSForm where TJustify : Enum
    {
        public event Action? OnResetEventHandler;

        /// <summary>
        /// If enabled, form submission is performed without fully reloading the page. This is
        /// equivalent to adding <code>data-enhance</code> to the form.
        ///
        /// This flag is only relevant in server-side rendering (SSR) scenarios. For interactive
        /// rendering, the flag has no effect since there is no full-page reload on submit anyway.
        /// </summary>
        [Parameter] public bool Enhance { get; set; }

        /// <summary>
        /// Form alignment.
        /// </summary>
        [Parameter] public Align Align { get; set; }

        /// <summary>
        /// Form editcontext.
        /// </summary>
        [Parameter] public EditContext? EditContext { get; set; }

        /// <summary>
        /// Gets or sets the form handler name. This is required for posting it to a server-side endpoint.
        /// It is not used during interactive rendering.
        /// </summary>
        [Parameter] public string? FormName { get; set; }

        /// <summary>
        /// Gutters
        /// </summary>
        [Parameter] public Gutters Gutters { get; set; }

        /// <summary>
        /// Horizontal Gutters.
        /// </summary>
        [Parameter] public Gutters HorizontalGutters { get; set; }
        [Parameter] public TValue? IsBasic { get; set; }
        [Parameter] public bool IsFloating { get; set; }
        [Parameter] public bool IsRow { get; set; }

        /// <summary>
        /// Justify
        /// </summary>
        [Parameter] public TJustify Justify { get; set; }

        [Parameter] public TValue? Model { get; set; }

        /// <summary>
        /// Method called when form is submitted and validation fails.
        /// </summary>
        [Parameter] public EventCallback<EditContext> OnInvalidSubmit { get; set; }

        /// <summary>
        /// Method called when form is submitted.
        /// </summary>
        [Parameter] public EventCallback<EditContext> OnSubmit { get; set; }

        /// <summary>
        /// Method called when form is submitted and validation passes.
        /// </summary>
        [Parameter] public EventCallback<EditContext> OnValidSubmit { get; set; }

        /// <summary>
        /// Method called when form is reset.
        /// </summary>
        [Parameter] public EventCallback<IBSForm> OnReset { get; set; }

        // [Parameter] public bool ValidateOnInit { get; set; }

        /// <summary>
        /// Vertical Gutters
        /// </summary>
        [Parameter] public Gutters VerticalGutters { get; set; }
        
        [CascadingParameter] public BSModalBase? Modal { get; set; }

        protected RenderFragment<EditContext>? EditFormChildContent { get; set; }

        protected RenderFragment? Form { get; set; }

        protected abstract string? ClassBuilder { get; }
        // Is there even a good use for this?
        /*public void FormIsReady(EditContext e)
        {
            EditContext = e;
            if (ValidateOnInit)
            {
                ForceValidate();
            }
        }*/

        public async Task OnValidSubmitEvent(EditContext context)
        {
            if (OnValidSubmit.HasDelegate)
            {
                if (Modal?.HideOnValidSubmit ?? false)
                {
                    await Modal.HideAsync();
                }
                await OnValidSubmit.InvokeAsync(context);
            }
        }

        public async Task OnSubmitEvent(EditContext context)
        {
            if (OnSubmit.HasDelegate)
            {
                if (Modal?.HideOnSubmit ?? false)
                {
                    await Modal.HideAsync();
                }
                await OnSubmit.InvokeAsync(context);
            }
        }
        
        public Task OnResetEvent()
        {
            if (OnReset.HasDelegate)
                return OnReset.InvokeAsync(this);
            return Task.CompletedTask;
        }
        public void Refresh()
        {
            StateHasChanged();
        }
        public void Reset()
        {
            OnResetEventHandler?.Invoke();
        }
        private void ForceValidate()
        {
            InvokeAsync(() => EditContext?.Validate());
            StateHasChanged();
        }

    }
}
