using System.Threading.Tasks;
using BlazorStrap.V5;
using Bunit;
using Xunit;

namespace BlazorStrap.Tests;

[UseCulture("en-US")]
public class BSInputDebounceTests
{
    private const int Interval = 50;

    private static IRenderedComponent<BSInput<string>> Render(TestContext ctx, System.Action<string> changed) =>
        ctx.RenderComponent<BSInput<string>>(parameters => parameters
            .Add(p => p.Value, "")
            .Add(p => p.UpdateOnInput, true)
            .Add(p => p.DebounceInterval, Interval)
            .Add(p => p.ValueChanged, changed));

    [Fact]
    public async Task UpdateOnInput_applies_the_value_after_the_interval()
    {
        var value = "";
        using var ctx = new TestContext();
        var cut = Render(ctx, e => value = e);

        cut.Find("input").Input("hello");
        Assert.Equal("", value);

        await Task.Delay(Interval * 4);
        Assert.Equal("hello", value);
    }

    [Fact]
    public async Task Change_event_cancels_a_pending_input_update()
    {
        // Typing then pressing Enter: onchange carries the final value and the form clears the model on submit.
        // The debounced oninput update must not arrive afterwards and put the old text back.
        var value = "";
        using var ctx = new TestContext();
        var cut = Render(ctx, e => value = e);

        cut.Find("input").Input("hel");
        cut.Find("input").Change("hello");
        Assert.Equal("hello", value);
        value = ""; // submit handler clears the message

        await Task.Delay(Interval * 4);
        Assert.Equal("", value);
    }

    [Fact]
    public async Task Inputs_do_not_share_a_debounce_timer()
    {
        var first = "";
        var second = "";
        using var ctx = new TestContext();
        var a = Render(ctx, e => first = e);
        var b = Render(ctx, e => second = e);

        a.Find("input").Input("one");
        b.Find("input").Input("two");

        await Task.Delay(Interval * 4);
        Assert.Equal("one", first);
        Assert.Equal("two", second);
    }
}
