<BSInputGroup MarginBottom="Margins.Medium">
    <BSDropdown>
        <Toggler><BSToggle IsButton="true" IsOutlined="true" Color="BSColor.Secondary">Dropdown</BSToggle></Toggler>
        <Content>
            <BSDropdownItem>Action</BSDropdownItem>
            <BSDropdownItem>Another action</BSDropdownItem>
            <BSDropdownItem>Something else here</BSDropdownItem>
            <BSDropdownItem IsDivider="true" />
            <BSDropdownItem>Separated link</BSDropdownItem>
        </Content>
    </BSDropdown>
    <BSInputGroup IsAppend="true">
        <BSInput InputType="InputType.Text" Value="@("")" />
    </BSInputGroup>
</BSInputGroup>

<BSInputGroup MarginBottom="Margins.Medium">
    <BSInputGroup IsPrepend="true">
        <BSInput InputType="InputType.Text" Value="@("")" />
    </BSInputGroup>
    <BSDropdown Placement="Placement.BottomEnd">
        <Toggler><BSToggle IsButton="true" IsOutlined="true" Color="BSColor.Secondary">Dropdown</BSToggle></Toggler>
        <Content>
            <BSDropdownItem>Action</BSDropdownItem>
            <BSDropdownItem>Another action</BSDropdownItem>
            <BSDropdownItem>Something else here</BSDropdownItem>
            <BSDropdownItem IsDivider="true" />
            <BSDropdownItem>Separated link</BSDropdownItem>
        </Content>
    </BSDropdown>
</BSInputGroup>

<BSInputGroup MarginBottom="Margins.Medium">
    <BSInputGroup IsPrepend="true">
        <BSDropdown>
            <Toggler><BSToggle IsButton="true" IsOutlined="true" Color="BSColor.Secondary">Dropdown</BSToggle></Toggler>
            <Content>
                <BSDropdownItem>Action before</BSDropdownItem>
                <BSDropdownItem>Another action before</BSDropdownItem>
                <BSDropdownItem>Something else here</BSDropdownItem>
                <BSDropdownItem IsDivider="true" />
                <BSDropdownItem>Separated link</BSDropdownItem>
            </Content>
        </BSDropdown>
    </BSInputGroup>
    <BSInput InputType="InputType.Text" Value="@("")" />
    <BSInputGroup IsAppend="true">
        <BSDropdown Placement="Placement.BottomEnd">
            <Toggler><BSToggle IsButton="true" IsOutlined="true" Color="BSColor.Secondary">Dropdown</BSToggle></Toggler>
            <Content>
                <BSDropdownItem>Action</BSDropdownItem>
                <BSDropdownItem>Another action</BSDropdownItem>
                <BSDropdownItem>Something else here</BSDropdownItem>
                <BSDropdownItem IsDivider="true" />
                <BSDropdownItem>Separated link</BSDropdownItem>
            </Content>
        </BSDropdown>
    </BSInputGroup>
</BSInputGroup>