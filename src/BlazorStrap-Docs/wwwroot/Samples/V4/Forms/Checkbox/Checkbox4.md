<div Class="@BS.Form_Check">
    <BSInputRadio CheckedValue="@("on")" @bind-Value="Value"/>
    <BSLabel IsCheckLabel="true">Default radio</BSLabel>
</div>
<div Class="@BS.Form_Check">
    <BSInputRadio CheckedValue="@("off")" @bind-Value="Value"/>
    <BSLabel IsCheckLabel="true">Default checked radio</BSLabel>
</div>
@code {
    private string Value { get; set; } = "off";
}
