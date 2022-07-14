@implements IDisposable

<BSProgress>
    <BSProgressBar Color="BSColor.Primary" Value="@Value">@(Math.Round(Value))%</BSProgressBar>
</BSProgress>
<BSProgress>
    <BSProgressBar Color="BSColor.Danger" Value="@Value1"></BSProgressBar>
    <BSProgressBar Color="BSColor.Warning" Value="@Value2"></BSProgressBar>
    <BSProgressBar Color="BSColor.Success" Value="@Value3"></BSProgressBar>
</BSProgress>
<BSButton Color="BSColor.Primary" OnClick="Toggle" MarginTop="Margins.Medium">Start/Stop</BSButton>

@code {
    private double Value = 35;
    private double Value1 = 35;
    private double Value2 = 0;
    private double Value3 = 0;
    private bool running = false;
    private Timer timer = default!;

    protected override void OnInitialized()
    {
        timer = new Timer(Tick);
        base.OnInitialized();
    }

    public void Dispose()
    {
        timer.Dispose();
    }

    private void Toggle()
    {
        running = !running;
        if (running)
        {
            timer.Change(0, 500);
        }
        else
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }

    private void Tick(object? _)
    {
        Value = (Value + 5) % 105;
        Value1 = Math.Clamp(Value, 0, 50);
        Value2 = Math.Clamp(Value - 50, 0, 30);
        Value3 = Math.Clamp(Value - 80, 0, 20);
        InvokeAsync(StateHasChanged);
    }
}