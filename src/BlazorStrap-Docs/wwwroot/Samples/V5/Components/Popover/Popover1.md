<BSButton IsOutlined="true" Color="BSColor.Primary" DataId="popoverLeft">Left</BSButton>
<BSPopover Placement="Placement.Left" Target="popoverLeft">
    <Header>Left</Header>
    <Content>To the left</Content>
</BSPopover>
<BSButton IsOutlined="true" Color="BSColor.Primary" DataId="popoverTop">Top</BSButton>
<BSPopover Placement="Placement.Top" Target="popoverTop" MouseOver="true">
    <Header>Top</Header>
    <Content>Up top</Content>
</BSPopover>
<BSButton IsOutlined="true" Color="BSColor.Primary" DataId="popoverBottom">Bottom</BSButton>
<BSPopover Placement="Placement.Bottom" Target="popoverBottom">
    <Header>Bottom</Header>
    <Content>Down below</Content>
</BSPopover>
<BSButton IsOutlined="true" Color="BSColor.Primary" DataId="popoverRight">Right</BSButton>
<BSPopover Placement="Placement.Right" Target="popoverRight">
    <Header>Right</Header>
    <Content>To the right</Content>
</BSPopover>


<BSModal DataId="modal1">
    <Header>Modal Title</Header>
    <Content>Woohoo, you're reading this text in a modal!</Content>
    <Footer Context="modal">
        <BSButton MarginStart="Margins.Auto" Color="BSColor.Secondary" @onclick="modal.HideAsync">Close</BSButton>
        <BSButton Color="BSColor.Primary">Save changes</BSButton>
    </Footer>
</BSModal>
<BSButton Color="BSColor.Primary" Target="model9">New Employee</BSButton>
<BSModal DataId="model9" IsStaticBackdrop="true">
        <BSModalHeader>New Employee Form</BSModalHeader>
        <BSModalContent>
        @correctPopoverRef?.Shown
        <BSButton Color="BSColor.Primary" Target="modal1">Launch demo modal</BSButton>
        <BSButton DataId="@CorrectPopoverId" OnClick="ShowCorrectPopover"><i class="fas fa-check-circle"></i> Show</BSButton>
        <BSButton DataId="@CorrectPopoverId" OnClick="HideCorrectPopover"><i class="fas fa-check-circle"></i> Hide</BSButton>
        <BSPopover NoClickEvent="true" @key="CorrectPopoverId" @ref="correctPopoverRef" Class="mw-100" Placement="Placement.Right" Target="@CorrectPopoverId" id="@CorrectPopoverId">
            <Header>Title</Header>
            <Content>
                <Test CorrectPopoverRef="correctPopoverRef" />
            </Content>
        </BSPopover>
        </BSModalContent>
        <BSModalFooter>
            <BSButton Target="model9">Cancel</BSButton>
            <BSButton IsSubmit="true" Color="BSColor.Primary">Save</BSButton>
        </BSModalFooter>
</BSModal>


@code{
    private string CorrectPopoverId = "correctPopover";
    private BSPopover correctPopoverRef;

    private async Task ShowCorrectPopover()
    {
        await correctPopoverRef.ShowAsync();
    }
    private async Task HideCorrectPopover()
    {
        await correctPopoverRef.HideAsync();
    }
}