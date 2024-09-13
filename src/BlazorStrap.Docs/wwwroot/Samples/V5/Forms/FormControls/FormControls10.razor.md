<div class="input-group mb-4">
    <span class="input-group-text">Text</span>
    <BSInput InputType="InputType.Text" @bind-Value="TextValue" ValidateOnInput="true"/>
    <span class="input-group-text">Password</span>
    <BSInput InputType="InputType.Password" @bind-Value="TextValue"/>
    <span class="input-group-text">Email</span>
    <BSInput InputType="InputType.Email" @bind-Value="TextValue"/>
    <span class="input-group-text">Value = @TextValue</span>
</div>
<div class="input-group mb-4">
    <span class="input-group-text">DateTimeLocal As DateTime</span>
    <BSInput InputType="InputType.DateTimeLocal" @bind-Value="DateValue"/>
    <span class="input-group-text">Value = @DateValue</span>
</div>
<div class="input-group mb-4">
    <span class="input-group-text">DateTimeLocal As DateTimeOffset</span>
    <BSInput InputType="InputType.DateTimeLocal" @bind-Value="DateOffsetValue"/>
    <span class="input-group-text">Value = @DateOffsetValue</span>
</div>
<div class="input-group mb-4">
    <span class="input-group-text">Date As DateTime</span>
    <BSInput InputType="InputType.Date" @bind-Value="DateValue"/>
    <span class="input-group-text">Value = @DateValue</span>
</div>
<div class="input-group mb-4">
    <span class="input-group-text">Date As DateTimeOffset</span>
    <BSInput InputType="InputType.Date" @bind-Value="DateOffsetValue"/>
    <span class="input-group-text">Value = @DateOffsetValue</span>
</div>
<div class="input-group mb-4">
    <span class="input-group-text">Month As DateOnly</span>
    <BSInput InputType="InputType.Date" @bind-Value="DateOnlyValue"/>
    <span class="input-group-text">Value = @DateOnlyValue</span>
</div>
<div class="input-group mb-4">
    <span class="input-group-text">Time As TimeOnly</span>
    <BSInput InputType="InputType.Time" @bind-Value="TimeOnlyValue"/>
    <span class="input-group-text">Value = @TimeOnlyValue</span>
</div>
<div class="input-group mb-4">
    <span class="input-group-text">On Input</span>
    <BSInput InputType="InputType.Text" @bind-Value="OnInputValue" UpdateOnInput="true"/>
    <span class="input-group-text">Value = @OnInputValue</span>
</div>
@code{
    private string? TextValue { get; set; }
    private string? OnInputValue { get; set; }
    private DateTime DateValue { get; set; }
    private DateTimeOffset DateOffsetValue { get; set; }
    private TimeOnly TimeOnlyValue { get; set; }
    private DateOnly DateOnlyValue { get; set; }
}