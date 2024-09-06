@inject IBlazorStrap _blazorStrap
<!--\\-->
<div aria-live="polite" aria-atomic="true" class="position-relative bd-example-toasts" style="overflow-y: scroll ">
    <!--//-->
    <BSToaster Position="ToastPosition" ZIndex="ZIndex" />
    <!--\\-->
</div>
<!--//-->
<div class="input-group">
    <span class="input-group-text">Toaster Position</span>
    <BSInput InputType="InputType.Select" @bind-Value="ToastPosition">
        @foreach (var item in Enum.GetNames(typeof(Position)).ToList())
        {
            <option value="@item">@item</option>
        }
    </BSInput>
    <span class="input-group-text">Toaster Z-Index</span>
    <BSInput InputType="InputType.Text" @bind-Value="ZIndex" />
</div>
<div class="input-group">
    <span class="input-group-text">Toast Placement</span>
    <BSInput InputType="InputType.Select" @bind-Value="ToastPlacement">
        @foreach (var item in Enum.GetNames(typeof(Toast)).ToList())
        {
            <option value="@item">@item</option>
        }
    </BSInput>
    <span class="input-group-text">Background Color</span>
    <BSInput InputType="InputType.Select" @bind-Value="Color">
        @foreach (var item in Enum.GetNames(typeof(BSColor)).ToList())
        {
            <option value="@item">@item</option>
        }
    </BSInput>
</div>
<div class="input-group">
    <span class="input-group-text">Has Icon</span>
    <BSInput InputType="InputType.Select" @bind-Value="HasIcon">

        <option value="@("False")">False</option>
        <option value="@("True")">True</option>

    </BSInput>
    <span class="input-group-text">Close Time in milliseconds</span>
    <BSInput InputType="InputType.Text" @bind-Value="Time" />
    <BSButton @onclick="Show" Color="BSColor.Primary">Show</BSButton>
</div>
@HasIcon
@code
{
    private bool HasIcon { get; set; }
    private Toast ToastPlacement { get; set; }
    private int ZIndex { get; set; } = 1025;
    private Position ToastPosition { get; set; } = Position.Absolute;
    private BSColor Color { get; set; }
    private int Time { get; set; } = 0;
    private int i = 0; 

    private void Show()
    {
        _blazorStrap.Toaster.Add("Live Example " + @i, "Live Example Text", o =>
        {
            o.Color = Color;
            o.CloseAfter = Time;
            o.Toast = ToastPlacement;
            o.HasIcon = HasIcon;
        });
        ++i;
    }
}