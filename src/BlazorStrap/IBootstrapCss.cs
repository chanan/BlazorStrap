using System.Threading.Tasks;

namespace BlazorStrap
{
    public interface IBootstrapCss
    {
        Theme CurrentTheme { get; }
        Task SetBootstrapCss();
        Task SetBootstrapCss(string version);
        Task SetBootstrapCss(string theme, string version);
        Task SetBootstrapCss(Theme theme, string version);
    }
}