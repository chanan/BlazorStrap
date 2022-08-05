using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorStrap.Shared.Components.Forms
{
    public abstract class BSFormBase<TValue, TJustify> : BlazorStrapBase, IBSForm where TJustify : Enum
    {
        public event Action? OnResetEventHandler;
        /// <summary>
        /// Form alignment.
        /// </summary>
        [Parameter] public Align Align { get; set; }

        /// <summary>
        /// Form editcontext.
        /// </summary>
        [Parameter] public EditContext? EditContext { get; set; }

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

        // [Parameter] public bool ValidateOnInit { get; set; }

        /// <summary>
        /// Vertical Gutters
        /// </summary>
        [Parameter] public Gutters VerticalGutters { get; set; }

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
