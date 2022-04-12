using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorStrap.Extensions.FluentValidation
{
    public class FluentValidator<TValidator> : BaseFluentValidator
        where TValidator : IValidator, new()
    {
        protected override IValidator CreateValidator()
        {
            return new TValidator();
        }
    }

    public class FluentValidatorInjectable<TValidator> : BaseFluentValidator
        where TValidator : IValidator
    {
        [Inject] private IServiceProvider _services { get; set; }

        protected override IValidator CreateValidator()
        {
            return ActivatorUtilities.GetServiceOrCreateInstance<TValidator>(_services);
        }
    }

    public class FluentValidatorInjected : BaseFluentValidator
    {
        [Inject] private IServiceProvider _services { get; set; }

        protected override IValidator CreateValidator()
        {
            var type = typeof(IValidator<>).MakeGenericType(_editContext.Model.GetType());
            return _services.GetRequiredService(type) as IValidator;
        }
    }

    public abstract class BaseFluentValidator : ComponentBase, IDisposable
    {
        private readonly static char[] _separators = new[] { '.', '[' };

        [Parameter] public bool ValidateAll { get; set; }

        [CascadingParameter] protected EditContext _editContext { get; set; }
        protected IValidator _validator;
        protected ValidationMessageStore _messages;
        protected abstract IValidator CreateValidator();

        protected override void OnInitialized()
        {
            _validator = CreateValidator();
            _messages = new ValidationMessageStore(_editContext);

            // Revalidate when any field changes, or if the entire form requests validation
            // (e.g., on submit)

            //_editContext.OnFieldChanged += (sender, eventArgs)
            //    => ValidateModel((EditContext)sender, messages);

            _editContext.OnFieldChanged += OnFieldChanged;
            _editContext.OnValidationRequested += OnValidationRequested;
        }

        private void OnFieldChanged(object sender, FieldChangedEventArgs e)
        {
            if (ValidateAll)
            {
                ValidateModel(_editContext, _messages);
            }
            else
            {
                ValidateModelField(_editContext, _messages, e);
            }
        }

        private void OnValidationRequested(object sender, ValidationRequestedEventArgs e)
        {
            ValidateModel(_editContext, _messages);
        }

        private void ValidateModel(EditContext editContext, ValidationMessageStore messages)
        {
#if NET5_0_OR_GREATER
            var validationResult =
                _validator.Validate(ValidationContext<object>.CreateWithOptions(editContext.Model,
                    options => options.IncludeAllRuleSets()));
#else
            var validationResult = _validator.Validate(editContext.Model);
#endif
            messages.Clear();
            foreach (var error in validationResult.Errors)
            {
                var fieldIdentifier = ToFieldIdentifier(editContext, error.PropertyName);
                messages.Add(fieldIdentifier, error.ErrorMessage);
            }
            editContext.NotifyValidationStateChanged();

        }

        private void ValidateModelField(EditContext editContext, ValidationMessageStore messages,
            FieldChangedEventArgs fieldChangedEventArgs)
        {
            var type = editContext.Model.GetType();
#if NET5_0_OR_GREATER
            var validationResult =
                _validator.Validate(ValidationContext<object>.CreateWithOptions(editContext.Model,
                    options => options.IncludeAllRuleSets()));
#else
            var validationResult = _validator.Validate(editContext.Model);
#endif
            messages.Clear(fieldChangedEventArgs.FieldIdentifier);
            foreach (var error in validationResult.Errors.Where(w => ToFieldIdentifier(editContext, w.PropertyName).Equals(fieldChangedEventArgs.FieldIdentifier)))
            {
                messages.Add(fieldChangedEventArgs.FieldIdentifier, error.ErrorMessage);
            }
            editContext.NotifyValidationStateChanged();

        }

        private static FieldIdentifier ToFieldIdentifier(EditContext editContext, string propertyPath)
        {
            // This method parses property paths like 'SomeProp.MyCollection[123].ChildProp'
            // and returns a FieldIdentifier which is an (instance, propName) pair. For example,
            // it would return the pair (SomeProp.MyCollection[123], "ChildProp"). It traverses
            // as far into the propertyPath as it can go until it finds any null instance.

            var obj = editContext.Model;

            while (true)
            {
                var nextTokenEnd = propertyPath.IndexOfAny(_separators);
                if (nextTokenEnd < 0)
                {
                    return new FieldIdentifier(obj, propertyPath);
                }

                var nextToken = propertyPath.Substring(0, nextTokenEnd);
                propertyPath = propertyPath.Substring(nextTokenEnd + 1);

                object newObj;
                if (nextToken.EndsWith("]"))
                {
                    // It's an indexer
                    // This code assumes C# conventions (one indexer named Item with one param)
                    nextToken = nextToken.Substring(0, nextToken.Length - 1);
                    var prop = obj.GetType().GetProperty("Item");
                    var indexerType = prop.GetIndexParameters()[0].ParameterType;
                    var indexerValue = Convert.ChangeType(nextToken, indexerType);
                    newObj = prop.GetValue(obj, new object[] { indexerValue });
                }
                else
                {
                    // It's a regular property
                    var prop = obj.GetType().GetProperty(nextToken);
                    if (prop == null)
                    {
                        throw new InvalidOperationException(
                            $"Could not find property named {nextToken} on object of type {obj.GetType().FullName}.");
                    }

                    newObj = prop.GetValue(obj);
                }

                if (newObj == null)
                {
                    // This is as far as we can go
                    return new FieldIdentifier(obj, nextToken);
                }

                obj = newObj;
            }
        }

        public void Dispose()
        {
            if (_editContext is null) return;
            _editContext.OnFieldChanged -= OnFieldChanged;
            _editContext.OnValidationRequested -= OnValidationRequested;
        }
    }
}