namespace BlazorStrap.Extensions
{
    public class TimerPlus : System.Timers.Timer
    {
        private DateTime m_dueTime;

        public TimerPlus() => Elapsed += this.ElapsedAction;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Elapsed -= ElapsedAction;
            }
            base.Dispose(disposing);
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