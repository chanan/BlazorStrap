<BSButtonGroup>
    <BSDropdown Offset="10,20">
        <Toggler>
            <BSToggle IsButton="true" Color="BSColor.Secondary">Offset</BSToggle>
        </Toggler>
        <Content>
            <BSDropdownItem IsButton="true">Action</BSDropdownItem>
            <BSDropdownItem IsButton="true">Another action</BSDropdownItem>
            <BSDropdownItem IsButton="true">Something else here</BSDropdownItem>
        </Content>
    </BSDropdown>
</BSButtonGroup>
<BSButtonGroup>
    <BSButton Color="BSColor.Secondary" DataId="dropdownTarget">Reference</BSButton>
    <BSButton Color="BSColor.Secondary" Class="dropdown-toggle dropdown-toggle-split" Target="dropref"></BSButton>
    <BSDropdown DataId="dropref" Target="dropdownTarget">
        <Content>
            <BSDropdownItem IsButton="true">Action</BSDropdownItem>
            <BSDropdownItem IsButton="true">Another action</BSDropdownItem>
            <BSDropdownItem IsButton="true">Something else here</BSDropdownItem>
        </Content>
    </BSDropdown>
</BSButtonGroup>
<BSButtonGroup>
    <BSDropdown>
        <Toggler>
            <BSToggle IsButton="true" Color="BSColor.Secondary">Default Dropdown</BSToggle>
        </Toggler>
        <Content>
            <BSDropdownItem>Action</BSDropdownItem>
            <BSDropdownItem>Another action</BSDropdownItem>
            <BSDropdownItem>Something else here</BSDropdownItem>
        </Content>
    </BSDropdown>
</BSButtonGroup>
<BSButtonGroup>
    <BSDropdown AllowOutsideClick="true">
        <Toggler>
            <BSToggle IsButton="true" Color="BSColor.Secondary">Clickable outside</BSToggle>
        </Toggler>
        <Content>
            <BSDropdownItem>Action</BSDropdownItem>
            <BSDropdownItem>Another action</BSDropdownItem>
            <BSDropdownItem>Something else here</BSDropdownItem>
        </Content>
    </BSDropdown>
</BSButtonGroup>
<BSButtonGroup>
    <BSDropdown AllowItemClick="true">
        <Toggler>
            <BSToggle IsButton="true" Color="BSColor.Secondary">Clickable inside</BSToggle>
        </Toggler>
        <Content>
            <BSDropdownItem>Action</BSDropdownItem>
            <BSDropdownItem>Another action</BSDropdownItem>
            <BSDropdownItem>Something else here</BSDropdownItem>
        </Content>
    </BSDropdown>
</BSButtonGroup>
<BSButtonGroup>
    <BSDropdown IsManual="true">
        <Toggler>
            <BSToggle IsButton="true" Color="BSColor.Secondary">Manual close</BSToggle>
        </Toggler>
        <Content>
            <BSDropdownItem>Action</BSDropdownItem>
            <BSDropdownItem>Another action</BSDropdownItem>
            <BSDropdownItem>Something else here</BSDropdownItem>
        </Content>
    </BSDropdown>
</BSButtonGroup>