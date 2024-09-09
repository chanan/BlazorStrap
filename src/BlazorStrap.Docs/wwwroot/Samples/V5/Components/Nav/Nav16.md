    <BSTabWrapper>
            <BSNav IsTabs="true" OnTabChange="(RenderFragment e) => OnTabChangeAsync(e)">
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
    </BSTabWrapper>

    <BSCard CardType="CardType.Body">
        <p>Somewhere else on the page.</p>
        <hr/>
        @_tabContent
    </BSCard>
    @code{
    private RenderFragment? _tabContent;

        private async Task OnTabChangeAsync(RenderFragment content)
        {
            _tabContent = content;
            await InvokeAsync(StateHasChanged);
        }

    }