﻿<p>
    <BSButton Target="collapse3a">Toggle first element</BSButton>
    <BSButton IsLink="true" Color="BSColor.Primary" Target="collapse3b">Toggle second element</BSButton>
    <BSButton Color="BSColor.Primary" Target="collapse3a,collapse3b">Toggle both</BSButton>
</p>
<BSRow>
    <BSCol>
        <BSCollapse DataId="collapse3a">
            <BSCardBody Class="card">
                Some placeholder content for the first collapse component of this multi-collapse example. This panel is hidden by default but revealed when the user activates the relevant trigger.
            </BSCardBody>
        </BSCollapse>
    </BSCol>
    <BSCol>
        <BSCollapse DataId="collapse3b">
            <BSCardBody Class="card">
                Some placeholder content for the second collapse component of this multi-collapse example. This panel is hidden by default but revealed when the user activates the relevant trigger.
            </BSCardBody>
        </BSCollapse>
    </BSCol>
</BSRow>