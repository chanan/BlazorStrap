<!--\\-->
@using BlazorStrap_Docs.SamplesHelpers.Content.Tables
<BSToaster/>
<!--//-->
@inject IBlazorStrap blazorStrap

<BSModal DataId="model9" OnShow="@(() => NewEmployee(new Employee()))" HideOnValidSubmit="true" IsStaticBackdrop="true">
    <BSForm Model="model" OnValidSubmit="@Update">
        <DataAnnotationsValidator/>
        <BSModalHeader>New Employee Form</BSModalHeader>
        <BSModalContent>
            <BSValidationSummary/>
            <div class="mb-3">
                <BSLabel>Name</BSLabel>
                <BSInput InputType="InputType.Text" @bind-Value="model.Name"/>
                <BSFeedback For="@(() => model.Name)"/>
            </div>
            <div class="mb-3">
                <BSLabel>Email address</BSLabel>
                <BSInput InputType="InputType.Email" placeholder="name@example.com" @bind-Value="model.Email"/>
                <BSFeedback For="@(() => model.Email)"/>
            </div>
        </BSModalContent>
        <BSModalFooter>
            <BSButton Target="model9">Cancel</BSButton>
            <BSButton IsSubmit="true" Color="BSColor.Primary">Save</BSButton>
        </BSModalFooter>
    </BSForm>
</BSModal>

<BSButton Color="BSColor.Primary" Target="model9">New Employee</BSButton>

@code {
    BSModal refModal;
    private Employee model = new Employee();

    private async Task NewEmployee(Employee e)
    {
        model = e;
    }

    private async Task Update()
    {
        blazorStrap.Toaster.Add("Fake record added!", options => options.CloseAfter = 5000);
    }

}