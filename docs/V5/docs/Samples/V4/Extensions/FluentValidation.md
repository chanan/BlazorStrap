@using global::FluentValidation
@using BlazorStrap.Extensions.FluentValidation
 <p>This example was taken copied from Steve Sanderson fluent validator project. </p>
    <BSForm Model="customer" OnValidSubmit="SaveCustomer">
        <FluentValidator TValidator="CustomerValidator" />

        <h3>Your name</h3>
        <BSInput InputType="InputType.Text" placeholder="First name" @bind-Value="customer.FirstName" MarginBottom="Margins.Medium"/>
        <ValidationMessage For="@(() => customer.FirstName)" />
        <BSInput InputType="InputType.Text" placeholder="Last name" @bind-Value="customer.LastName" MarginBottom="Margins.Medium"/>
        <ValidationMessage For="@(() => customer.LastName)" />

        <h3>Your address</h3>
        <div>
            <BSInput InputType="InputType.Text" placeholder="Line 1" @bind-Value="customer.Address.Line1" MarginBottom="Margins.Medium"/>
            <ValidationMessage For="@(() => customer.Address.Line1)" />
        </div>
        <div>
            <BSInput InputType="InputType.Text" placeholder="City" @bind-Value="customer.Address.City" MarginBottom="Margins.Medium"/>
            <ValidationMessage For="@(() => customer.Address.City)" />
        </div>
        <div>
            <BSInput InputType="InputType.Text" placeholder="Postcode" @bind-Value="customer.Address.Postcode" MarginBottom="Margins.Medium"/>
            <ValidationMessage For="@(() => customer.Address.Postcode)" />
        </div>

        <h3>
            Payment methods
            [<a href="/Extensions" @onclick="AddPaymentMethod" @onclick:preventDefault>Add new</a>]
        </h3>
        <ValidationMessage For="@(() => customer.PaymentMethods)" />

        @foreach (var paymentMethod in customer.PaymentMethods)
        {
            <p>
                <BSInput InputType="InputType.Select" @bind-Value="paymentMethod.MethodType" MarginBottom="Margins.Medium">
                    <option value="@PaymentMethod.Type.CreditCard">Credit card</option>
                    <option value="@PaymentMethod.Type.HonourSystem">Honour system</option>
                </BSInput>

                @if (paymentMethod.MethodType == PaymentMethod.Type.CreditCard)
                {
                    <BSInput InputType="InputType.Text" placeholder="Card number" @bind-Value="paymentMethod.CardNumber" MarginBottom="Margins.Medium"/>
                }
                else if (paymentMethod.MethodType == PaymentMethod.Type.HonourSystem)
                {
                    <span>Sure, we trust you to pay us somehow eventually</span>
                }
                <ValidationMessage For="@(() => paymentMethod.CardNumber)" />

                <BSButton @onclick="@(() => customer.PaymentMethods.Remove(paymentMethod))" Color="BSColor.Danger">Remove Payment method</BSButton>


            </p>
        }

        <p><BSButton IsSubmit="true" Color="BSColor.Primary">Submit</BSButton></p>
    </BSForm>
@code {
    private Customer customer = new Customer();

    void AddPaymentMethod()
    {
        customer.PaymentMethods.Add(new PaymentMethod());
    }

    void SaveCustomer()
    {

    }
    
    public class Customer
    {
        public bool Checked { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; } = new Address();
        public List<PaymentMethod> PaymentMethods { get; } = new List<PaymentMethod>();
    }

    public class Address
    {
        public string Line1 { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
    }

    public class PaymentMethod
    {
        public enum Type { CreditCard, HonourSystem }

        public Type MethodType { get; set; }

        public string CardNumber { get; set; }
    }
    
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