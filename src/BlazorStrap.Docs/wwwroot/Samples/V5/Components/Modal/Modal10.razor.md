<BSModal DataId="modal10" ContentAlwaysRendered="true">
    <Header>Modal Title</Header>
    <Content>Woohoo, you're reading this text in a modal!</Content>
    <Footer Context="modal">
        <BSButton MarginStart="Margins.Auto" Color="BSColor.Secondary" @onclick="modal.HideAsync">Close</BSButton>
        <BSButton Color="BSColor.Primary">Save changes</BSButton>
    </Footer>
</BSModal>
<BSButton Color="BSColor.Primary" Target="modal10">Launch demo modal</BSButton>
