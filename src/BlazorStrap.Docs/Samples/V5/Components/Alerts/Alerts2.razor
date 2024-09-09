@foreach (var alert in Alerts)
{
    <BSAlert Color="alert.Color" IsDismissible="true" Dismissed="@(() => Dismiss(alert))" @key="alert">
        This is a dismissable alert!
    </BSAlert>
}
<BSButton Color="BSColor.Primary" @onclick="New">Show live alert</BSButton>


@code {
    List<BSAlert> Alerts { get; set; } = new List<BSAlert>();
    BSColor _lastColor = BSColor.Default;
    void New()
    {
        var color = Random();
        _lastColor = color;
        Alerts.Add(new BSAlert() { Color = color });
    }
    void Dismiss(BSAlert alert)
    {
        Alerts.Remove(alert);
        StateHasChanged();
    }
    BSColor Random()
    {
        var values = Enum.GetValues(typeof(BSColor));
        var random = new Random();
        var color = (BSColor)(values.GetValue(random.Next(values.Length)) ?? 0);
        while (color == _lastColor || color == BSColor.Default)
        {
            color = (BSColor)(values.GetValue(random.Next(values.Length)) ?? 0);
        }
        return color;
    }
}