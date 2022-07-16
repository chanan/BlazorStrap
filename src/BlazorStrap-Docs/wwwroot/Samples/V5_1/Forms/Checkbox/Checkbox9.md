<BSInputRadio CheckedValue="@("Radio 1")" @bind-Value="Value" IsToggle="true" Color="BSColor.Secondary">
    Radio 1
</BSInputRadio>
<BSInputRadio CheckedValue="@("Radio 2")" @bind-Value="Value" IsToggle="true" Color="BSColor.Secondary">
    Radio 2
</BSInputRadio>
<BSInputRadio CheckedValue="@("Radio 3")" @bind-Value="Value" IsToggle="true" Color="BSColor.Secondary">
    Radio 3
</BSInputRadio>
<BSInputRadio CheckedValue="@("Radio 4")" @bind-Value="Value" IsToggle="true" Color="BSColor.Secondary" IsDisabled="true">
    Disabled
</BSInputRadio>
Value = @Value
@code {
    private string Value { get; set; } = "Radio 1";
}