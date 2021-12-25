using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Utilities
{
    public static class EventUtil
    {
        public static Action AsNonRenderingEventHandler(Action callback)
            => new SyncReceiver(callback).Invoke;

        public static Action<TValue> AsNonRenderingEventHandler<TValue>(
                Action<TValue> callback)
            => new SyncReceiver<TValue>(callback).Invoke;

        public static Func<Task> AsNonRenderingEventHandler(Func<Task> callback)
            => new AsyncReceiver(callback).Invoke;

        public static Func<TValue, Task> AsNonRenderingEventHandler<TValue>(
                Func<TValue, Task> callback)
            => new AsyncReceiver<TValue>(callback).Invoke;

        private record AsyncReceiver(Func<Task> callback)
            : ReceiverBase
        {
            public Task Invoke() => callback();
        }

        private record AsyncReceiver<T>(Func<T, Task> callback)
            : ReceiverBase
        {
            public Task Invoke(T arg) => callback(arg);
        }

        private record ReceiverBase : IHandleEvent
        {
            public Task HandleEventAsync(EventCallbackWorkItem item, object arg) =>
                item.InvokeAsync(arg);
        }

        private record SyncReceiver(Action callback)
            : ReceiverBase
        {
            public void Invoke() => callback();
        }

        private record SyncReceiver<T>(Action<T> callback)
            : ReceiverBase
        {
            public void Invoke(T arg) => callback(arg);
        }
    }
}
