@inject IBlazorStrap _blazorStrap

<BSModal DataId="confirmModal" @ref="_modal">
    <Header>You are about to do something</Header>
    <Content>Are you sure you want to do something?</Content>
    <Footer Context="modal">
        <BSButton MarginStart="Margins.Auto" Color="BSColor.Secondary" @onclick="@(() => modal.HideAsync(false))">Cancel</BSButton>
        <BSButton Color="BSColor.Primary" @onclick="@(() => modal.HideAsync(true))">Ok</BSButton>
    </Footer>
</BSModal>
<BSButton Color="BSColor.Primary" OnClick="ShowModalAsync">Do something</BSButton>

@code {

    BSModal _modal = null!;

    private async Task ShowModalAsync()
    {
        var confirmed = await _modal.ShowAsync(true);
        if(confirmed)
        {
            _blazorStrap.Toaster.Add("Modal Confirmed", o => 
            {
                o.Color = BSColor.Success;
                o.HasIcon = true;
                o.CloseAfter = 5000;
            });
        }
        else
        {
            _blazorStrap.Toaster.Add("Modal Cancelled", o => 
            {
                o.Color = BSColor.Danger;
                o.HasIcon = true;
                o.CloseAfter = 5000;
            });
        }
    }
}