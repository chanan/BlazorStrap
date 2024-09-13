<!--\\-->
@using BlazorStrap_Docs.SampleModels
<!--//-->

<BSDropdown Demo="true">
    <Content>
        <BSForm Model="model" Class="px-4 py-3">
            <div class="mb-3">
                <BSLabel for="exampleDropdownFormEmail2">Email address</BSLabel>
                <BSInput InputType="InputType.Email" @bind-Value="model.Email" id="exampleDropdownFormEmail2" placeholder="email@example.com"/>
            </div>
            <div class="mb-3">
                <BSLabel for="exampleDropdownFormPassword2">Email address</BSLabel>
                <BSInput InputType="InputType.Password" @bind-Value="model.Password" id="exampleDropdownFormPassword2" placeholder="Password"/>
            </div>
            <div class="mb-3">
                <div class="form-check">
                    <BSInputCheckbox @bind-Value="model.RememberMe" id="RememberMeCheck1"/>
                    <BSLabel for="RememberMeCheck1">Remember me</BSLabel>
                </div>
            </div>
            <BSButton Color="BSColor.Primary" >Sign In</BSButton>
        </BSForm>
    </Content>
</BSDropdown>

@code {
    private DropdownFormModel model = new DropdownFormModel();
}