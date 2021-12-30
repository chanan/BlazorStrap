<BSModal IsCentered="true" DataId="modal5">
    <Header>Modal Title</Header>
    <Content>This is a vertically centered modal.</Content>
    <Footer Context="modal">
        <BSButton MarginStart="Margins.Auto" Color="BSColor.Secondary" @onclick="modal.HideAsync">Close</BSButton>
        <BSButton Color="BSColor.Primary">Understood</BSButton>
    </Footer>
</BSModal>
<BSModal IsCentered="true" IsScrollable="true" DataId="modal5a">
    <Header>Modal Title</Header>
    <Content>
        <p>This is some placeholder content to show the scrolling behavior for modals. We use repeated line breaks to demonstrate how content can exceed minimum inner height, thereby showing inner scrolling. When content becomes longer than the prefedined max-height of modal, content will be cropped and scrollable within the modal.</p>
        <br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br>
        <p>This content should appear at the bottom after you scroll.</p>
    </Content>
    <Footer Context="modal">
        <BSButton MarginStart="Margins.Auto" Color="BSColor.Secondary" @onclick="modal.HideAsync">Close</BSButton>
        <BSButton Color="BSColor.Primary">Understood</BSButton>
    </Footer>
</BSModal>
<BSButton Color="BSColor.Primary" Target="modal5">Vertically centered modal</BSButton>
<BSButton Color="BSColor.Primary" Target="modal5a">Vertically centered scrollable modal</BSButton>