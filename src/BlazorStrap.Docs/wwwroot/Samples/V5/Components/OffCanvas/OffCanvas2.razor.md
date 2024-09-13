<BSButton Color="BSColor.Primary" @onclick="OpenCanvas">Toggle Top offcanvas</BSButton>
<BSOffCanvas @ref="_offCanvas" Placement="Placement.Top">
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