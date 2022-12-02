namespace BlazorStrap.Shared.Components;

public class EventQue
{
    public TaskCompletionSource<bool> TaskSource { get; set; }
    public Func<Task> Func { get; set; }
}