<BSInputGroup MarginBottom="Margins.Medium">
    <BSInputGroup IsPrepend="true">
        <BSButton IsOutlined="true" Color="BSColor.Secondary">Action</BSButton>
    </BSInputGroup>
    <BSDropdown>
        <Toggler><BSToggle IsButton="true" IsSplitButton="true" IsOutlined="true" Color="BSColor.Secondary" /></Toggler>
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
    <BSButton IsOutlined="true" Color="BSColor.Secondary">Action</BSButton>
    <BSInputGroup IsAppend="true">
        <BSDropdown Placement="Placement.BottomEnd">
            <Toggler><BSToggle IsButton="true" IsSplitButton="true" IsOutlined="true" Color="BSColor.Secondary" /></Toggler>
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