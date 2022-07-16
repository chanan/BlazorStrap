<BSInputCheckbox CheckedValue="@("on")" @bind-Value="Value" UnCheckedValue="@("off")" IsToggle="true" Color="BSColor.Secondary">
    @Value
</BSInputCheckbox>
<BSInputCheckbox CheckedValue="@("on")" @bind-Value="Value" UnCheckedValue="@("off")" IsToggle="true" Color="BSColor.Secondary" IsDisabled="true">
    Disabled
</BSInputCheckbox>
@code {
    private string Value { get; set; } = "off";
}