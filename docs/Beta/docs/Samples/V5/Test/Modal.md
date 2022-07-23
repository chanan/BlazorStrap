<EditForm Model="model" OnValidSubmit="@Update">
    <BSModal @ref="ModelRef">
        <Header>Head</Header>
        <Content>
            @model.ToString()
        </Content>
        <Footer Context="ctx">
            <BSButton @onclick="@(() => ctx.HideAsync())">Cancel</BSButton>
            <BSButton IsSubmit="true" Color="BSColor.Primary">Save</BSButton>
        </Footer>
    </BSModal>
</EditForm>

<BSButton Color="BSColor.Primary" OnClick="@(() => NewEmployee(new Employee()))">New Employee</BSButton>

@code {
    private BSModal ModelRef;
    private Employee model = new Employee();
    private InputText _inputRef;
    private async Task NewEmployee(Employee e)
    {
        model = e;
        await ModelRef.ShowAsync();

    }
    private async Task Update()
    {
    }

    public class Employee
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public BSModal ModelRef { get; set; }
    }
}