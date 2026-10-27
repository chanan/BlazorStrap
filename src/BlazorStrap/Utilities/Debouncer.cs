namespace BlazorStrap.Utilities
{
    /// <summary>
    /// Runs an action once calls have stopped for an interval. One instance per component, so components (and
    /// circuits) never share a timer. A pending action can be cancelled, e.g. when the value it carries is stale.
    /// </summary>
    internal sealed class Debouncer : IDisposable
    {
        private CancellationTokenSource? _cts;

        /// <summary>
        /// Cancels any pending action and schedules <paramref name="action"/> to run after <paramref name="interval"/> ms.
        /// </summary>
        public void Debounce(int interval, Func<Task> action)
        {
            Cancel();
            var cts = _cts = new CancellationTokenSource();
            _ = RunAsync(interval, action, cts.Token);
        }

        /// <summary>
        /// Drops the pending action, if any.
        /// </summary>
        public void Cancel()
        {
            if (_cts == null) return;
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }

        private static async Task RunAsync(int interval, Func<Task> action, CancellationToken token)
        {
            try
            {
                await Task.Delay(interval, token);
                await action();
            }
            catch (OperationCanceledException) when (token.IsCancellationRequested)
            {
            }
        }

        public void Dispose() => Cancel();
    }
}
