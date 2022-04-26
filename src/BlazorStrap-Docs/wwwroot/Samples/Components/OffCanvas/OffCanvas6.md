<!--\\-->
@using BlazorStrap_Docs.SamplesHelpers.Content.Tables
<!--//-->
@inject IBlazorStrap blazorStrap

<BSOffCanvas DataId="offcanvas6" OnShow="@(() => NewEmployee(new Employee()))">
    <BSForm Model="model" OnValidSubmit="@Update">
        <BSOffCanvasHeader>New Employee Form</BSOffCanvasHeader>
        <BSOffCanvasContent>
            <div class="mb-3">
                <BSLabel>Name</BSLabel>
                <BSInput InputType="InputType.Text" @bind-Value="model.Name" />
            </div>
            <div class="mb-3">
                <BSLabel>Email address</BSLabel>
                <BSInput InputType="InputType.Email" placeholder="name@example.com" @bind-Value="model.Email" />
            </div>
            <div class="mb-3">
                <BSButton Target="offcanvas6">Cancel</BSButton>
                <BSButton IsSubmit="true" Color="BSColor.Primary">Save</BSButton>
            </div>
        </BSOffCanvasContent>
    </BSForm>
</BSOffCanvas>

<BSButton Color="BSColor.Primary" Target="offcanvas6">New Employee</BSButton>

@code {
    BSModal refModal;
    private Employee model = new Employee();
    private async Task NewEmployee(Employee e)
    {
        model = e;
    }
    private async Task Update()
    {
        blazorStrap.ForwardClick("offcanvas6");
    }
}