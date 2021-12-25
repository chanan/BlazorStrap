using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public class BSInputFile<TValue> : BlazorStrapBase, IDisposable
    {
        [Parameter] public string InvalidClass { get; set; } = "is-invalid";
        [Parameter] public bool IsDisabled { get; set; }
        [Parameter] public bool IsInvalid { get; set; }
        [Parameter] public TValue IsManual { get; set; }
        [Parameter] public bool IsValid { get; set; }
        [Parameter] public EventCallback<InputFileChangeEventArgs> OnChange { get; set; }
        [Parameter] public string ValidClass { get; set; } = "is-valid";
        [Parameter] public Expression<Func<TValue>> ValidWhen { get; set; }
        private bool _hasInitialized;
        [CascadingParameter] private EditContext? CascadedEditContext { get; set; }

        private string? ClassBuilder => new CssBuilder("form-control")
            .AddClass(ValidClass, IsValid)
            .AddClass(InvalidClass, IsInvalid)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        protected EditContext EditContext { get; set; } = default!;

        protected internal FieldIdentifier FieldIdentifier { get; set; }

        protected override void OnParametersSet()
        {
            if (!_hasInitialized)
            {
                if (CascadedEditContext != null)
                {

                    _hasInitialized = true;
                    EditContext = CascadedEditContext;
                    FieldIdentifier = FieldIdentifier.Create(ValidWhen);
                    //Field Changed
                    EditContext.OnFieldChanged += OnFieldChanged;
                    // Submitted
                    EditContext.OnValidationRequested += OnValidationRequested;
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

        private async Task OnFileChange(InputFileChangeEventArgs e)
        {
            if (OnChange.HasDelegate)
                await OnChange.InvokeAsync(e);

            if (EditContext is not null)
            {
                Console.WriteLine(FieldIdentifier.FieldName);
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

        private void OnFieldChanged(object? sender, FieldChangedEventArgs e)
        {
            if (e.FieldIdentifier.Equals(FieldIdentifier))
                DoValidation();
        }

        private void OnValidationRequested(object? sender, ValidationRequestedEventArgs e)
        {
            DoValidation();
        }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<InputFile>(0);
            builder.AddAttribute(1, "OnChange", EventCallback.Factory.Create<InputFileChangeEventArgs>(this, OnFileChange));
            builder.AddAttribute(2, "class", ClassBuilder);
            builder.AddAttribute(3, "onclick", EventCallback.Factory.Create(this, OnFileClick));
            builder.AddMultipleAttributes(4, Attributes);
            builder.CloseElement();
        }

        public void Dispose()
        {
            if (EditContext is not null)
            {
                EditContext.OnFieldChanged -= OnFieldChanged;
                EditContext.OnValidationRequested -= OnValidationRequested;
            }
        }
    }
}