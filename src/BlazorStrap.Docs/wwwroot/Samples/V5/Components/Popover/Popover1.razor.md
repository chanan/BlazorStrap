<BSButton IsOutlined="true" Color="BSColor.Primary" DataId="popoverLeft" >Left</BSButton>
<BSPopover Placement="Placement.Left" Target="popoverLeft" PopperOptions="@(new { strategy  = "fixed"})">
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