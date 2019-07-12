using System.Threading.Tasks;

namespace BlazorStrap
{
    public interface IBootstrapCSS
    {
        Theme CurrentTheme { get; }
        Task SetBootstrapCSS();
        Task SetBootstrapCSS(string version);
        Task SetBootstrapCSS(string theme, string version);
        Task SetBootstrapCSS(Theme theme, string version);
    }
}