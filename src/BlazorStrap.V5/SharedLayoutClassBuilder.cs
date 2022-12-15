namespace BlazorStrap.V5;

public class SharedLayoutClassBuilder : ILayoutClassBuilder
{
    public string? Build(IBlazorStrapBase blazorStrapBase) => BlazorStrap.V5.LayoutClassBuilder.Build(blazorStrapBase);
}