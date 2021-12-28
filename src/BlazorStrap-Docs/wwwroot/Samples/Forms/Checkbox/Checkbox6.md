<div Class="@BS.Form_Check @BS.Form_Switch">
    <BSInputRadio CheckedValue="@("on")" @bind-Value="Value"/>
    <BSLabel IsCheckLabel="true">Default switch checkbox input</BSLabel>
</div>
<div Class="@BS.Form_Check @BS.Form_Switch">
    <BSInputRadio CheckedValue="@("off")" @bind-Value="Value"/>
    <BSLabel IsCheckLabel="true">Checked switch checkbox input</BSLabel>
</div>
<div Class="@BS.Form_Check @BS.Form_Switch">
    <BSInputRadio CheckedValue="@("on")" @bind-Value="Value" IsDisabled="true"/>
    <BSLabel IsCheckLabel="true">Disabled switch checkbox input</BSLabel>
</div>
<div Class="@BS.Form_Check @BS.Form_Switch">
    <BSInputRadio CheckedValue="@("off")" @bind-Value="Value" IsDisabled="true"/>
    <BSLabel IsCheckLabel="true">Disabled checked switch checkbox input</BSLabel>
</div>
@code {
    private string Value { get; set; } = "off";
}