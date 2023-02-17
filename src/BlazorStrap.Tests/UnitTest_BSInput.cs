using System;
using System.Threading.Tasks;
using BlazorStrap.V5;
using Bunit;
using Microsoft.AspNetCore.Components.Forms;
using Xunit;
using Xunit.Abstractions;

namespace BlazorStrap.Tests;

[UseCulture("en-US")]
public class BSInputTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public BSInputTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task StringValueTest()
    {
        var expected = "hello world";
        var value = "";
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInput<string>>(parameters => 
            parameters.Add(p => p.Value, value)
                      .Add(p => p.ValueChanged, e =>
                      {
                          value = e;
                      } )
            );
        cut.Find("input").Change(expected);
        Assert.Equal(expected, value);
        
    }
    
    [Fact]
    public async Task IntValueTest()
    {
        var expected = 100;
        var value = 0;
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInput<int>>(parameters => 
            parameters.Add(p => p.Value, value)
                .Add(p => p.ValueChanged, e =>
                {
                    value = e;
                } )
        );
        cut.Find("input").Change(expected);
        Assert.Equal(expected, value);
    }    
    [Fact]
    public async Task NullAbleIntValueNullTest()
    {
        var expected = 100;
        int? value = 0;
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInput<int?>>(parameters => 
            parameters.Add(p => p.Value, value)
                .Add(p => p.ValueChanged, e =>
                {
                    value = e;
                } )
        );
        cut.Find("input").Change(expected);
        Assert.Equal(expected, value);
    }   
    [Fact]
    public async Task NullAbleIntValueTest()
    {
        int? expected = null;
        int? value = 0;
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInput<int?>>(parameters => 
            parameters.Add(p => p.Value, value)
                .Add(p => p.ValueChanged, e =>
                {
                    value = e;
                } )
        );
        cut.Find("input").Change(expected);
        Assert.Equal(expected, value);
    }     
    [Fact]
    public async Task LongValueTest()
    {
        var expected = 9223372036854775807;
        long value = 0;
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInput<long>>(parameters => 
            parameters.Add(p => p.Value, value)
                .Add(p => p.ValueChanged, e =>
                {
                    value = e;
                } )
        );
        cut.Find("input").Change(expected);
        Assert.Equal(expected, value);
    }   
 
    [Fact]
    public async Task DateTimeOffsetValueTest()
    {
        var expected = "2000-10-20";
        DateTimeOffset value = new DateTimeOffset();
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInput<DateTimeOffset>>(parameters => 
            parameters.Add(p => p.Value, value)
                .Add(p => p.ValueChanged, e =>
                {
                    value = e;
                } )
        );
        cut.Find("input").Change(expected);
        Assert.Equal(DateTimeOffset.Parse(expected), value);
    }   
    
    [Fact]
    public async Task DecimalValueTest()
    {
        decimal expected = 999.99m;
        decimal value = 0;
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInput<decimal>>(parameters => 
            parameters.Add(p => p.Value, value)
                .Add(p => p.ValueChanged, e =>
                {
                    value = e;
                } )
        );
        cut.Find("input").Change(expected);
        Assert.Equal(expected, value);
    }   
    
    [Fact]
    public async Task DoubleValueTest()
    {
        var expected = 999.99;
        double value = 0;
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInput<double>>(parameters => 
            parameters.Add(p => p.Value, value)
                .Add(p => p.ValueChanged, e =>
                {
                    value = e;
                } )
        );
        cut.Find("input").Change(expected);
        Assert.Equal(expected, value);
    }    
    
    [Fact]
    public async Task FloatValueTest()
    {
        float expected = 999.99f;
        float value = 0;
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInput<float>>(parameters => 
            parameters.Add(p => p.Value, value)
                .Add(p => p.ValueChanged, e =>
                {
                    value = e;
                } )
        );
        cut.Find("input").Change(expected);
        Assert.Equal(expected, value);
    }    
    
    [Fact]
    public async Task DateTimeValueTest()
    {
        var expected = "2000-10-20T12:30:00";
        DateTime? value = null;
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInput<DateTime?>>(parameters => 
            parameters.Add(p => p.Value, value)
                      .Add(p => p.ValueChanged, e =>
                      {
                        value = e;
                      })
        );
        cut.Find("input").Change(expected);
        Assert.Equal(DateTime.Parse(expected), value);
    }
    [Fact]
    public async Task DateOnlyValueTest()
    {
        var expected = "2000-10-12";
        DateOnly? value = null;
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInput<DateOnly?>>(parameters => 
            parameters.Add(p => p.Value, value)
                .Add(p => p.ValueChanged, e =>
                {
                    value = e;
                })
        );
        cut.Find("input").Change(expected);
        Assert.Equal(DateOnly.Parse(expected), value);
    }
    [Fact]
    public async Task TimeOnlyValueTest()
    {
        var expected = "12:30";
        TimeOnly? value = null;
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInput<TimeOnly?>>(parameters => 
            parameters.Add(p => p.Value, value)
                .Add(p => p.ValueChanged, e =>
                {
                    value = e;
                })
        );
        cut.Find("input").Change(expected);
        Assert.Equal(TimeOnly.Parse(expected), value);
    }
}