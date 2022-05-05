using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

// Created by Harold Collins
namespace BlazorStrap.Utilities
{
    /// <summary>
    /// Uses HttpClient to load the markup from an svg file and inject it into your html
    /// </summary>
    public class SvgLoader : ISvgLoader
    {
        private readonly HttpClient _httpClient;
        private bool isServerSide = false;
        private NavigationManager _navman;
        public SvgLoader(NavigationManager navman, IServiceProvider services)
        {
            // Server Side Blazor doesn't register HttpClient by default
            if (services.GetService(typeof(HttpClient)) == null)
            {
                isServerSide = true;
                _httpClient = new HttpClient();
            }
            else
            {
                _httpClient = services.GetServices<HttpClient>().First();
            }

            _navman = navman;
        }

        /// <summary>
        /// Loads an SVG file and returns the Markup
        /// </summary>
        /// <param name="url">The path of the svg file to load.</param>
        /// <returns>MarkupString</returns>
        public async Task<MarkupString> LoadSvg(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return new MarkupString(string.Empty);

            try
            {
                string markup;
                if (isServerSide)
                {
                    using (var httpClient = new HttpClient() { BaseAddress = new Uri(_navman.BaseUri) })
                    {
                        markup = await httpClient.GetStringAsync(url);
                    }
                }
                else
                {
                    markup = await _httpClient.GetStringAsync(url);
                }
                return new MarkupString(markup);
            }
            catch (ArgumentNullException)
            {
                return new MarkupString(string.Empty);
            }
            catch (HttpRequestException)
            {
                return new MarkupString(string.Empty);
            }
        }
    }
}
