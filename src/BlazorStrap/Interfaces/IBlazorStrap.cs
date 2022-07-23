namespace BlazorStrap
{
    public interface IBlazorStrap
    {
        bool ShowDebugMessages { get; }
        Toaster Toaster { get; }
        public T CurrentTheme<T>() where T : Enum;
        Task SetBootstrapCss();
        Task SetBootstrapCss(string version);
        Task SetBootstrapCss(string theme, string version);
        Task SetBootstrapCss<T>(T theme, string version) where T : Enum;
        void ForwardClick(string id);
    }
}