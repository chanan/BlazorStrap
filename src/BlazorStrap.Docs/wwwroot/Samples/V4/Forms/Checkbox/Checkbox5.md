<div Class="@BS.Form_Check">
    <BSInputRadio CheckedValue="@("on")" @bind-Value="Value" IsDisabled="true"/>
    <BSLabel IsCheckLabel="true">Disabled radio</BSLabel>
</div>
<div Class="@BS.Form_Check">
    <BSInputRadio CheckedValue="@("off")" @bind-Value="Value" IsDisabled="true"/>
    <BSLabel IsCheckLabel="true">Disabled checked radio</BSLabel>
</div>
@code {
    private string Value { get; set; } = "off";
}