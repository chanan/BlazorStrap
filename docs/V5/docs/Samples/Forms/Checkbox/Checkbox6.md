<div Class="@BS.Form_Check @BS.Form_Switch">
    <BSInputCheckbox CheckedValue="@("on")" @bind-Value="Value"/>
    <BSLabel IsCheckLabel="true">Default switch checkbox input</BSLabel>
</div>
<div Class="@BS.Form_Check @BS.Form_Switch">
    <BSInputCheckbox CheckedValue="@("off")" @bind-Value="ValueTwo"/>
    <BSLabel IsCheckLabel="true">Checked switch checkbox input</BSLabel>
</div>
<div Class="@BS.Form_Check @BS.Form_Switch">
    <BSInputCheckbox CheckedValue="1" Value="0" IsDisabled="true"/>
    <BSLabel IsCheckLabel="true">Disabled switch checkbox input</BSLabel>
</div>
<div Class="@BS.Form_Check @BS.Form_Switch">
    <BSInputCheckbox CheckedValue="0" Value="0" IsDisabled="true"/>
    <BSLabel IsCheckLabel="true">Disabled checked switch checkbox input</BSLabel>
</div>
@code {
    private string Value { get; set; } = "off";
    private string ValueTwo { get; set; } = "off";
}