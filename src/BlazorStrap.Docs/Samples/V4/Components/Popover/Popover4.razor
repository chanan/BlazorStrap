@inject IBlazorStrap _blazorStrap
<BSToaster />
<BSButton IsOutlined="true" Color="BSColor.Primary" DataId="delconfirm" OnClick="ShowAsync">Fake Delete</BSButton>
<BSPopover Placement="Placement.Top" Target="delconfirm" ContentAlwaysRendered="false" @ref="_popover" NoClickEvent="true">
    <Header>Delete Confirmation</Header>
    <Content Context="popover">
        <div class="d-flex flex-column">
        <div>Are you sure you want to delete this file?</div>
        <div class="text-center">
            <BSButton Color="BSColor.Danger" OnClick="()=>popover.HideAsync(true)">Yes</BSButton>
            <BSButton Color="BSColor.Secondary" OnClick="()=>popover.HideAsync(false)">No</BSButton>
        </div>
        </div>
    </Content>
</BSPopover>
<BSButton IsOutlined="true" Color="BSColor.Primary" DataId="dynamicdelconfirm" OnClick="ShowDynamicAsync">Fake Delete Dynamic</BSButton>
<BSPopover @ref="_dynamicPopover">
    <Content Context="popover">
        <div class="d-flex flex-column">
            <div>Are you sure you want to delete this file?</div>
            <div class="text-center">
                <BSButton Color="BSColor.Danger" OnClick="()=>popover.HideAsync(true)">Yes</BSButton>
                <BSButton Color="BSColor.Secondary" OnClick="()=>popover.HideAsync(false)">No</BSButton>
            </div>
        </div>
    </Content>
</BSPopover>
@code
{
    private BSPopover _popover;
    private BSPopover _dynamicPopover;

    private async Task ShowDynamicAsync()
    {
        var result = await _dynamicPopover.ShowAsync(true, "dynamicdelconfirm", null, Placement.Top, "Delete Confirmation");
        if (result)
        {
            _blazorStrap.Toaster.Add("Fake File Deleted", o =>
            {
                o.Color = BSColor.Success;
                o.HasIcon = true;
                o.CloseAfter = 3000;
            });
        }
        else
        {
            _blazorStrap.Toaster.Add("Fake File Not Deleted", o =>
            {
                o.Color = BSColor.Danger;
                o.HasIcon = true;
                o.CloseAfter = 3000;
            });
        }
    }
    private async Task ShowAsync()
    {
        var result = await _popover.ShowAsync(true);
        if (result)
        {
            _blazorStrap.Toaster.Add("Fake File Deleted", o =>
            {
                o.Color = BSColor.Success;
                o.HasIcon = true;
                o.CloseAfter = 3000;
            });
        }
        else
        {
            _blazorStrap.Toaster.Add("Fake File Not Deleted", o =>
            {
                o.Color = BSColor.Danger;
                o.HasIcon = true;
                o.CloseAfter = 3000;
            });
        }
    }
}