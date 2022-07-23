@using global::FluentValidation
@using BlazorStrap.Extensions.FluentValidation
    <BSForm Model="user" OnValidSubmit="SaveUser">
        <FluentValidatorInjectable TValidator="UserValidator" Context="names" />

        <div class="mb-3">
            <BSLabel for="name">Enter a new username</BSLabel>
            <BSInput id="name" InputType="InputType.Text" placeholder="Name" @bind-Value="user.Name" />
            <BSFeedback For="@(() => user.Name)" />
        </div>

        <p><BSButton IsSubmit="true" Color="BSColor.Primary">Submit</BSButton></p>
    </BSForm>
@code {
    private User user = new();
    private List<string> names = new() { "bob", "mary", "alice" };

    void SaveUser()
    {
    }
    
    public class User
    {
        public string? Name { get; set; }
    }

    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator(HttpClient http, List<string> existingNames)
        {
            // A validator shouldn't ordinarily include an HttpClient; it's just a demonstration of injecting a service.
            // A more common service would be to inject an IStringLocalizer<T> to provide localized error messages.

            RuleFor(user => user.Name).NotEmpty().MaximumLength(50);
            RuleFor(user => user.Name).Must(value => BeUnique(value, existingNames)).WithMessage("That name is already in use.");
        }

        private static bool BeUnique(string? value, List<string> existingValues)
        {
            return !existingValues.Any(v => v.Equals(value, StringComparison.OrdinalIgnoreCase));
        }
    }
}