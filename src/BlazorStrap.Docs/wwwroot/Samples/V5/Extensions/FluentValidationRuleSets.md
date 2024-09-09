<!--\\-->
@using global::FluentValidation
@using BlazorStrap.Extensions.FluentValidation
 <p>In this example RuleSet Name, default are used. Excuting only default rules (outside of a ruleset) and the Name RuleSet. Address is shown as valid because no rules are being applied to it. </p>
    <BSForm Model="customer" OnValidSubmit="SaveCustomer" IsFloating="true">
    <FluentValidator TValidator="CustomerValidator" RuleSets="Name,default" />

        <h3>Your name</h3>
        <BSInput InputType="InputType.Text" placeholder="First name" @bind-Value="customer.FirstName" />
        <ValidationMessage For="@(() => customer.FirstName)" />
        <BSInput InputType="InputType.Text" placeholder="Last name" @bind-Value="customer.LastName" MarginTop="Margins.Medium"/>
        <ValidationMessage For="@(() => customer.LastName)" />

        <h3>Your address</h3>
        <div>
        <BSInput InputType="InputType.Text" placeholder="Line 1" @bind-Value="customer.Address.Line1" MarginTop="Margins.Medium" />
            <ValidationMessage For="@(() => customer.Address.Line1)"  />
        </div>
        <div>
        <BSInput InputType="InputType.Text" placeholder="City" @bind-Value="customer.Address.City" MarginTop="Margins.Medium" />
            <ValidationMessage For="@(() => customer.Address.City)" />
        </div>
        <div>
        <BSInput InputType="InputType.Text" placeholder="Postcode" @bind-Value="customer.Address.Postcode" MarginTop="Margins.Medium" />
            <ValidationMessage For="@(() => customer.Address.Postcode)" />
        </div>

        <p><BSButton IsSubmit="true" Color="BSColor.Primary" MarginTop="Margins.Medium">Submit</BSButton></p>
    </BSForm>
<!--//-->
@code {
    //<!--\\-->
    private Customer customer = new Customer();

    void SaveCustomer()
    {

    }
    
    public class Customer
    {
        public bool Checked { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; } = new Address();
    }

    public class Address
    {
        public string Line1 { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
    }

    //<!--//-->
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.FirstName).NotEmpty().MaximumLength(50);
            RuleSet("Name", () => {
                RuleFor(customer => customer.LastName).NotEmpty().MaximumLength(50);
            });
            RuleSet("Address", () => {
                RuleFor(customer => customer.Address.Line1).NotEmpty();
                RuleFor(customer => customer.Address.City).NotEmpty();
                RuleFor(customer => customer.Address.Postcode).NotEmpty().MaximumLength(10);
            });
        }
    }
}