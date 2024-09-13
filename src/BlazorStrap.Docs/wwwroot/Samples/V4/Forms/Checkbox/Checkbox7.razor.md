<div Class="@BS.Form_Check_Inline">
    <BSInputCheckbox CheckedValue="@("on1")" @bind-Value="Value"/>
    <BSLabel IsCheckLabel="true">1</BSLabel>
</div>
<div Class="@BS.Form_Check_Inline">
    <BSInputCheckbox CheckedValue="@("on2")" @bind-Value="ValueTwo"/>
    <BSLabel IsCheckLabel="true">2</BSLabel>
</div>
<div Class="@BS.Form_Check_Inline">
    <BSInputCheckbox CheckedValue="@("on3")" @bind-Value="Value" IsDisabled="true"/>
    <BSLabel IsCheckLabel="true">3 (disabled)</BSLabel>
</div>
<BSColBreak/>
<div Class="@BS.Form_Check_Inline">
    <BSInputRadio CheckedValue="@("on4")" @bind-Value="Value"/>
    <BSLabel IsCheckLabel="true">1</BSLabel>
</div>
<div Class="@BS.Form_Check_Inline">
    <BSInputRadio CheckedValue="@("on5")" @bind-Value="Value"/>
    <BSLabel IsCheckLabel="true">2</BSLabel>
</div>
<div Class="@BS.Form_Check_Inline">
    <BSInputRadio CheckedValue="@("on6")" @bind-Value="Value" IsDisabled="true"/>
    <BSLabel IsCheckLabel="true">3 (disabled)</BSLabel>
</div>
@code {
    private string Value { get; set; } = "off";
    private string ValueTwo { get; set; } = "off";
}