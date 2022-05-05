namespace BlazorStrap
{
    public class TimerPlus : System.Timers.Timer
    {
        private DateTime m_dueTime;

        public TimerPlus() => Elapsed += this.ElapsedAction;

        protected new void Dispose()
        {
            Elapsed -= this.ElapsedAction;
            base.Dispose();
        }

        public double TimeLeft => (m_dueTime - DateTime.Now).TotalMilliseconds.RemoveNegative();
        public new void Start()
        {
            m_dueTime = DateTime.Now.AddMilliseconds(Interval);
            base.Start();
        }

        private void ElapsedAction(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if (AutoReset)
                m_dueTime = DateTime.Now.AddMilliseconds(Interval);
        }

    
    }
}