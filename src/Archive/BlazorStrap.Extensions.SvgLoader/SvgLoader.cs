using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorStrap.Extensions
{
    /// <summary>
    /// Uses HttpClient to load the markup from an svg file and inject it into your html
    /// </summary>
    public class SvgLoader : ISvgLoader
    {
        private readonly HttpClient _httpClient;

        public SvgLoader(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
                var markup = await _httpClient.GetStringAsync(url);

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
