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

<BSButton Color="BSColor.Primary" OnClick="@(() => NewEmployee(new Employee()))">New Employee</BSButton>

@code {
    private BSModal? ModelRef;
    private Employee model = new Employee();
    private InputText? _inputRef;
    private async Task NewEmployee(Employee e)
    {
        model = e;
        for (var i = 0; i < 100; i++)
        {
            await ModelRef.ShowAsync();
            Console.WriteLine("Show Async");
            await ModelRef.HideAsync();
            Console.WriteLine("Hide Async");
        }
        
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