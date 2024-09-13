<BSCard CardType="CardType.Card">
    <BSTabWrapper>
        <BSCard CardType="CardType.Header">
            <BSNav IsTabs="true">
                <BSNavItem>
                    <TabLabel>Active</TabLabel>
                    <TabContent>Active Tab content</TabContent>
                </BSNavItem>
                <BSNavItem>
                    <TabLabel>Link</TabLabel>
                    <TabContent>Link 1 Tab content</TabContent>
                </BSNavItem>
                <BSNavItem>
                    <TabLabel>Link</TabLabel>
                    <TabContent>Link 2 Tab content</TabContent>
                </BSNavItem>
                <BSNavItem IsDisabled="true">
                    <TabLabel>Disabled</TabLabel>
                    <TabContent>Active Tab content</TabContent>
                </BSNavItem>
            </BSNav>
        </BSCard>
        <BSCard CardType="CardType.Body" PaddingTopAndBottom="Padding.ExtraLarge">
            <BSTabRender/>
        </BSCard>
    </BSTabWrapper>
</BSCard>