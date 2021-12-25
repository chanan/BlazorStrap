<BSButton Color="BSColor.Primary" @onclick="OpenCanvas">Toggle Right offcanvas</BSButton>
<BSOffCanvas @ref="_offCanvas" Placement="Placement.Right">
    <Header>Offcanvas</Header>
    <Content>
        ...
    </Content>
</BSOffCanvas>
@code {
    private BSOffCanvas? _offCanvas;
    private async Task OpenCanvas()
    {
        if (_offCanvas != null)
            await _offCanvas.ToggleAsync();
    }
}