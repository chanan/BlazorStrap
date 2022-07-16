<BSButton Color="BSColor.Primary" @onclick="OpenCanvas">Enable Body Scrolling</BSButton>
<BSButton Color="BSColor.Primary" @onclick="OpenCanvas2">Enable backdrop (default)</BSButton>
<BSButton Color="BSColor.Primary" @onclick="OpenCanvas3">Enable both scrolling & backdrop</BSButton>
<BSOffCanvas @ref="_offCanvas" ShowBackdrop="false" AllowScroll="true">
    <Header>Enable Body Scrolling</Header>
    <Content>
        ...
    </Content>
</BSOffCanvas>
<BSOffCanvas @ref="_offCanvas2">
    <Header>Enable backdrop (default)</Header>
    <Content>
        ...
    </Content>
</BSOffCanvas>
<BSOffCanvas @ref="_offCanvas3" AllowScroll="true">
    <Header>Enable both scrolling & backdrop</Header>
    <Content>
        ...
    </Content>
</BSOffCanvas>
@code {
    private BSOffCanvas? _offCanvas;
    private BSOffCanvas? _offCanvas2;
    private BSOffCanvas? _offCanvas3;
    private async Task OpenCanvas()
    {
        if (_offCanvas != null)
            await _offCanvas.ToggleAsync();
    }
    private async Task OpenCanvas2()
    {
        if (_offCanvas2 != null)
            await _offCanvas2.ToggleAsync();
    }
    private async Task OpenCanvas3()
    {
        if (_offCanvas3 != null)
            await _offCanvas3.ToggleAsync();
    }
}