<BSTree IsExpanded="true" ActiveItemAdded="ActiveItemAdded" ActiveItemRemoved="ActiveItemRemoved" IsMultiSelect="true" IsDoubleClickToOpen="true">
    <BSTreeNode>
        @foreach (var item in Data)
        {
            <BSTreeItem TextLabel="@item.Label" Id="@item.id" IsAlwaysActive="@item.IsAlwaysActive" IsDefaultActive="@item.IsDefaultActive">
                <Action>@item.ActionLinks</Action>
                <ChildContent>
                    @if (item.Children != null)
                    {
                        <BSTreeNode>
                        @foreach (var item2 in item.Children)
                        {
                            <BSTreeItem TextLabel="@item2.Label" Id="@item2.id">
                                <Action>@item2.ActionLinks</Action>
                                <ChildContent>
                                    @if (item2.Children != null)
                                    {
                                        <BSTreeNode>
                                        @foreach (var item3 in item2.Children)
                                        {
                                            <BSTreeItem TextLabel="@item3.Label" Id="@item3.id">
                                                <Action>@item3.ActionLinks</Action>
                                            </BSTreeItem>
                                        }
                                        </BSTreeNode>
                                    }
                                </ChildContent>
                            </BSTreeItem>
                        }
                        </BSTreeNode>
                    }
                </ChildContent>
            </BSTreeItem>
        }
    </BSTreeNode>
</BSTree>

@foreach (var id in Selected)
{
    <span>@id ,</span>
}
@code {
    private List<string> Selected = new List<string>();
    private void ActiveItemAdded(BSTreeItem child)
    {
        if (Selected.Contains(child.Id)) return;
        Selected.Add(child.Id);
        StateHasChanged();
    }
    private void ActiveItemRemoved(BSTreeItem child)
    {
        if (Selected.Contains(child.Id))
            Selected.Remove(child.Id);
        StateHasChanged();
    }

    private List<TreeContentExample> Data = new List<TreeContentExample>()
    {
        new TreeContentExample() { id = "1-a", Label="Level 1-a", Children = new List<TreeContentExample>()
        {
              new TreeContentExample() {id = "2-a", Label = "Level 2-a"},
            new TreeContentExample() {id = "2-b", Label = "Level 2-b"},
            new TreeContentExample() {id = "2-c", Label = "Level 2-c", Children = new List<TreeContentExample>()
            {
                new TreeContentExample(){id = "3-a", Label = "Level 3-a"},
                new TreeContentExample(){id = "3-b", Label = "Level 3-b"},
                new TreeContentExample(){id = "3-c", Label = "Level 3-c", ActionLinks = "With Action Links"}
            }
            }
        }} ,
        new TreeContentExample() {id = "1-b", Label="Level 1-b", IsAlwaysActive = true},
        new TreeContentExample() {id = "1-c", Label="Level 1-c", IsDefaultActive = true}
    };

    public class TreeContentExample
    {
        public string id { get; set; }
        public string Label { get; set; }
        public string? ActionLinks { get; set; }
        public bool IsAlwaysActive {get; set; }
        public bool IsDefaultActive { get; set; }
        public List<TreeContentExample>? Children { get; set; }
    }
    }