using System.Threading.Tasks;

namespace BlazorStrap
{
    public interface IBootstrapCSS
    {
        Task SetBootstrapCSS();
        Task SetBootstrapCSS(string version);
        Task SetBootstrapCSS(string theme, string version);
        Task SetBootstrapCSS(Theme theme, string version);
    }
}