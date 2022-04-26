<!--\\-->
@using BlazorStrap_Docs.SamplesHelpers.Content.Tables
<!--//-->
@inject IBlazorStrap blazorStrap

<BSModal DataId="model9" OnShow="@(() => NewEmployee(new Employee()))">
    <BSForm Model="model" OnValidSubmit="@Update">
        <BSModalHeader>New Employee Form</BSModalHeader>
        <BSModalContent>
            <div class="mb-3">
                <BSLabel>Name</BSLabel>
                <BSInput InputType="InputType.Text" @bind-Value="model.Name" />
            </div>
            <div class="mb-3">
                <BSLabel>Email address</BSLabel>
                <BSInput InputType="InputType.Email" placeholder="name@example.com" @bind-Value="model.Email" />
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
        blazorStrap.ForwardClick("model9");
    }
}