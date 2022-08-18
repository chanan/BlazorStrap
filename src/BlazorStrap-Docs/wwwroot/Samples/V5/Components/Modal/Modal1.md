@*<BSModal DataId="modal1">
    <Header>Modal Title</Header>
    <Content>Woohoo, you're reading this text in a modal!</Content>
    <Footer Context="modal">
        <BSButton MarginStart="Margins.Auto" Color="BSColor.Secondary" @onclick="modal.HideAsync">Close</BSButton>
        <BSButton Color="BSColor.Primary">Save changes</BSButton>
    </Footer>
</BSModal>
<BSButton Color="BSColor.Primary" Target="modal1">Launch demo modal</BSButton>
*@

<div>
    <BSModal IsStaticBackdrop="true" HasCloseButton="false" @ref="Modal">
        <Header>Modal Title</Header>
        <Content>This modal prompts the user to do something and closes when it is complete or times out.</Content>
    </BSModal>
    <BSButton Color="BSColor.Primary" OnClick="LaunchModal">Launch demo modal</BSButton>
</div>

@code {
    public BSModal? Modal { get; set; }
    public async Task LaunchModal()
    {
        for (int i = 0; i < 100; ++i)
        {
            Console.WriteLine(i);
            await Modal!.ShowAsync();
            await Modal!.HideAsync();
        }
    }
}