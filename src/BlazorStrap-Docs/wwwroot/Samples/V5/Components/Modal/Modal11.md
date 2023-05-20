
<BSModal DataId="modal11" ContentAlwaysRendered="false" IsCentered="true" Size="Size.ExtraLarge" IsManual="true" @ref="FirstModal" style="height:600px">
    <Header>First Modal</Header>
    <Content>
        <div style="height:500px">Display two modals are one time</div>
        <BSButton Color="BSColor.Primary" OnClick="ShowSecondModalAsync">Launch second modal</BSButton>
    </Content>
    <Footer Context="modal">
        <BSButton MarginStart="Margins.Auto" Color="BSColor.Secondary" @onclick="modal.HideAsync">Close</BSButton>
    </Footer>
</BSModal>
<BSButton Color="BSColor.Primary" OnClick="ShowFirstModalAsync">Launch first modal</BSButton>

<BSModal ContentAlwaysRendered="false" IsManual="true" @ref="SecondModal" ShowBackdrop="false" IsCentered="true">
    <Header>Second Modal</Header>
    <Content>Second Modal</Content>
    <Footer Context="modal">
        <BSButton MarginStart="Margins.Auto" Color="BSColor.Secondary" @onclick="modal.HideAsync">Close</BSButton>
    </Footer>
</BSModal>

<h5>Nested</h5>
<BSModal DataId="modal11" ContentAlwaysRendered="false" IsCentered="true" Size="Size.ExtraLarge" IsManual="true" @ref="ParentModal">
    <Header>Parent Modal</Header>
    <Content>
        <div style="height:500px">Display two modals are one time</div>
        <BSModal ContentAlwaysRendered="false" IsManual="true" @ref="ChildModal" ShowBackdrop="false" IsCentered="true" Context="child">
            <Header>Child Modal</Header>
            <Content>Child Modal</Content>
            <Footer Context="modal">
                <BSButton MarginStart="Margins.Auto" Color="BSColor.Secondary" @onclick="modal.HideAsync">Close</BSButton>
            </Footer>
        </BSModal>
        <BSButton Color="BSColor.Primary" OnClick="ShowChildModalAsync">Launch child modal</BSButton>
    </Content>
    <Footer Context="modal">
        <BSButton MarginStart="Margins.Auto" Color="BSColor.Secondary" @onclick="modal.HideAsync">Close</BSButton>
    </Footer>
</BSModal>
<BSButton Color="BSColor.Primary" OnClick="ShowParentModalAsync">Launch Parent modal</BSButton>


@code {
    private BSModal? FirstModal { get; set; }
    private BSModal? SecondModal { get; set; }
    private BSModal? ParentModal { get; set; }
    private BSModal? ChildModal { get; set; }
    private async Task ShowFirstModalAsync()
    {
        if (FirstModal != null)
            await FirstModal.ShowAsync();
    }
    private async Task ShowSecondModalAsync()
    {
        if (SecondModal != null)
            await SecondModal.ShowAsync();
    }
    private async Task ShowParentModalAsync()
    {
        if (ParentModal != null)
            await ParentModal.ShowAsync();
    }
    private async Task ShowChildModalAsync()
    {
        if (ChildModal != null)
            await ChildModal.ShowAsync();
    }
}