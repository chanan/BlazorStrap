using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap
{
    public class BSToaster : ComponentBase, IDisposable
    {
        // ReSharper disable once NullableWarningSuppressionIsUsed
        [Inject] private IBlazorStrap? BlazorStrap { get; set; } = default!;

        /// <summary>
        /// Sets the wrapper class for the toaster.
        /// </summary>
        [Parameter] public string WrapperClass { get; set; } = "";
        protected override void OnInitialized()
        {
            if (BlazorStrap == null) throw new ArgumentNullException(nameof(BlazorStrap));
            if (BlazorStrap.Toaster == null) throw new ArgumentNullException(nameof(BlazorStrap.Toaster));
            BlazorStrap.Toaster.OnChange += OnChange;
        }

        private void OnChange()
        {
            RateLimitingExceptionForObject.Debounce("", 50,
                (CurrentValueAsString) => { InvokeAsync(StateHasChanged); });
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var lastPlacment = Toast.Default;
            var building = false;
            var i = 0;
            if (BlazorStrap == null) throw new ArgumentNullException(nameof(BlazorStrap));
            if (BlazorStrap.Toaster == null) throw new ArgumentNullException(nameof(BlazorStrap.Toaster));
            foreach (var toast in BlazorStrap.Toaster.Children.OrderBy(q => q.Placement).ThenBy(q => q.Created))
            {
                if (toast.Placement != Toast.Default)
                {
                    building = true;
                    // Close and open new wrapper when changes
                    if (lastPlacment != toast.Placement && i != 0)
                    {
                        builder.CloseElement();
                        builder.OpenElement(i, "div");
                        builder.AddAttribute(i + 1, "class", GetClass(toast.Placement));
                    }

                    //Open first wrapper
                    if (i == 0)
                    {
                        builder.OpenElement(i, "div");
                        builder.AddAttribute(i + 1, "class", GetClass(toast.Placement));
                    }

                    builder.OpenElement(i + 2, "div");
                    builder.SetKey(toast.Id);
                    builder.AddContent(i + 4, GetFragment(toast));
                    builder.CloseElement();
                    i = i + 5;
                    if (lastPlacment != toast.Placement)
                        lastPlacment = toast.Placement;
                }
            }

            if (building)
            {
                builder.CloseElement();
            }
        }

        public void Dispose()
        {
            if (BlazorStrap == null) throw new ArgumentNullException(nameof(BlazorStrap));
            if (BlazorStrap.Toaster == null) throw new ArgumentNullException(nameof(BlazorStrap.Toaster));
            BlazorStrap.Toaster.OnChange -= OnChange;
        }

        internal RenderFragment GetFragment(Toasts? Toast)
        {
            if (Toast == null) throw new ArgumentNullException(nameof(Toast));

            var header = new RenderFragment(childBuilder =>
            {
                childBuilder.AddContent(0, new MarkupString(Toast.HeaderText ?? ""));
            });
            var content = new RenderFragment(childBuilder =>
            {
                childBuilder.AddContent(0, new MarkupString(Toast.ContentText ?? ""));
                Console.WriteLine("Here 1");
                if (Toast.Options?.Template != null)
                {
                    Console.WriteLine("Here");
                    childBuilder.OpenComponent(1, Toast.Options.Template);
                    childBuilder.AddAttribute(2, "Data", Toast.Options.Data);
                    childBuilder.CloseComponent();
                }
            });

            return builder =>
            {
                if (Toast.Options == null) throw new ArgumentNullException(nameof(Toast.Options));
                if (BlazorStrap == null) throw new ArgumentNullException(nameof(BlazorStrap));
                if (BlazorStrap.Toaster == null) throw new ArgumentNullException(nameof(BlazorStrap.Toaster));
                if (BlazorStrap.Toaster.OnChange == null) throw new ArgumentNullException(nameof(BlazorStrap.Toaster.OnChange));

                builder.OpenComponent<BSToast>(0);
                builder.AddAttribute(1, "ButtonClass", Toast.Options.ButtonClass);
                builder.AddAttribute(2, "CloseAfter", Toast.Options.CloseAfter);
                builder.AddAttribute(3, "Color", Toast.Options.Color);
                if (!string.IsNullOrEmpty(Toast.HeaderText))
                {
                    builder.AddAttribute(4, "Header", header);
                }
                builder.AddAttribute(5, "Content", content);
                builder.AddAttribute(6, "ContentClass", Toast.Options.ContentClass);
                builder.AddAttribute(7, "HeaderClass", Toast.Options.HeaderClass);
                builder.AddAttribute(8, "TimeOut",
                    EventCallback.Factory.Create<BSToast>(this, BlazorStrap.Toaster.OnChange));
                builder.AddAttribute(9, "ToasterId", Toast.Id);
                builder.AddAttribute(10, "style", "z-index:1080;position:relative");
                builder.CloseComponent();
            };
        }

        public string GetClass(Toast pos)
        {
            var rootClassBuilder = new CssBuilder("blazorstrap-toaster")
                .AddClass(WrapperClass, !string.IsNullOrEmpty(WrapperClass))
                .Build().ToNullString();
            return rootClassBuilder + " " + pos switch
            {
                Toast.TopLeft => "position-absolute top-0 start-0",
                Toast.TopCenter => "position-absolute top-0 start-50 translate-middle-x",
                Toast.TopRight => "position-absolute top-0 end-0",
                Toast.MiddleLeft => "position-absolute top-50 start-0 translate-middle-y",
                Toast.MiddleCenter => "position-absolute top-50 start-50 translate-middle",
                Toast.MiddleRight => "position-absolute top-50 end-0 translate-middle-y",
                Toast.BottomLeft => "position-absolute bottom-0 start-0",
                Toast.BottomCenter => "position-absolute bottom-0 start-50 translate-middle-x",
                _ => "position-absolute bottom-0 end-0"
            };
        }
    }
}