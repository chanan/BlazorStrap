using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorStrap.Shared.Components.Forms
{
    public abstract class BSValidationSummaryBase : ComponentBase, IDisposable
    {
        protected bool DidSubmit = false;
        [CascadingParameter] protected EditContext? CurrentEditContext { get; set; }

        /// <summary>
        /// string: Key ,  Tuple of string: Error Message, bool: Valid
        /// </summary>
        [Parameter] public Dictionary<string, bool> ValidationMessages { get; set; } = new Dictionary<string, bool>();

        [Parameter(CaptureUnmatchedValues = true)] public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }
        protected override void OnInitialized()
        {
            if (CurrentEditContext != null)
            {
                CurrentEditContext.OnValidationRequested += CurrentEditContext_OnValidationRequested;
                CurrentEditContext.OnValidationStateChanged += CurrentEditContext_OnValidationStateChanged;
            }

        }

        private void CurrentEditContext_OnValidationStateChanged(object? sender, ValidationStateChangedEventArgs e)
        {
            StateHasChanged();
        }

        private void CurrentEditContext_OnValidationRequested(object? sender, ValidationRequestedEventArgs e)
        {
            DidSubmit = true;
        }

        public void Dispose()
        {
            if (CurrentEditContext != null)
            {
                CurrentEditContext.OnValidationRequested -= CurrentEditContext_OnValidationRequested;
                CurrentEditContext.OnValidationStateChanged -= CurrentEditContext_OnValidationStateChanged;
            }
        }
    }
}
