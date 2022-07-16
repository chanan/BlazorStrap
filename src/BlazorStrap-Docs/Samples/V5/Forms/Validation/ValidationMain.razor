@using System.ComponentModel.DataAnnotations
<BSForm Model="Modal" IsRow="true" Gutters="Gutters.Medium" OnSubmit="OnSubmit" OnReset="Reset">

    <DataAnnotationsValidator />
    <BSCol Position="Position.Relative" ColumnMedium="12">
        @_message
        <BSValidationSummary />
    </BSCol>
    <BSCol Position="Position.Relative" ColumnMedium="4">
        <BSLabel>First name</BSLabel>
        <BSInput InputType="InputType.Text" @bind-Value="Modal.FirstName" ValidateOnInput="true" />
        <BSFeedback For="@(() => Modal.FirstName)" ValidMessage="First name looks good." />
    </BSCol>
    <BSCol Position="Position.Relative" ColumnMedium="4">
        <BSLabel>Middle name</BSLabel>
        <BSInput InputType="InputType.Text" @bind-Value="Modal.MiddleName" />
        <BSFeedback For="@(() => Modal.MiddleName)" ValidMessage="Middle name looks good." />
    </BSCol>
    <BSCol Position="Position.Relative" ColumnMedium="4">
        <BSLabel>Last name</BSLabel>
        <BSInput InputType="InputType.Text" @bind-Value="Modal.LastName" />
        <BSFeedback For="@(() => Modal.LastName)" ValidMessage="Last name looks good." />
    </BSCol>
    <BSCol Position="Position.Relative" ColumnMedium="4">
        <BSLabel>Email Address</BSLabel>
        <BSInput InputType="InputType.Text" @bind-Value="Modal.Email" placeholder="Email Address" />
        <BSFeedback For="@(() => Modal.Email)" ValidMessage="Email address looks good." />
    </BSCol>
    <BSCol Position="Position.Relative" ColumnMedium="6">
        <BSLabel>Photo</BSLabel>
        <BSInputFile ValidWhen="@(() => Modal.HasPendingPhoto)" OnChange="OnFileChange" />
        <BSFeedback For="@(() => Modal.HasPendingPhoto)" ValidMessage="Looks like you selected a photo." />
    </BSCol>
    <BSCol Column="12">
        <BSButton Color="BSColor.Primary" IsSubmit="true">Submit</BSButton>
        <BSButton Color="BSColor.Primary" IsReset="true" @onclick="Reset">Reset</BSButton>
    </BSCol>
</BSForm>
@code {
    private EmployeeModal Modal { get; set; } = new EmployeeModal();
    private string _message = "";
    private void OnFileChange(InputFileChangeEventArgs e)
    {
        Modal.HasPendingPhoto = null;
        if (e.FileCount > 0)
        {
            Modal.HasPendingPhoto = true;
            Modal.PhotoName = e.File.Name;
        }
    }
    private void OnSubmit(EditContext e)
    {
        if (e.Validate())
        {
            _message = "Submitted to database";
        }
    }
    public class EmployeeModal
    {
        [Required(ErrorMessage = "Employee's First name must be provided.")]
        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Employee's Last name must be provided.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Employee's Email address must be provided.")]
        [EmailAddress(ErrorMessage = "Employee's Email address must be valid.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Employee must have a photo")]
        public bool? HasPendingPhoto { get; set; }
        public string? PhotoName { get; set; }
    }
    public void Reset()
    {
        Modal = new EmployeeModal();
    }
}