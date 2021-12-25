using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace SampleCore.Pages.ExtensionsModels
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(customer => customer.LastName).NotEmpty().MaximumLength(50);
            RuleFor(customer => customer.Address).SetValidator(new AddressValidator());
            RuleFor(customer => customer.PaymentMethods).NotEmpty().WithMessage("You have to define at least one payment method");
            RuleForEach(customer => customer.PaymentMethods).SetValidator(new PaymentMethodValidator());
        }
    }

    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(address => address.Line1).NotEmpty();
            RuleFor(address => address.City).NotEmpty();
            RuleFor(address => address.Postcode).NotEmpty().MaximumLength(10);
        }
    }

    public class PaymentMethodValidator : AbstractValidator<PaymentMethod>
    {
        public PaymentMethodValidator()
        {
            RuleFor(card => card.CardNumber)
                .NotEmpty().CreditCard()
                .When(method => method.MethodType == PaymentMethod.Type.CreditCard);
        }
    }
}
