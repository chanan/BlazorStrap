<a class="btn btn-primary" href="javascript:void(0);" @onclick="OpenCanvas">Link</a>
<BSButton Color="BSColor.Primary" @onclick="OpenCanvas">Button</BSButton>
<BSOffCanvas @ref="_offCanvas">
    <Header>Offcanvas</Header>
    <Content>
        <div>
            Some text as placeholder. In real life you can have the elements you have chosen. Like, text, images, lists, etc.
        </div>
        <BSDropdown>
            <Toggler></Toggler>
            <Content>
                <BSDropdownItem Url="javascript:void(0)">Action</BSDropdownItem>
                <BSDropdownItem Url="javascript:void(0)">Another action</BSDropdownItem>
                <BSDropdownItem Url="javascript:void(0)">Something else here</BSDropdownItem>
            </Content>
        </BSDropdown>
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