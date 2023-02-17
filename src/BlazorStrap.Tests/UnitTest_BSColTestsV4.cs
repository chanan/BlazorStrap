using System.Threading.Tasks;
using BlazorStrap.V4;
using Bunit;
using Xunit;
using Xunit.Abstractions;

namespace BlazorStrap.Tests;

public class BSColTestsV4 
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    public BSColTestsV4(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task RenderCol12Test()
    {
        using var ctx = new TestContext();
        ctx.Services.AddBlazorStrap();

        ctx.RenderTree.Add<BSRow>();
        var cut = ctx.RenderComponent<BSCol>(parameters => parameters
                .Add(p => p.Column, "12")            
            );

        var renderedMarkup = cut.Markup;
        Assert.Equal(@"<div class=""col-12""></div>", renderedMarkup);        
    }

    [Fact]
    public async Task RenderColAutoTest()
    {
        using var ctx = new TestContext();
        ctx.Services.AddBlazorStrap();

        ctx.RenderTree.Add<BSRow>();
        var cut = ctx.RenderComponent<BSCol>(parameters => parameters
                .Add(p => p.Auto, true)            
            );
        
        var renderedMarkup = cut.Markup;
        Assert.Equal(@"<div class=""col-auto""></div>", renderedMarkup); 
    }
}
