<BSNavbar Expand="Size.Large">
    <BSContainer Container="Container.Fluid">
        <BSCollapse IsInNavbar="true">
            <Toggler>
                <BSNavbarToggle/>
            </Toggler>
            <Content>
                <BSNav MarginEnd="Margins.Auto" MarginBottom="Margins.Small" class="mb-lg-0">
                    <BSNavItem IsActive="true">Active</BSNavItem>
                    <BSNavItem IsDropdown="true">
                        <BSDropdown>
                            <Toggler>
                                <BSToggle IsNavLink="true">Dropdown</BSToggle>
                            </Toggler>
                            <Content>
                                <BSDropdownItem Url="javascript:void(0);">Action</BSDropdownItem>
                                <BSDropdownItem Url="javascript:void(0);">Another action</BSDropdownItem>
                                <BSDropdownItem Url="javascript:void(0);">Something else here</BSDropdownItem>
                                <BSDropdownItem IsDivider="true"/>
                                <BSDropdownItem IsSubmenu="true">
                                    <BSDropdown>
                                        <Toggler>
                                            <BSToggle>Submenu</BSToggle>
                                        </Toggler>
                                        <Content>
                                            <BSDropdownItem Url="javascript:void(0);">Sub Action</BSDropdownItem>
                                            <BSDropdownItem Url="javascript:void(0);">Another sub action</BSDropdownItem>
                                            <BSDropdownItem Url="javascript:void(0);">Some other sub action</BSDropdownItem>
                                        </Content>
                                    </BSDropdown>
                                </BSDropdownItem>
                            </Content>
                        </BSDropdown>
                    </BSNavItem>
                    <BSNavItem>Link</BSNavItem>
                    <BSNavItem IsDisabled="true">Disabled</BSNavItem>
                </BSNav>
            </Content>
        </BSCollapse>
    </BSContainer>
</BSNavbar>