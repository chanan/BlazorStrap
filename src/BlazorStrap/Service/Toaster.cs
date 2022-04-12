using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public class Toaster : ComponentBase
    {
        public Action? OnChange;
        internal List<Toasts> Children { get; } = new List<Toasts>();

        public void Add(string? header, string? content)
        {
            AddChild(header,content, o =>
            {
                o.Color = BSColor.Primary;
            });   
        }
        public void Add(string? content)
        {
            AddChild(null,content, o =>
            {
                o.Color = BSColor.Primary;
            });   
        }
        public void Add(string? content, Action<Options>? options)
        {
            AddChild(null,content, options);   
        }

        public void Add(string? header, string? content, Action<Options>? options)
        {
            AddChild(header, content, options);
        }
        private void AddChild(string? headerText, string? contentText, Action<Options>? options)
        {
            var opts = new Options();
            options?.Invoke(opts);
            if (opts.Toast == Toast.Default)
                opts.Toast = Toast.TopRight;
       
            var newChild = new Toasts
            {
                Id = Guid.NewGuid(),
                CloseAfter = opts.CloseAfter,
                Timer = new TimerPlus(),
                Options = new Options()
                {
                    ButtonClass = opts.ButtonClass,
                    CloseAfter = opts.CloseAfter ,
                    Color = opts.Color,
                    ContentClass = opts.ContentClass,
                    HeaderClass = opts.HeaderClass,
                    Toast = opts.Toast
                },
                Placement = opts.Toast,
                HeaderText = headerText,
                ContentText = contentText
            };
            if (opts.CloseAfter != 0)
            {
                newChild.Timer.Interval = opts.CloseAfter;
            }

            Children.Add(newChild);
            OnChange?.Invoke();
        }
        internal void RemoveChild(Guid? id)
        {
            var toast = Children.FirstOrDefault(q => q.Id == id);
            toast?.Timer?.Stop();
            toast?.Timer?.Dispose();
            if (toast != null) 
                Children.Remove(toast);
 
            OnChange?.Invoke();
        }
    }
    public class Options
    {
        public string? ButtonClass { get; set; }
        public int CloseAfter { get; set; } = 0;
        public BSColor Color { get; set; } = BSColor.Default;
        public Toast Toast { get; set; } = Toast.Default;
        public string? ContentClass { get; set; }
        public string? HeaderClass { get; set; }
    }
    internal class Toasts
    {
        public Guid? Id { get; set; }
        public bool Rendered { get; set; }
        public int CloseAfter { get; set; }
        public TimerPlus? Timer { get; set; }
        public Options? Options { get; set; }
        public string? HeaderText { get; set; }
        public string? ContentText { get; set; }
        internal int TimeRemaining { get; set; } = 0;
        public DateTime Created { get; } = DateTime.Now;
        public Toast Placement { get; set; } = Toast.Default;
    }
}