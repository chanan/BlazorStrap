using System;
using System.Threading.Tasks;
using Bunit;
using Microsoft.AspNetCore.Components.Forms;
using Xunit;
using Xunit.Abstractions;

namespace BlazorStrap.Tests;

public class BSInputCheckboxTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public BSInputCheckboxTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task BoolValueTest()
    {
        var expected = true;
        var value = false;
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInputCheckbox<bool>>(parameters => 
            parameters.Add(p => p.Value, value)
                      .Add(p => p.CheckedValue, true)
                      .Add(p => p.UnCheckedValue, false)
                      .Add(p => p.ValueChanged, e =>
                      {
                          value = e;
                      } )
            );
        cut.Find("input").Click();
        Assert.Equal(expected, value);
        cut.Find("input").Click();
        Assert.False(value);
    }
    [Fact]
    public async Task StringValueTest()
    {
        var expected = "hello world";
        var value = "";
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInputCheckbox<string>>(parameters => 
            parameters.Add(p => p.Value, value)
                      .Add(p => p.CheckedValue, "hello world")
                      .Add(p => p.UnCheckedValue, "")
                      .Add(p => p.ValueChanged, e =>
                      {
                          value = e;
                      } )
            );
        cut.Find("input").Click();
        Assert.Equal(expected, value);
        cut.Find("input").Click();
        Assert.Equal("", value);
    }
    [Fact]
    public async Task IntValueTest()
    {
        var expected = 100;
        var value = 0;
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInputCheckbox<int>>(parameters => 
            parameters.Add(p => p.Value, value)
                .Add(p => p.CheckedValue, 100)
                .Add(p => p.UnCheckedValue, 0)
                .Add(p => p.ValueChanged, e =>
                {
                    value = e;
                } )
        );
        cut.Find("input").Click();
        Assert.Equal(expected, value);
        cut.Find("input").Click();
        Assert.Equal(0, value);
    }
    [Fact]
    public async Task LongValueTest()
    {
        var expected = 9223372036854775807;
        long value = 0;
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInputCheckbox<long>>(parameters => 
            parameters.Add(p => p.Value, value)
                .Add(p => p.CheckedValue, 9223372036854775807)
                .Add(p => p.UnCheckedValue, 0)
                .Add(p => p.ValueChanged, e =>
                {
                    value = e;
                } )
        );
        cut.Find("input").Click();
        Assert.Equal(expected, value);
        cut.Find("input").Click();
        Assert.Equal(0, value);
    }
    [Fact]
    public async Task FloatValueTest()
    {
        var expected = 555.555f;
        var value = 0f;
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInputCheckbox<float>>(parameters => 
            parameters.Add(p => p.Value, value)
                .Add(p => p.CheckedValue, 555.555f)
                .Add(p => p.UnCheckedValue, 0f)
                .Add(p => p.ValueChanged, e =>
                {
                    value = e;
                } )
        );
        cut.Find("input").Click();
        Assert.Equal(expected, value);
        cut.Find("input").Click();
        Assert.Equal(0f, value);
    }
    [Fact]
    public async Task DoubleValueTest()
    {
        var expected = 555.555;
        var value = 0.0;
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInputCheckbox<double>>(parameters => 
            parameters.Add(p => p.Value, value)
                .Add(p => p.CheckedValue, 555.555)
                .Add(p => p.UnCheckedValue, 0.0)
                .Add(p => p.ValueChanged, e =>
                {
                    value = e;
                } )
        );
        cut.Find("input").Click();
        Assert.Equal(expected, value);
        cut.Find("input").Click();
        Assert.Equal(0.0, value);
    }
    [Fact]
    public async Task DecimalValueTest()
    {
        var expected = 555.555m;
        var value = 0.0m;
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInputCheckbox<decimal>>(parameters => 
            parameters.Add(p => p.Value, value)
                .Add(p => p.CheckedValue, 555.555m)
                .Add(p => p.UnCheckedValue, 0.0m)
                .Add(p => p.ValueChanged, e =>
                {
                    value = e;
                } )
        );
        cut.Find("input").Click();
        Assert.Equal(expected, value);
        cut.Find("input").Click();
        Assert.Equal(0.0m, value);
    }
    [Fact]
    public async Task DateTimeValueTest()
    {
        var expected = DateTime.Parse("2000-10-20T12:30:00");
        var value = DateTime.Parse("2020-10-20T12:30:00");
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInputCheckbox<DateTime>>(parameters => 
            parameters.Add(p => p.Value, value)
                .Add(p => p.CheckedValue, DateTime.Parse("2000-10-20T12:30:00"))
                .Add(p => p.UnCheckedValue, DateTime.Parse("2020-10-20T12:30:00"))
                .Add(p => p.ValueChanged, e =>
                {
                    value = e;
                } )
        );
        cut.Find("input").Click();
        Assert.Equal(expected, value);
        cut.Find("input").Click();
        Assert.Equal(DateTime.Parse("2020-10-20T12:30:00"), value);
    }
    [Fact]
    public async Task DateOnlyValueTest()
    {
        var expected = DateOnly.Parse("2000-10-20");
        var value = DateOnly.Parse("2020-10-20");
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInputCheckbox<DateOnly>>(parameters => 
            parameters.Add(p => p.Value, value)
                .Add(p => p.CheckedValue, DateOnly.Parse("2000-10-20"))
                .Add(p => p.UnCheckedValue, DateOnly.Parse("2020-10-20"))
                .Add(p => p.ValueChanged, e =>
                {
                    value = e;
                } )
        );
        cut.Find("input").Click();
        Assert.Equal(expected, value);
        cut.Find("input").Click();
        Assert.Equal(DateOnly.Parse("2020-10-20"), value);
    }
    [Fact]
    public async Task TimeOnlyValueTest()
    {
        var expected = TimeOnly.Parse("12:30:00");
        var value = TimeOnly.Parse("12:30:00");
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<BSInputCheckbox<TimeOnly>>(parameters => 
            parameters.Add(p => p.Value, value)
                .Add(p => p.CheckedValue, TimeOnly.Parse("12:30:00"))
                .Add(p => p.UnCheckedValue, TimeOnly.Parse("12:30:00"))
                .Add(p => p.ValueChanged, e =>
                {
                    value = e;
                } )
        );
        cut.Find("input").Click();
        Assert.Equal(expected, value);
        cut.Find("input").Click();
        Assert.Equal(TimeOnly.Parse("12:30:00"), value);
    }
}