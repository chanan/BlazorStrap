<BSModal IsCentered="true" DataId="modal6">
    <Header>Modal Title</Header>
    <Content>
        <h5>Popover in a modal</h5>
        this <BSButton Color="BSColor.Secondary" DataId="popover1">button</BSButton> triggers a popover on click.
        <hr/>
        <h5>Tooltips in a modal</h5>
        <a href="javascript:void(0);" data-blazorstrap="tooltip1">This link</a> and <a href="javascript:void(0);" data-blazorstrap="tooltip2">that link</a> have tooltips on hover
        <BSPopover Placement="Placement.Top" Target="popover1">
            <Header>Popover title</Header>
            <Content Context="popover">Popover body content is set in this attribute.</Content>
        </BSPopover>
        <BSTooltip Target="tooltip1">Tooltip</BSTooltip>
        <BSTooltip Target="tooltip2">Tooltip</BSTooltip>
    </Content>
    <Footer Context="modal">
        <BSButton MarginStart="Margins.Auto" Color="BSColor.Secondary" @onclick="modal.HideAsync" >Close</BSButton>
        <BSButton Color="BSColor.Primary">Understood</BSButton>
    </Footer>
</BSModal>
<BSButton Color="BSColor.Primary" Target="modal6">Launch demo modal</BSButton>