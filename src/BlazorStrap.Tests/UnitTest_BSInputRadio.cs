using System;
using System.Threading.Tasks;
using Bunit;
using Microsoft.AspNetCore.Components.Forms;
using Xunit;
using Xunit.Abstractions;

namespace BlazorStrap.Tests;

public class BSInputRadioTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public BSInputRadioTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    // Checkbox and Radios are the same components value test not needed. 
    [Fact]
    public async Task StringValueTest()
    {
        var expected = "hello world";
        var expected2 = "hello space";
        var value = "";
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInputRadio<string>>(parameters => 
            parameters.Add(p => p.Value, value)
                      .Add(p => p.CheckedValue, "hello world")
                      .Add(p => p.ValueChanged, e =>
                      {
                          value = e;
                      } )
            );
        cut.Find("input").Click();
        Assert.Equal(expected, value);
        var cut2 = ctx.RenderComponent<BSInputRadio<string>>(parameters => 
            parameters.Add(p => p.Value, value)
                .Add(p => p.CheckedValue, "hello space")
                .Add(p => p.ValueChanged, e =>
                {
                    value = e;
                } )
        );
        cut2.Find("input").Click();
        Assert.Equal(expected2, value);
        
    }
}