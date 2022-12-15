namespace BlazorStrap.V4;

public class SharedLayoutClassBuilder : ILayoutClassBuilder
{
    public string? Build(IBlazorStrapBase blazorStrapBase) => BlazorStrap.V4.LayoutClassBuilder.Build(blazorStrapBase);
}