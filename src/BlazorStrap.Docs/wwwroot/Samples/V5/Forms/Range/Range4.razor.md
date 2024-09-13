<BSLabel>Example range @Value</BSLabel>
<BSInput InputType="InputType.Range" @bind-Value="Value" min="0" max="5" step="0.5"/>
@code {
    private double Value { get; set; }
}