<BSModal IsStaticBackdrop="true" DataId="modal2">
    <Header>Modal Title</Header>
    <Content>I will not close if you click outside me.</Content>
    <Footer Context="modal">
        <BSButton MarginStart="Margins.Auto" Color="BSColor.Secondary" @onclick="modal.HideAsync">Close</BSButton>
        <BSButton Color="BSColor.Primary">Understood</BSButton>
    </Footer>
</BSModal>
<BSButton Color="BSColor.Primary" Target="modal2">Launch demo modal</BSButton>