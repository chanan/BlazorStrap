<BSInputSwitch CheckedValue="@("on")" UnCheckedValue="@("off")" @bind-Value="Value">
    Default switch checkbox input @Value
</BSInputSwitch>
<BSInputSwitch CheckedValue="@("off")" UnCheckedValue="@("on")" @bind-Value="ValueTwo">
    Checked switch checkbox input @ValueTwo
</BSInputSwitch>
<BSInputSwitch CheckedValue="1" Value="0" IsDisabled="true">
    Disabled switch checkbox input 
</BSInputSwitch>
<BSInputSwitch CheckedValue="0" Value="0" IsDisabled="true">
    Disabled checked switch checkbox input
</BSInputSwitch>

@code {
    private string Value { get; set; } = "off";
    private string ValueTwo { get; set; } = "off";
}