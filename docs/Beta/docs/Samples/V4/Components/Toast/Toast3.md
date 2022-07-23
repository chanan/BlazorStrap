@inject IBlazorStrap _blazorStrap
<!--\\-->
<div aria-live="polite" aria-atomic="true" class="position-relative bd-example-toasts" style="overflow-y: scroll ">
<!--//-->
    <BSToaster WrapperStyle="width:300px;"/>
<!--\\-->
</div>
<!--//-->
<div class="input-group">
    <span class="input-group-text">Toast Placement</span>
    <BSInput InputType="InputType.Select" @bind-Value="ToastPlacement">
        @foreach (var item in  Enum.GetNames(typeof(Toast)).ToList())
        {
            <option value="@item">@item</option>
        }
    </BSInput>
    <span class="input-group-text">Background Color</span>
    <BSInput InputType="InputType.Select" @bind-Value="Color">
        @foreach (var item in  Enum.GetNames(typeof(BSColor)).ToList())
        {
            <option value="@item">@item</option>
        }
    </BSInput>
</div>
<div class="input-group">
    <span class="input-group-text">Close Time in milliseconds</span>
    <BSInput InputType="InputType.Text" @bind-Value="Time"/>
    <BSButton @onclick="Show" Color="BSColor.Primary">Show</BSButton>
</div>

@code
{
    private Toast ToastPlacement { get; set; }
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
        });
        ++i;
    }
}