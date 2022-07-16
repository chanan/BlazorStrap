<BSInputGroup MarginBottom="Margins.Medium">
    <BSButton IsOutlined="true" Color="BSColor.Secondary">Action</BSButton>
    <BSDropdown>
        <Toggler><BSToggle IsButton="true" IsSplitButton="true" IsOutlined="true" Color="BSColor.Secondary"/></Toggler>
        <Content>
            <BSDropdownItem>Action</BSDropdownItem>
            <BSDropdownItem>Another action</BSDropdownItem>
            <BSDropdownItem>Something else here</BSDropdownItem>
            <BSDropdownItem IsDivider="true"/>
            <BSDropdownItem>Separated link</BSDropdownItem>
        </Content>
    </BSDropdown>
    <BSInput InputType="InputType.Text" Value="@("")"/>
</BSInputGroup>

<BSInputGroup MarginBottom="Margins.Medium">
    <BSInput InputType="InputType.Text"  Value="@("")"/>
    <BSButton IsOutlined="true" Color="BSColor.Secondary">Action</BSButton>
    <BSDropdown Placement="Placement.BottomEnd">
        <Toggler><BSToggle IsButton="true" IsSplitButton="true" IsOutlined="true" Color="BSColor.Secondary"/></Toggler>
        <Content>
            <BSDropdownItem>Action</BSDropdownItem>
            <BSDropdownItem>Another action</BSDropdownItem>
            <BSDropdownItem>Something else here</BSDropdownItem>
            <BSDropdownItem IsDivider="true" />
            <BSDropdownItem>Separated link</BSDropdownItem>
        </Content>
    </BSDropdown>
</BSInputGroup>