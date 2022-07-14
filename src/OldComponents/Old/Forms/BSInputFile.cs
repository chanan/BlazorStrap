using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlazorComponentUtilities;
using BlazorStrap.Components.Base;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public class BSInputFile<TValue> : LayoutBase, IDisposable
    {
        /// <summary>
        /// CSS class added to input when input is invalid. Defaults to <c>is-invalid</c>.
        /// </summary>
        [Parameter] public string InvalidClass { get; set; } = "is-invalid";

        [Parameter] public TValue? IsBasic { get; set; }

        /// <summary>
        /// Sets the input to be disabled.
        /// </summary>
        [Parameter] public bool IsDisabled { get; set; }

        /// <summary>
        /// Whether the input is invalid.
        /// </summary>
        [Parameter] public bool IsInvalid { get; set; }

        /// <summary>
        /// Removes default class.
        /// </summary>
        [Parameter] public bool RemoveDefaultClass { get; set; }

        /// <summary>
        /// Whether the input is valid.
        /// </summary>
        [Parameter] public bool IsValid { get; set; }

        /// <summary>
        /// Event called when input is changed.
        /// </summary>
        [Parameter] public EventCallback<InputFileChangeEventArgs> OnChange { get; set; }

        /// <summary>
        /// CSS class added to input when input is valid. Defaults to <c>is-valid</c>.
        /// </summary>
        [Parameter] public string ValidClass { get; set; } = "is-valid";

        /// <summary>
        /// Custom validator.
        /// </summary>
        [Parameter] public Expression<Func<TValue>>? ValidWhen { get; set; }

        private bool _hasInitialized;
        [CascadingParameter] private EditContext? CascadedEditContext { get; set; }

        private string? ClassBuilder => new CssBuilder()
            .AddClass("form-control", !RemoveDefaultClass)
            .AddClass(ValidClass, IsValid)
            .AddClass(InvalidClass, IsInvalid)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .AddClass(ValidClass, IsValid)
            .AddClass(InvalidClass, IsInvalid)
            .Build().ToNullString();

        protected EditContext? EditContext { get; set; }

        protected internal FieldIdentifier FieldIdentifier { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<InputFile>(0);
            builder.AddAttribute(1, "OnChange", EventCallback.Factory.Create<InputFileChangeEventArgs>(this, OnFileChange));
            builder.AddAttribute(2, "class", ClassBuilder);
            builder.AddAttribute(3, "onclick", OnFileClick);
            builder.AddMultipleAttributes(4, Attributes);
            builder.CloseComponent();
        }

        protected override void OnParametersSet()
        {
            if (!_hasInitialized)
            {
                if (CascadedEditContext != null)
                {

                    _hasInitialized = true;
                    EditContext = CascadedEditContext;
                    if (ValidWhen != null) FieldIdentifier = FieldIdentifier.Create(ValidWhen);
                    EditContext.OnValidationStateChanged += OnValidationStateChanged;
                    EditContext.OnValidationRequested += EditContext_OnValidationRequested;
                }


            }
            else if (CascadedEditContext != EditContext)
            {
                // Not the first run

                // We don't support changing EditContext because it's messy to be clearing up state and event
                // handlers for the previous one, and there's no strong use case. If a strong use case
                // emerges, we can consider changing this.
                throw new InvalidOperationException($"{GetType()} does not support changing the EditContext dynamically.");
            }
        }

        private void EditContext_OnValidationRequested(object? sender, ValidationRequestedEventArgs e)
        {
            if (sender != null)
                ((EditContext)sender).NotifyFieldChanged(FieldIdentifier);
        }

        private async Task OnFileChange(InputFileChangeEventArgs e)
        {
            if (OnChange.HasDelegate)
                await OnChange.InvokeAsync(e);

            if (EditContext is not null)
            {
                EditContext.NotifyFieldChanged(FieldIdentifier);
                EditContext.NotifyValidationStateChanged();
            }
        }

        private Task OnFileClick(MouseEventArgs e)
        {
            var filechangevent = new InputFileChangeEventArgs(Array.Empty<IBrowserFile>());
            return OnFileChange(filechangevent);
        }

        private void DoValidation()
        {
            if (EditContext is null)
            {
                return;
            }

            if (EditContext.IsModified(FieldIdentifier))
            {
                if (EditContext.GetValidationMessages(FieldIdentifier).Any())
                {
                    IsInvalid = true;
                    IsValid = false;
                }
                else
                {
                    IsValid = true;
                    IsInvalid = false;
                }
            }
            else
            {
                IsInvalid = false;
                IsValid = false;
            }
        }

        private void OnValidationStateChanged(object? sender, ValidationStateChangedEventArgs e)
        {
            DoValidation();
        }

        public void Dispose()
        {
            if (EditContext is not null)
            {
                EditContext.OnValidationStateChanged -= OnValidationStateChanged;
            }
        }
    }
}