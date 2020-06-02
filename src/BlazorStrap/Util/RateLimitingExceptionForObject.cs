using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BlazorStrap.Util
{
    public static class RateLimitingExceptionForObject
    {
        #region Private Properties

        private static Timer _throttleTimerInterval;
        private static Timer _debounceTimerInterval;

        private static Action<object> _debounceAction;
        private static object _lastObjectDebounce;

        private static Action<object> _throttleAction;
        private static object _lastObjectThrottle;

        #endregion

        #region Debounce

        /// <summary>
        /// Debounce reset timer and after last item received give you last item. 
        /// </summary>
        /// <param name="obj">Your object</param>
        /// <param name="interval">Milisecond interval</param>
        /// <param name="debounceAction">Called when last item call this method and after interval was finished</param>
        public static void Debounce(object obj, int interval, Action<object> debounceAction)
        {
            _lastObjectDebounce = obj;
            _debounceAction = debounceAction;

            _debounceTimerInterval?.Dispose();

            _debounceTimerInterval = new Timer(DebounceTimerIntervalOnTick, obj, interval, interval);
        }

        /// <summary>
        /// DispatchTimer tick event for debounce
        /// </summary>
        /// <param name="state"></param>
        private static void DebounceTimerIntervalOnTick(object state)
        {
            _debounceTimerInterval?.Dispose();

            if (_debounceTimerInterval != null)
            {
                _debounceAction?.Invoke(_lastObjectDebounce);
            }

            _debounceTimerInterval = null;
        }

        #endregion

        #region Throttle

        /// <summary>
        /// Throttle give you last object when timer was ticked and invoke throttleAction callback.
        /// </summary>
        /// <param name="obj">Your object</param>
        /// <param name="interval">Milisecond interval</param>
        /// <param name="throttleAction">Invoked last object when timer ticked invoked</param>
        public static void Throttle(object obj, int interval, Action<object> throttleAction)
        {
            _lastObjectThrottle = obj;
            _throttleAction = throttleAction;

            if (_throttleTimerInterval == null)
            {
                _throttleTimerInterval = new Timer(ThrottleTimerIntervalOnTick, obj, interval, interval);
            }
        }

        /// <summary>
        /// DispatchTimer tick event for throttle
        /// </summary>
        /// <param name="state"></param>
        private static void ThrottleTimerIntervalOnTick(object state)
        {
            _throttleTimerInterval?.Dispose();
            _throttleTimerInterval = null;

            if (_lastObjectThrottle != null)
            {
                _throttleAction?.Invoke(_lastObjectThrottle);
            }
        }

        #endregion
    }
}
